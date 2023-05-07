using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginUI : MonoBehaviour
{
    LoginData loginData = new LoginData();

    [SerializeField] private UIDocument loginDoc;

    [SerializeField] private List<string> userTypes;

    [SerializeField] private Color errorColor;

    private void Awake()
    {
        GlobalData.UserTypes = userTypes;

        var root = loginDoc.rootVisualElement;
        var loginBtn = root.Q<Button>("loginBtn");
        var userTypeSelect = root.Q<DropdownField>("selectUser");
        var institutionTxt = root.Q<TextField>("institutionTxt");
        var usernameTxt = root.Q<TextField>("usernameTxt");
        var passwordTxt = root.Q<TextField>("passwordTxt");
        var messagegTxt = root.Q<VisualElement>("status-message").Q<Label>();

        messagegTxt.text = "";

        userTypeSelect.choices = GlobalData.UserTypes;

        loginBtn.clicked += () => {

            loginData.Institution = institutionTxt.value;
            loginData.Username = usernameTxt.value;
            loginData.Password = passwordTxt.value;

            string userType = userTypeSelect.value;

            InputValidation(userType, loginData, messagegTxt);
        };

        if (messagegTxt.text == "")
        {
            messagegTxt.style.visibility = Visibility.Hidden;
        }
    }

    private void GetData (string userType, LoginData inputData, Label label)
    {
        // Define headers, and payload for the request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");
        string payload = $"{{\"institution\":\"{inputData.Institution}\",\"name\":\"{inputData.Username}\",\"password\":\"{inputData.Password}\"}}";

        SendRequests sendPostRequest = GetComponent<SendRequests>();

        GlobalMethods.DisplayMessage(label, "Please Wait...");

        if (userType == userTypes[0])
        {
            sendPostRequest.SendPostPutRequest(GlobalData.url + "/teacherlogin", GlobalData.methodPost, headers, payload, label, (responseJson) => {
                GlobalUser.AssignUser(userType,
                    responseJson["userid"].Value<string>(),
                    responseJson["name"].Value<string>(),
                    responseJson["institution"].Value<string>(),
                    responseJson["dp"].Value<string>());

                JArray subjectArray = (JArray)responseJson["subjects"];

                GlobalMethods.InitializeSubjects(subjectArray);

                JArray classroomArray = (JArray)responseJson["classrooms"];

                GlobalMethods.InitializeClassrooms(classroomArray);

                GlobalMethods.LoadScene("TeacherDashboardClassroom");
            });
        }
        else if (userType == userTypes[1])
        {
            sendPostRequest.SendPostPutRequest(GlobalData.url + "/studentlogin", GlobalData.methodPost, headers, payload, label, (responseJson) => {
                GlobalUser.AssignUser(userType,
                    responseJson["userid"].Value<string>(),
                    responseJson["name"].Value<string>(),
                    responseJson["institution"].Value<string>(),
                    responseJson["dp"].Value<string>());

                JArray subjectArray = (JArray)responseJson["subjects"];

                GlobalMethods.InitializeSubjects(subjectArray);

                GlobalData.classroom = responseJson["classroom"].Value<string>();

                GlobalMethods.LoadScene("StudentDashboardResults");
            });
        }
    }

    private void InputValidation(string userType, LoginData inputData, Label label)
    {
        if (userType == null || inputData.Institution == "" || inputData.Username == "" || inputData.Password == "")
        {
            GlobalMethods.DisplayMessage(label, "Fill All Fields", true);
        }
        else
        {
            GetData(userType, inputData, label);
        }
    }
}

public interface ILoginContainer
{
    string Institution { get; set; }
    string Username { get; set; }
    string Password { get; set; }
}

public class LoginData : ILoginContainer
{
    public string Institution { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
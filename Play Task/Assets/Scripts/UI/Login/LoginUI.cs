using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginUI : MonoBehaviour
{
    LoginData loginData = new LoginData();

    [SerializeField] private UIDocument loginDoc;

    [SerializeField] private List<string> userTypes;

    private void Awake()
    {
        GlobalData.UserTypes = userTypes;

        var root = loginDoc.rootVisualElement;
        var loginBtn = root.Q<Button>("loginBtn");
        var userTypeSelect = root.Q<DropdownField>("selectUser");
        var institutionTxt = root.Q<TextField>("institutionTxt");
        var usernameTxt = root.Q<TextField>("usernameTxt");
        var passwordTxt = root.Q<TextField>("passwordTxt");
        var errorTxt = root.Q<VisualElement>("error-message").Q<Label>();

        errorTxt.text = "";

        userTypeSelect.choices = GlobalData.UserTypes;

        loginBtn.clicked += () => {

            loginData.Institution = institutionTxt.value;
            loginData.Username = usernameTxt.value;
            loginData.Password = passwordTxt.value;

            string userType = userTypeSelect.value;

            InputValidation(userType, loginData, errorTxt);
        };

        if (errorTxt.text == "")
        {
            errorTxt.style.visibility = Visibility.Hidden;
        }
    }

    private void GetData (string userType, LoginData inputData, Label errMsg)
    {
        // Define URL, method, headers, and payload for the request
        string url = "http://localhost:3000/api/teacherlogin";
        string method = "POST";
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string payload = $"{{\"institution\":\"{inputData.Institution}\",\"name\":\"{inputData.Username}\",\"password\":\"{inputData.Password}\"}}";

        SendPostRequest sendPostRequest = GetComponent<SendPostRequest>();
        sendPostRequest.SendPostData(url, method, headers, payload, errMsg);

        //if (userType == userTypes[0])
        //{
        //    GlobalMethods.AssignUser(loginData.Username, loginData.Institution);
        //    GlobalMethods.LoadScene("TeacherDashboardClassroom");
        //}
        //else if (userType == userTypes[1])
        //{
        //    //GlobalMethods.AssignUser(loginDummyDataStudent.Username, loginDummyDataStudent.Institution);
        //    GlobalMethods.LoadScene("StudentDashboardSubject");
        //}
    }

    private void InputValidation(string userType, LoginData inputData, Label errMsg)
    {
        if (userType == null || inputData.Institution == "" || inputData.Username == "" || inputData.Password == "")
        {
            GlobalMethods.DisplayError(errMsg, "Fill All Fields");
        }
        else
        {
            GetData(userType, inputData, errMsg);
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
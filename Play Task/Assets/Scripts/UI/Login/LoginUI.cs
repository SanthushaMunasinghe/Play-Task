using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    LoginData loginDummyDataTeacher = new LoginData();
    LoginData loginDummyDataStudent = new LoginData();

    [SerializeField] private UIDocument loginDoc;

    [SerializeField] private List<string> userTypes = new List<string>();

    [SerializeField] private Color errorColor;

    private List<string> inputData = new List<string>();

    private string error;

    private void Awake()
    {
        var root = loginDoc.rootVisualElement;
        var loginBtn = root.Q<Button>("loginBtn");
        var userTypeSelect = root.Q<DropdownField>("selectUser");
        var institutionTxt = root.Q<TextField>("institutionTxt");
        var usernameTxt = root.Q<TextField>("usernameTxt");
        var passwordTxt = root.Q<TextField>("passwordTxt");
        var errorTxt = root.Q<VisualElement>("error-message").Q<Label>();

        userTypeSelect.choices = userTypes;

        loginBtn.clicked += () => {
            error = "";

            inputData.Add(institutionTxt.value);
            inputData.Add(usernameTxt.value);
            inputData.Add(passwordTxt.value);

            string userType = userTypeSelect.value;

            InputValidation(userType, inputData);

            if (error != "")
            {
                errorTxt.text = "Error: " + error;
                errorTxt.style.visibility = Visibility.Visible;
            }
            else
            {
                errorTxt.style.visibility = Visibility.Hidden;
            }
        };
    }

    private void GetData (string userType, List<string> inputData)
    {
        loginDummyDataTeacher.Institution = "Thurstan College";
        loginDummyDataTeacher.Username = "DSKumara";
        loginDummyDataTeacher.UserID = "123456789";

        loginDummyDataStudent.Institution = "Thurstan College";
        loginDummyDataStudent.Username = "MASHMunasinghe";
        loginDummyDataStudent.UserID = "123456789";

        if (userType == userTypes[0])
        {
            GlobalMethods.AssignUser(loginDummyDataTeacher.Username, loginDummyDataTeacher.Institution);
            GlobalMethods.LoadScene("TeacherDashboardClassroom");
        }
        else if (userType == userTypes[1])
        {
            GlobalMethods.AssignUser(loginDummyDataStudent.Username, loginDummyDataStudent.Institution);
            GlobalMethods.LoadScene("StudentDashboardSubject");
        }
    }

    private void InputValidation(string userType, List<string> inputData)
    {
        if (inputData.Count < 3)
        {
            error = "Fill All Fields";
        }
        else
        {
            GetData(userType, inputData);
        }
    }
}

public interface ILoginContainer
{
    string Institution { get; set; }
    string Username { get; set; }
    string UserID { get; set; }
}

public class LoginData : ILoginContainer
{
    public string Institution { get; set; }
    public string Username { get; set; }
    public string UserID { get; set; }
}
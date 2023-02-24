using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public static class GlobalMethods
{
    //Assign Data
    public static void AssignUser(string type, string id, string name, string institution, string dp)
    {
        GlobalUser.userData.UserType = type;
        GlobalUser.userData.UserID = id;
        GlobalUser.userData.Username = name;
        GlobalUser.userData.Institution = institution;
        GlobalUser.userData.dp = dp;
        Debug.Log(GlobalUser.userData.UserID);
    }

    //Assign Subjects
    public static void InitializeSubjects(JArray inputSubjects)
    {
        foreach (string subject in inputSubjects)
        {
            GlobalData.subjects.Add(subject);
        }

        Debug.Log(GlobalData.subjects.Count);
    }
    
    //Assign Classrooms
    public static void InitializeClassrooms(JArray inputClassrooms)
    {
        foreach (string classroom in inputClassrooms)
        {
            GlobalData.classrooms.Add(classroom);
        }

        Debug.Log(GlobalData.classrooms.Count);
    }

    //Display
    public static void DisplayUser(UIDocument UIDoc)
    {
        var userBar = UIDoc.rootVisualElement.Q<VisualElement>("user-bar");

        userBar.Q<Label>().text = GlobalUser.userData.Username;
    }

    public static void DisplayMessage(Label txt, string message, bool isError = false)
    {
        txt.style.visibility = Visibility.Visible;
        txt.text = message;

        if (isError)
        {
            txt.style.backgroundColor = new Color(1f, 0.302f, 0.302f, 1f);
        }
        else
        {
            txt.style.backgroundColor = Color.black;
        }
    }

    //Click
    //Next Back
    public static void NextBackBtn(bool isNext, Label label, ref int count, List<string> list, string labelTxt = "")
    {
        if (isNext && count < list.Count - 1)
        {
            count++;
        }

        if (!isNext && count > 0)
        {
            count--;
        }

        label.text = labelTxt + list[count];
    }

    //Scene
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public static class GlobalMethods
{
    //Assign Data
    public static void AssignUser(UserData getUserData)
    {
        GlobalUser.userData = getUserData;
    }

    public static void AssignTeacher(TeacherData getTeacherData)
    {
        GlobalTeacher.teacherData = getTeacherData;
    }

    //Display
    public static void DisplayUser(UIDocument UIDoc)
    {
        var userBar = UIDoc.rootVisualElement.Q<VisualElement>("user-bar");

        userBar.Q<Label>().text = GlobalUser.userData.Username;
    }

    public static void DisplayError(Label txt, string message)
    {
        txt.style.visibility = Visibility.Visible;
        txt.text = message;
    }

    //Scene
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

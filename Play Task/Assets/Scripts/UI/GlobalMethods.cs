using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public static class GlobalMethods
{
    public static string UserName;
    public static string Institution;

    public static void AssignUser(string userName, string institution)
    {
        UserName = userName;
        Institution = institution;
    }

    public static void DisplayUser(UIDocument UIDoc)
    {
        var userBar = UIDoc.rootVisualElement.Q<VisualElement>("user-bar");

        userBar.Q<Label>().text = UserName;
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

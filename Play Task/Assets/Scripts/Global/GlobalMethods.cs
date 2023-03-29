
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public static class GlobalMethods
{
    //Assign Subjects
    public static void InitializeSubjects(JArray inputSubjects)
    {
        foreach (string subject in inputSubjects)
        {
            GlobalData.subjects.Add(subject);
        }
    }
    
    //Assign Classrooms
    public static void InitializeClassrooms(JArray inputClassrooms)
    {
        foreach (string classroom in inputClassrooms)
        {
            GlobalData.classrooms.Add(classroom);
        }
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
    public static void NextBackBtn(bool isNext, ref int index, int max)
    {
        if (isNext && index < max - 1)
        {
            index++;
        }

        if (!isNext && index > 0)
        {
            index--;
        }
    }

    //Scene
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Input Float Validation
    public static bool ValidateTransformInput(string input)
    {
        if (float.TryParse(input, out float floatValue))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidateMinMax(float min, float max, float value)
    {
        if (value >= min && value <= max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Switch Bool
    public static bool SwitchBool(bool isTrue)
    {
        return !isTrue;
    }

    public static string SetValue(bool isTrue)
    {
        if (isTrue)
        {
            return "On";
        }
        else
        {
            return "Off";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static List<string> UserTypes = new List<string>();

    public static List<string> subjects = new List<string>();
    public static List<string> classrooms = new List<string>();
    public static string classroom;

    public static string projectID;
    public static string projectData;

    public static string gameMode;

    public static GameplayData currentGameplayData;

    public static string url = "http://localhost:3000/api";
    public static string methodPost = "POST";
    public static string methodPut = "PUT";
    public static string methodGet = "Get";
}



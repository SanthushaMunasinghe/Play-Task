using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static List<string> UserTypes = new List<string>();

    public static List<string> subjects = new List<string>();
    public static List<string> classrooms = new List<string>();

    public static string url = "http://localhost:3000/api";
    public static string methodPost = "POST";
    public static string methodGet = "Get";
}


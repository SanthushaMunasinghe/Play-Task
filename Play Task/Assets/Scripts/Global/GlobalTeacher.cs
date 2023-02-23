using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalTeacher
{
    public static TeacherData teacherData = new TeacherData();
}

public interface ITeacherContainer
{
    string[] Subjects { get; set; }
    string[] Classrooms { get; set; }
}

public class TeacherData : ITeacherContainer
{
    public string[] Subjects { get; set; }
    public string[] Classrooms { get; set; }
}

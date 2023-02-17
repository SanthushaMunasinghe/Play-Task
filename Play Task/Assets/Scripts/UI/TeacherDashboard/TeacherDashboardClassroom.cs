using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeacherDashboardClassroom : MonoBehaviour
{
    private UIDocument tdcDoc;

    protected List<StudentData> DummyStudentData = new List<StudentData>();

    protected VisualElement classroomBox;
    protected VisualElement studentBox;

    private void Awake()
    {
        tdcDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = tdcDoc.rootVisualElement;

        var toolBar = root.Q<VisualElement>("toolbar");
        var classroomBtn = toolBar.Q<Button>("classroom-btn");
        var subjectBtn = toolBar.Q<Button>("subject-btn");

        classroomBox = root.Q<VisualElement>("classroom-list-box");
        studentBox = root.Q<VisualElement>("student-details-box");

        GlobalMethods.DisplayUser(tdcDoc);

        Dictionary<string, string>[] toolBarBtns = new Dictionary<string, string>[2];

        classroomBtn.clicked += () =>
        {
            GlobalMethods.LoadScene("TeacherDashboardClassroom");
        };
        
        subjectBtn.clicked += () =>
        {
            GlobalMethods.LoadScene("TeacherDashboardSubject");
        };
    }

    public void InitializeStudentData()
    {
        for (int i = 0; i < 10; i++)
        {
            StudentData newData = new StudentData();
            newData.Username = "Student" + (i + 1);
            newData.UserID = "123456789" + (i + 1);
            newData.Phone = "0123456789" + (i + 1);
            newData.Email = "student@gmail.com";
            newData.Home = (i + 1) + "student home";

            DummyStudentData.Add(newData);
        }
    }
}
 

public interface IStudentContainer
{
    string Username { get; set; }
    string UserID { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    string Home { get; set; }
}

public class StudentData : IStudentContainer
{
    public string Username { get; set; }
    public string UserID { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Home { get; set; }
}

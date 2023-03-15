using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeacherDashboardClassroom : MonoBehaviour
{
    private UIDocument tdcDoc;

    //UI Elements
    protected VisualElement classroomBox;
    protected VisualElement studentBox;

    //Lists
    protected List<Dictionary<string, string>> classroomList = new List<Dictionary<string, string>>();
    protected List<StudentData> studentDataList = new List<StudentData>();

    //Methods
    protected SendRequests sendRequests;

    private void Awake()
    {
        tdcDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        sendRequests = gameObject.GetComponent<SendRequests>();

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
}
 

public interface IStudentContainer
{
    string Stdname { get; set; }
    string StdID { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    string Home { get; set; }
    string Classroom { get; set; }
}

public class StudentData : IStudentContainer
{
    public string Stdname { get; set; }
    public string StdID { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Home { get; set; }
    public string Classroom { get; set; }
}

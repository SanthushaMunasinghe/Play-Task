using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeacherDashboardSubjects : MonoBehaviour
{
    private UIDocument tdsDoc;

    //Protected Variables

    //UI Elements Classroom
    protected VisualElement subjectBox;

    //UI Elements Student
    protected VisualElement subtopicBox;

    //Lists
    protected List<Dictionary<string, string>> subjectList = new List<Dictionary<string, string>>();
    protected List<Dictionary<string, string>> topicList = new List<Dictionary<string, string>>();
    //Methods
    protected SendRequests sendRequests;

    void Awake()
    {
        tdsDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        sendRequests = gameObject.GetComponent<SendRequests>();

        var root = tdsDoc.rootVisualElement;

        var toolBar = root.Q<VisualElement>("toolbar");
        var classroomBtn = toolBar.Q<Button>("classroom-btn");
        var subjectBtn = toolBar.Q<Button>("subject-btn");

        subjectBox = root.Q<VisualElement>("subject-list-box");
        subtopicBox = root.Q<VisualElement>("subtopics-box");

        GlobalMethods.DisplayUser(tdsDoc);

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

public interface ISubtopicContainer
{
    string SbtID { get; set; }
    string TopicID { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    string[] Instructions { get; set; }
}

public class SubtopicData : ISubtopicContainer
{
    public string SbtID { get; set; }
    public string TopicID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string[] Instructions { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StudentDashboardSubjects : MonoBehaviour
{
    private UIDocument sdsDoc;

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
        sdsDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        sendRequests = gameObject.GetComponent<SendRequests>();

        var root = sdsDoc.rootVisualElement;

        var toolBar = root.Q<VisualElement>("toolbar");
        var classroomBtn = toolBar.Q<Button>("classroom-btn");
        var subjectBtn = toolBar.Q<Button>("subject-btn");

        subjectBox = root.Q<VisualElement>("subject-list-box");
        subtopicBox = root.Q<VisualElement>("subtopics-box");

        GlobalMethods.DisplayUser(sdsDoc);

        Dictionary<string, string>[] toolBarBtns = new Dictionary<string, string>[2];

        classroomBtn.clicked += () =>
        {
            GlobalMethods.LoadScene("StudentDashboardResults");
        };

        subjectBtn.clicked += () =>
        {
            GlobalMethods.LoadScene("StudentDashboardSubject");
        };
    }
}

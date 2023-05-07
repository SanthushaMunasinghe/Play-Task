using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StudentDashboardResults : MonoBehaviour
{
    private UIDocument tdcDoc;

    //Protected Variables

    //UI Elements Classroom
    protected VisualElement classroomBox;

    //UI Elements Student
    protected VisualElement studentBox;

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
            GlobalMethods.LoadScene("StudentDashboardResults");
        };

        subjectBtn.clicked += () =>
        {
            GlobalMethods.LoadScene("StudentDashboardSubject");
        };
    }
}

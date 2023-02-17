using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Classroom : TeacherDashboardClassroom
{
    public List<string> classNames = new List<string>();

    private int classCount = 0;

    [SerializeField] private Color highlightColor;

    void Start()
    {
        InitializeStudentData();

        var classroomLabel = classroomBox.Q<VisualElement>("classroom-label").Q<Label>();
        classroomLabel.text = "Classroom " + classNames[classCount];

        var nextBtn = classroomBox.Q<Button>("classroom-next-btn");
        var backBtn = classroomBox.Q<Button>("classroom-back-btn");

        var studentList = classroomBox.Q<ScrollView>("student-list");

        nextBtn.clicked += () =>
        {
            NextBackBtn(true, classroomLabel);
        };

        backBtn.clicked += () =>
        {
            NextBackBtn(false, classroomLabel);
        };

        for (int i = 0; i < DummyStudentData.Count; i++)
        {
            DisplayStudentList(studentList, DummyStudentData[i], i+1);
        }
    }

    private void NextBackBtn(bool isNext, Label label)
    {
        if (isNext && classCount < classNames.Count - 1)
        {
            classCount++;
        }

        if (!isNext && classCount > 0)
        {
            classCount--;
        }

        label.text = "Classroom " + classNames[classCount];
    }

    private void DisplayStudentList(ScrollView list, StudentData studentData, int number)
    {
        VisualElement newItem = new VisualElement();
        newItem.AddToClassList("list-primary-item");

        Label studentNo = new Label();
        Label studentName = new Label();
        Label studentID = new Label();

        studentNo.AddToClassList("list-primary-item-text");

        studentName.AddToClassList("list-primary-item-text");
        studentID.AddToClassList("list-primary-item-text");

        studentNo.text = number.ToString() + ".";
        studentName.text = studentData.Username;
        studentID.text = studentData.UserID;

        newItem.Add(studentNo);
        newItem.Add(studentName);
        newItem.Add(studentID);

        list.Add(newItem);
    }
}

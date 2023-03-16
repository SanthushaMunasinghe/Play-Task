using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Student : TeacherDashboardClassroom
{
    public StudentData selectedStudent;
    public List<Dictionary<string, string>> subjectList = new List<Dictionary<string, string>>();

    private Label studentLabel;

    private int tabIndex = 0;

    private List<VisualElement> tabList = new List<VisualElement>();
    private List<string> tabTitles = new List<string>();

    //Details Box Visual Elements
    private Label nameBox;
    private Label phoneBox;
    private Label emailBox;
    private Label homeBox;

    void Start()
    {
        studentLabel = studentBox.Q<VisualElement>("details-label").Q<Label>();

        //Tabs
        VisualElement detailsBox = studentBox.Q<VisualElement>("details-box");
        VisualElement termAnalyticsBox = studentBox.Q<VisualElement>("term-analytics-box");
        VisualElement subjectAnalytics = studentBox.Q<VisualElement>("subject-analytics-box");

        tabList.Add(detailsBox);
        tabList.Add(termAnalyticsBox);
        tabList.Add(subjectAnalytics);

        tabTitles.Add("Student Details");
        tabTitles.Add("Student Results");
        tabTitles.Add("Student Analytics");

        //Details Box
        nameBox = studentBox.Q<VisualElement>("student-detail-name").Q<Label>();
        phoneBox = studentBox.Q<VisualElement>("student-detail-phone").Q<Label>();
        emailBox = studentBox.Q<VisualElement>("student-detail-email").Q<Label>();
        homeBox = studentBox.Q<VisualElement>("student-detail-home").Q<Label>();

        //Buttons
        var nextBtn = studentBox.Q<Button>("student-next-btn");
        var backBtn = studentBox.Q<Button>("student-back-btn");

        nextBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(true, ref tabIndex, tabList.Count);
            SelectTab();
        };

        backBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(false, ref tabIndex, tabList.Count);
            SelectTab();
        };
    }

    void Update()
    {
        for (int i = 0; i < tabTitles.Count; i++)
        {
            if (tabIndex == 1)
            {
                studentLabel.text = tabTitles[i];
            }
        }

        if (selectedStudent != null)
        {
            PopulateDetails(selectedStudent);
        }
    }

    private void SelectTab()
    {
        for (int i = 0; i < tabList.Count; i++)
        {
            if (tabIndex == i)
            {
                tabList[i].style.display = DisplayStyle.Flex;
            }
            else
            {
                tabList[i].style.display = DisplayStyle.None;
            }
        }
    }

    private void PopulateDetails(StudentData data)
    {
        nameBox.text = "Name: " + data.Stdname;
        phoneBox.text = "Phone: " + data.Phone;
        emailBox.text = "Email: " + data.Email;
        homeBox.text = "Home: " + data.Home;
    }

    public void GetStudentSubjects(string subjectId)
    {
        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        Label label = new Label();

        string idResponse;
        string nameResponse;
        string gradeResponse;

        sendRequests.SendGetRequest(GlobalData.url + "/getsubject/" + subjectId, headers, label, (responseJson) => {

            idResponse = responseJson["_id"].Value<string>();
            nameResponse = responseJson["name"].Value<string>();

            sendRequests.SendGetRequest(GlobalData.url + "/getgradebyid/" + responseJson["grade"], headers, label, (responseJson) => {
                gradeResponse = responseJson["grade"].Value<string>();

                Dictionary<string, string> currentSubject = new Dictionary<string, string>();
                currentSubject.Add("Id", idResponse);
                currentSubject.Add("Name", nameResponse);
                currentSubject.Add("Grade", gradeResponse);

                subjectList.Add(currentSubject);
            });
        });
    }
}

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StudentDashboardStudentData : StudentDashboardResults
{
    StudentData student;

    public List<Dictionary<string, string>> subjectList = new List<Dictionary<string, string>>();

    private Label studentLabel;

    private int tabIndex = 0;

    private List<VisualElement> tabList = new List<VisualElement>();
    private List<string> tabTitles = new List<string>();

    //UI Elements
    private Label classroomLabel;
    private ScrollView studentList;

    //Details Box Visual Elements
    private Label nameBox;
    private Label phoneBox;
    private Label emailBox;
    private Label homeBox;

    void Start()
    {
        //Get Values
        classroomLabel = classroomBox.Q<VisualElement>("classroom-label").Q<Label>();
        studentList = classroomBox.Q<ScrollView>("student-list");

        //Student Details
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

        GetStudent(GlobalUser.userData.UserID);
    }

    private void GetStudent(string userId)
    {
        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        Label label = new Label();

        sendRequests.SendGetRequest(GlobalData.url + "/getstudent/" + userId, headers, label, (responseJson) => {
            student = new StudentData();
            student.Stdname = responseJson["name"].Value<string>();
            student.StdID = responseJson["id"].Value<string>();
            student.Phone = responseJson["contactno"].Value<string>();
            student.Email = responseJson["email"].Value<string>();
            student.Home = responseJson["home"].Value<string>();
            student.Classroom = responseJson["classroom"].Value<string>();
            student.Subjects = responseJson["subjects"].ToObject<string[]>();

            sendRequests.SendGetRequest(GlobalData.url + "/getclassroombyid/" + student.Classroom, headers, label, (responseJson) =>
            {
                string classroomName = responseJson["name"].ToString();

                sendRequests.SendGetRequest(GlobalData.url + "/getgradebyid/" + responseJson["grade"].ToString(), headers, label, (responseJson) =>
                {
                    classroomLabel.text = $"Classroom {responseJson["number"]}-{classroomName}";
                });

            });

            DisplayStudentList(studentList, student, 1);
        });
    }

    private void DisplayStudentList(ScrollView list, StudentData studentData, int number)
    {
        VisualElement newItem = new VisualElement();

        newItem.AddToClassList("list-primary-item");

        Label studentNo = new Label();
        Label studentName = new Label();
        Label studentEmail = new Label();

        studentNo.AddToClassList("list-primary-item-text");

        studentName.AddToClassList("list-primary-item-text");
        studentEmail.AddToClassList("list-primary-item-text");

        studentName.name = "studentName";

        studentNo.text = number.ToString() + ".";
        studentName.text = studentData.Stdname;
        studentEmail.text = studentData.Email;

        newItem.Add(studentNo);
        newItem.Add(studentName);
        newItem.Add(studentEmail);

        list.Add(newItem);

        GetComponent<StudentSubjectResults>().GetStudentResults(studentData);
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

        if (student != null)
        {
            PopulateDetails(student);
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
}

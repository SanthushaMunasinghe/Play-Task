using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Student : TeacherDashboardClassroom
{
    public StudentData selectedStudent;
    //public List<Dictionary<string, string>> subjectList = new List<Dictionary<string, string>>();
    public List<ISubjectc> subjectList = new List<ISubjectc>();
    public List<ITopic> topicList = new List<ITopic>();
    public List<SubtopicData> subtopicList = new List<SubtopicData>();
    public List<ISubtopictResults> subtopicResultsList = new List<ISubtopictResults>();

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

        //Results Box


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
            if (tabIndex == i)
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

        //Get Subject
        sendRequests.SendGetRequest(GlobalData.url + "/getsubject/" + subjectId, headers, label, (responseJson) => {

            idResponse = responseJson["_id"].Value<string>();
            nameResponse = responseJson["name"].Value<string>();

            //Get Grade
            sendRequests.SendGetRequest(GlobalData.url + "/getgradebyid/" + responseJson["grade"], headers, label, (responseJson) => {
                gradeResponse = responseJson["number"].Value<string>();

                ISubjectc newSubject = new ISubjectc();
                newSubject.SubjectID = idResponse;
                newSubject.Name = nameResponse;
                newSubject.Grade = gradeResponse;

                subjectList.Add(newSubject);

                //Get Topics
                sendRequests.GetArray(GlobalData.url + "/getsubjecttopics/" + newSubject.SubjectID, headers, label, (responseArray) => {
                    if (responseArray.Count != 0)
                    {
                        foreach (JObject topicObject in responseArray)
                        {
                            ITopic newTopic = new ITopic();

                            newTopic.TopicID = topicObject["_id"].Value<string>();
                            newTopic.SubjectID = topicObject["subject"].Value<string>();
                            newTopic.TopicName = topicObject["title"].Value<string>();

                            topicList.Add(newTopic);

                            //Get Subtopics
                            sendRequests.GetArray(GlobalData.url + "/getsubtopics/" + newTopic.TopicID, headers, label, (responseArray) => {
                                if (responseArray.Count != 0)
                                {
                                    foreach (JObject topicObject in responseArray)
                                    {
                                        SubtopicData newSubtopic = new SubtopicData();

                                        newSubtopic.SbtID = topicObject["_id"].Value<string>();
                                        newSubtopic.TopicID = topicObject["topic"].Value<string>();
                                        newSubtopic.Title = topicObject["title"].Value<string>();

                                        subtopicList.Add(newSubtopic);
                                    }
                                }
                            });
                        }
                    }
                });
            });
        });
    }
}

//Subject
public interface ISubjectContainer
{
    string SubjectID { get; set; }
    string Name { get; set; }
    string Grade { get; set; }
}

public class ISubjectc : ISubjectContainer
{
    public string SubjectID { get; set; }
    public string Name { get; set; }
    public string Grade { get; set; }
}


//Topic
public interface ITopicContainer
{
    string TopicID { get; set; }
    string SubjectID { get; set; }
    string TopicName { get; set; }
}

public class ITopic : ITopicContainer
{
    public string TopicID { get; set; }
    public string SubjectID { get; set; }
    public string TopicName { get; set; }
}

//Subtopic Results
public interface ISubtopicResultsContainer
{
    string ResultsID { get; set; }
    string SubtopicID { get; set; }
    string SubtopicName { get; set; }
    GameplayData GameplayData { get; set; }
}

public class ISubtopictResults : ISubtopicResultsContainer
{
    public string ResultsID { get; set; }
    public string SubtopicID { get; set; }
    public string SubtopicName { get; set; }
    public GameplayData GameplayData { get; set; }
}

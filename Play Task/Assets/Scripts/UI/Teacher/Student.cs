using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Student : TeacherDashboardClassroom
{
    public StudentData selectedStudent;
    public List<string> subjectList = new List<string>();
    public List<string> topicList = new List<string>();
    public List<string> subtopicList = new List<string>();
    public List<IAttempt> attemptList = new List<IAttempt>();
    public List<ISubject> subjectDataList = new List<ISubject>();

    private Label studentLabel;

    private int tabIndex = 0;

    private List<VisualElement> tabList = new List<VisualElement>();
    private List<string> tabTitles = new List<string>();

    //Details Box Elements
    private Label nameBox;
    private Label phoneBox;
    private Label emailBox;
    private Label homeBox;

    //Results Box Elements
    private Label subjectCountLabel;
    private Label totalLabel;
    private Label averageLabel;
    private ScrollView subjectsListView;

    //Subject Box
    private ScrollView subjectlistView;

    void Start()
    {
        studentLabel = studentBox.Q<VisualElement>("details-label").Q<Label>();

        //Tabs
        VisualElement detailsBox = studentBox.Q<VisualElement>("details-box");
        VisualElement termAnalyticsBox = studentBox.Q<VisualElement>("term-analytics-box");
        VisualElement subjectAnalyticsBox = studentBox.Q<VisualElement>("subject-analytics-box");

        tabList.Add(detailsBox);
        tabList.Add(termAnalyticsBox);
        tabList.Add(subjectAnalyticsBox);

        tabTitles.Add("Student Details");
        tabTitles.Add("Student Results");
        tabTitles.Add("Student Analytics");

        //Details Box
        nameBox = studentBox.Q<VisualElement>("student-detail-name").Q<Label>();
        phoneBox = studentBox.Q<VisualElement>("student-detail-phone").Q<Label>();
        emailBox = studentBox.Q<VisualElement>("student-detail-email").Q<Label>();
        homeBox = studentBox.Q<VisualElement>("student-detail-home").Q<Label>();

        //Results Box
        subjectCountLabel = studentBox.Q<VisualElement>("subjects-count").Q<VisualElement>("detail-content").Q<Label>();
        totalLabel = studentBox.Q<VisualElement>("total").Q<VisualElement>("detail-content").Q<Label>();
        averageLabel = studentBox.Q<VisualElement>("avg").Q<VisualElement>("detail-content").Q<Label>();
        subjectsListView = studentBox.Q<ScrollView>("term-subject-list");

        //Subject Box
        subjectlistView = subjectAnalyticsBox.Q<ScrollView>("subject-analytics-list");

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

        ISubject newSubject = new ISubject();

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
                newSubject.SubjectID = idResponse;
                newSubject.Name = nameResponse;
                newSubject.Grade = gradeResponse;

                sendRequests.GetArray(GlobalData.url + "/getsubjecttopics/" + idResponse, headers, label, (responseArray) => {
                    if (responseArray.Count != 0)
                    {
                        int count = 0;

                        newSubject.TopicList = new List<ITopic>();

                        foreach (JObject topicObject in responseArray)
                        {
                            ITopic newTopic = new ITopic();

                            newTopic.TopicID = topicObject["_id"].Value<string>();
                            newTopic.Name = topicObject["title"].Value<string>();
                            newTopic.SubtopicList = new List<IAttempt>();

                            foreach (IAttempt att in attemptList)
                            {
                                if (att.TopicName == newTopic.Name)
                                {
                                    newTopic.SubtopicList.Add(att);
                                }
                            }

                            newSubject.TopicList.Add(newTopic);

                            count++;

                            if (count >= responseArray.Count)
                            {
                                subjectDataList.Add(newSubject);

                                foreach (ISubject subj in subjectDataList)
                                {
                                    PopulateAttempts(subj);
                                }
                            }
                        }
                    }
                });

            });
        });
    }

    public void GetStudentResults(StudentData std)
    {
        attemptList.Clear();
        subjectList.Clear();
        topicList.Clear();
        subtopicList.Clear();

        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        Label label = new Label();
        //Get Attempts
        sendRequests.GetArray(GlobalData.url + "/getattempts/" + std.StdID, headers, label, (responseArray) => {
            if (responseArray.Count != 0)
            {
                int count = 0;

                foreach (JObject attemptObject in responseArray)
                {
                    IAttempt newAttempt = new IAttempt();
                    newAttempt.AttemptID = attemptObject["_id"].Value<string>();
                    newAttempt.SubtopicID = attemptObject["subtopic"].Value<string>();

                    newAttempt.GameData = new GameplayData();
                    newAttempt.GameData.StartDateTime = attemptObject["starttime"].Value<string>();
                    newAttempt.GameData.EndDateTime = attemptObject["endtime"].Value<string>();
                    newAttempt.GameData.Duration = attemptObject["duration"].Value<float>();
                    newAttempt.GameData.FinalScore = attemptObject["score"].Value<float>();
                    newAttempt.GameData.GameLevelData = JsonConvert.DeserializeObject<List<GameplayLevelData>>(attemptObject["levelscore"].Value<string>());

                    sendRequests.SendGetRequest(GlobalData.url + "/getsubtopic/" + newAttempt.SubtopicID, headers, label, (responseJson) =>
                    {
                        newAttempt.SubtopicName = responseJson["title"].Value<string>();

                        if (!subtopicList.Contains(responseJson["title"].Value<string>()))
                        {
                            subtopicList.Add(responseJson["title"].Value<string>());
                        }

                        sendRequests.SendGetRequest(GlobalData.url + "/gettopic/" + responseJson["topic"].Value<string>(), headers, label, (responseJson) =>
                        {
                            newAttempt.TopicName = responseJson["title"].Value<string>();

                            if (!topicList.Contains(responseJson["title"].Value<string>()))
                            {
                                topicList.Add(responseJson["title"].Value<string>());
                            }

                            sendRequests.SendGetRequest(GlobalData.url + "/getsubject/" + responseJson["subject"].Value<string>(), headers, label, (responseJson) =>
                            {
                                if (!subjectList.Contains(responseJson["name"].Value<string>()))
                                {
                                    subjectList.Add(responseJson["name"].Value<string>());
                                }

                                newAttempt.SubjectName = responseJson["name"].Value<string>();
                                attemptList.Add(newAttempt);
                                count++;
                                if (count >= responseArray.Count)
                                {
                                    float studentTotalScore = 0;

                                    foreach (string subj in subjectList)
                                    {
                                        List<IAttempt> curentAttepts = new List<IAttempt>();

                                        foreach (IAttempt attempt in attemptList)
                                        {
                                            curentAttepts.Add(attempt);
                                        }

                                        PopulateSubjectResults(subj, curentAttepts, ref studentTotalScore);
                                    }

                                    //Calculate Final
                                    subjectCountLabel.text = subjectList.Count.ToString();
                                    totalLabel.text = studentTotalScore.ToString();
                                    averageLabel.text = (studentTotalScore / subjectList.Count).ToString();

                                    //Get Subjects
                                    foreach (string subjIds in std.Subjects)
                                    {
                                        GetStudentSubjects(subjIds);
                                    }
                                }
                            });
                        });
                    });
                }
            }
        });
    }

    private void PopulateSubjectResults(string subjectName, List<IAttempt> currentAttempts, ref float totalScore)
    {
        float total = 0;
        float average = 0;

        //CALCULATE
        foreach (IAttempt currentAttempt in currentAttempts)
        {
            total += currentAttempt.GameData.FinalScore;
        }

        average = total / currentAttempts.Count;
        totalScore += total;

        subjectsListView.Clear();

        //CREATE
        VisualElement newSubjectElement = new VisualElement();

        VisualElement subjectNameElement = new VisualElement();
        Label subjectLabel = new Label();

        VisualElement totalElement = new VisualElement();
        Label totalLabel = new Label();

        VisualElement avgElement = new VisualElement();
        Label avgLabel = new Label();

        //ADD CLASSES
        newSubjectElement.AddToClassList("list-primary-item");

        subjectNameElement.AddToClassList("list-primary-item-text");
        subjectLabel.AddToClassList("student-item-label");

        totalElement.AddToClassList("list-primary-item-text");
        totalLabel.AddToClassList("student-item-label");

        avgElement.AddToClassList("list-primary-item-text");
        avgLabel.AddToClassList("student-item-label");

        //ADD VALUES
        subjectLabel.text = subjectName;
        totalLabel.text = total.ToString();
        avgLabel.text = average.ToString();

        //ADD ELEMENTS

        //Add Labels
        subjectNameElement.Add(subjectLabel);
        totalElement.Add(totalLabel);
        avgElement.Add(avgLabel);

        //Add to newSubjectElement
        newSubjectElement.Add(subjectNameElement);
        newSubjectElement.Add(totalElement);
        newSubjectElement.Add(avgElement);

        subjectsListView.Add(newSubjectElement);
    }

    private void PopulateAttempts(ISubject currentSubject)
    {
        subjectlistView.Clear();

        GroupBox subjectBox = new GroupBox();

        VisualElement newSubject = CreateListItem(currentSubject.Name);
        subjectBox.Add(newSubject);

        VisualElement subjectDetailsBox = new VisualElement();
        subjectDetailsBox.AddToClassList("subject-details-box");

        foreach (ITopic topic in currentSubject.TopicList)
        {
            VisualElement topicBox = new VisualElement();
            topicBox.AddToClassList("topics-box");

            VisualElement newTopic = CreateListItem(topic.Name);
            topicBox.Add(newTopic);

            foreach (IAttempt subtopic in topic.SubtopicList)
            {
                VisualElement subtopicBox = new VisualElement();
                subtopicBox.AddToClassList("topics-box");

                VisualElement newSubtopic = CreateListItem(subtopic.SubtopicName);
                VisualElement startTime = CreateListItem($"Start Date Time: {subtopic.GameData.StartDateTime}");
                VisualElement endTime = CreateListItem($"End Date Time: {subtopic.GameData.EndDateTime}");
                VisualElement finalduration = CreateListItem($"Duration: {subtopic.GameData.Duration}");
                VisualElement finalscore = CreateListItem($"Final Score: {subtopic.GameData.FinalScore}");

                subtopicBox.Add(newSubtopic);
                subtopicBox.Add(startTime);
                subtopicBox.Add(endTime);
                subtopicBox.Add(finalduration);
                subtopicBox.Add(finalscore);

                Debug.Log(subtopic.SubtopicName);

                foreach (GameplayLevelData gameLvlData in subtopic.GameData.GameLevelData)
                {
                    VisualElement levelsBox = new VisualElement();
                    levelsBox.AddToClassList("topics-box");

                    VisualElement levelIndex = CreateListItem($"Level {gameLvlData.LevelIndex + 1}");
                    VisualElement score = CreateListItem($"Score: {gameLvlData.Score}");
                    VisualElement duration = CreateListItem($"Duration: {gameLvlData.Duration} Sec");

                    levelsBox.Add(levelIndex);
                    levelsBox.Add(score);
                    levelsBox.Add(duration);

                    subtopicBox.Add(levelsBox);
                }

                topicBox.Add(subtopicBox);
            }

            subjectDetailsBox.Add(topicBox);
        }

        subjectBox.Add(subjectDetailsBox);

        subjectlistView.Add(subjectBox);
    }

    private VisualElement CreateListItem(string textData)
    {
        //Create
        VisualElement labelBox = new VisualElement();
        VisualElement detailLabelBox = new VisualElement();
        Label detailLabel = new Label();

        //ADD CLASSES
        labelBox.AddToClassList("list-primary-item");
        detailLabelBox.AddToClassList("list-primary-item-text");
        detailLabel.AddToClassList("student-item-label");

        //ADD VALUE
        detailLabel.text = textData;

        //ADD
        //Add to detailLabelBox
        detailLabelBox.Add(detailLabel);

        //Add to labelBox
        labelBox.Add(detailLabelBox);

        return labelBox;
    }
}

//Subject
public interface ISubjectContainer
{
    string SubjectID { get; set; }
    string Name { get; set; }
    string Grade { get; set; }
    List<ITopic> TopicList { get; set; }
}

public class ISubject : ISubjectContainer
{
    public string SubjectID { get; set; }
    public string Name { get; set; }
    public string Grade { get; set; }
    public List<ITopic> TopicList { get; set; }
}

public interface ITopicContainer
{
    string TopicID { get; set; }
    string Name { get; set; }
    List<IAttempt> SubtopicList { get; set; }
}

public class ITopic : ITopicContainer
{
    public string TopicID { get; set; }
    public string Name { get; set; }
    public List<IAttempt> SubtopicList { get; set; }
}

public interface IAttemptContainer
{
    string SubjectName { get; set; }
    string SubtopicName { get; set; }
    string TopicName { get; set; }
    string SubtopicID { get; set; }
    string AttemptID { get; set; }
    GameplayData GameData { get; set; }
}

public class IAttempt : IAttemptContainer
{
    public string SubtopicID { get; set; }
    public string SubjectName { get; set; }
    public string SubtopicName { get; set; }
    public string TopicName { get; set; }
    public string AttemptID { get; set; }
    public GameplayData GameData { get; set; }
}

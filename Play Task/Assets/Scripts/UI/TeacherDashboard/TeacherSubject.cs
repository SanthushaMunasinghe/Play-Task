using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TeacherSubject : TeacherDashboardSubjects
{
    private int subjectIndex = 0;

    //UI Elements
    private Label subjectLabel;
    private ScrollView subtopicView;

    void Start()
    {
        subjectLabel = subjectBox.Q<VisualElement>("subject-label").Q<Label>();

        var nextBtn = subjectBox.Q<Button>("subject-next-btn");
        var backBtn = subjectBox.Q<Button>("subject-back-btn");

        subtopicView = subjectBox.Q<ScrollView>("subject-list");

        foreach (string id in GlobalData.subjects)
        {
            GetSubjectData(id);
        }

        nextBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(true, ref subjectIndex, subjectList.Count);
            SelectClassroomStudents();
        };

        backBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(false, ref subjectIndex, subjectList.Count);
            SelectClassroomStudents();
        };
    }

    void Update()
    {
        if (subjectList.Count != 0)
        {
            Dictionary<string, string> currentSubject = subjectList[subjectIndex];
            subjectLabel.text = $"Grade {currentSubject["Grade"]} - {currentSubject["Name"]}";
        }
    }

    private void SelectClassroomStudents()
    {
        subtopicView.Clear();

        if (topicList.Count != 0)
        {
            for (int i = 0; i < topicList.Count; i++)
            {
                if (topicList[i]["Subject"] == subjectList[subjectIndex]["Id"])
                {
                    DisplayStudentList(subtopicView, topicList[i], i + 1);
                }
            }
        }
    }

    private void GetSubjectData(string id)
    {
        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        subjectLabel.text = "Please Wait...";

        string idResponse;
        string nameResponse;
        string gradeResponse;

        sendRequests.SendGetRequest(GlobalData.url + "/getsubject/" + id, headers, subjectLabel, (responseJson) => {

            //Get Classroom Name
            idResponse = responseJson["_id"].Value<string>();
            nameResponse = responseJson["name"].Value<string>();

            sendRequests.SendGetRequest(GlobalData.url + "/getgradebyid/" + responseJson["grade"], headers, subjectLabel, (responseJson) => {
                gradeResponse = responseJson["number"].Value<string>();

                Dictionary<string, string> currentClassroom = new Dictionary<string, string>();
                currentClassroom.Add("Id", idResponse);
                currentClassroom.Add("Name", nameResponse);
                currentClassroom.Add("Grade", gradeResponse);
                subjectList.Add(currentClassroom);

                if (subjectList.Count != 0)
                {
                    Dictionary<string, string> currentClasssroom = subjectList[0];
                    subjectLabel.text = $"Grade {currentClasssroom["Grade"]} - {currentClasssroom["Name"]}";
                }
                else
                {
                    subjectLabel.text = "No Subjects";
                }

                GetTopics(id);
            });

        });
    }

    private void GetTopics(string id)
    {
        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        Label label = new Label();

        sendRequests.GetArray(GlobalData.url + "/getsubjecttopics/" + id, headers, label, (responseArray) => {

            if (responseArray.Count != 0)
            {
                foreach (JObject topicObject in responseArray)
                {
                    Dictionary<string, string> currentTopic = new Dictionary<string, string>();
                    currentTopic.Add("Id", topicObject["_id"].Value<string>());
                    currentTopic.Add("Title", topicObject["title"].Value<string>());
                    currentTopic.Add("Subject", topicObject["subject"].Value<string>());

                    sendRequests.SendGetRequest(GlobalData.url + "/getterm/" + topicObject["term"].Value<string>(), headers, label, (responseJson) => {
                        currentTopic.Add("Term", responseJson["number"].Value<string>());

                        topicList.Add(currentTopic);
                    });
                }

                SelectClassroomStudents();
            }
        });
    }

    private void DisplayStudentList(ScrollView list, Dictionary<string, string> topicData, int number)
    {
        VisualElement newItem = new VisualElement();

        newItem.AddToClassList("list-primary-item");

        Label topicNo = new Label();
        Label topicTitle = new Label();
        Label topicTerm = new Label();

        topicNo.AddToClassList("list-primary-item-text");

        topicTitle.AddToClassList("list-primary-item-text");
        topicTerm.AddToClassList("list-primary-item-text");

        topicTitle.name = "topicTitle";

        topicNo.text = number.ToString() + ".";
        topicTitle.text = topicData["Title"];
        topicTerm.text = "Term " + topicData["Term"];

        newItem.Add(topicNo);
        newItem.Add(topicTitle);
        newItem.Add(topicTerm);

        list.Add(newItem);

        newItem.RegisterCallback<MouseUpEvent>(evt => {
            foreach (Dictionary<string, string> topic in topicList)
            {
                if (topic["Title"] == newItem.Q<Label>("topicTitle").text)
                {
                    GetComponent<TeacherSubtopic>().selectedTopicId = topic["Id"];
                    GetComponent<TeacherSubtopic>().selectedTopicTitle = topic["Title"];

                    //foreach (string subject in topic.Subjects)
                    //{
                    //    GetComponent<Student>().GetStudentSubjects(subject);
                    //}
                }
            }
        });
    }
}

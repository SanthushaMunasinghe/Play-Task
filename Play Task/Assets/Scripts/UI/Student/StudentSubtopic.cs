using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StudentSubtopic : StudentDashboardSubjects
{
    public string selectedTopicId;
    public string selectedTopicTitle;

    private int subtopicIndex;

    private List<SubtopicData> currentSubtopicList = new List<SubtopicData>();

    private Label subtopicLabel;
    private Label subtopicTitleLabel;
    private Label subtopicDescriptionLabel;

    //Buttons
    private Button playBtn;

    void Start()
    {
        subtopicLabel = subtopicBox.Q<VisualElement>("details-label").Q<Label>();
        subtopicTitleLabel = subtopicBox.Q<VisualElement>("subtopic-detail-title").Q<Label>();
        subtopicDescriptionLabel = subtopicBox.Q<VisualElement>("subtopic-description").Q<Label>();

        playBtn = subtopicBox.Q<VisualElement>("subtopics-buttons").Q<Button>("play-btn");

        var nextBtn = subtopicBox.Q<Button>("subtopic-next-btn");
        var backBtn = subtopicBox.Q<Button>("subtopic-back-btn");

        nextBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(true, ref subtopicIndex, currentSubtopicList.Count);
        };

        backBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(false, ref subtopicIndex, currentSubtopicList.Count);
        };
    }

    void Update()
    {
        if (currentSubtopicList.Count != 0)
        {
            subtopicLabel.text = $"{selectedTopicTitle} - {currentSubtopicList[subtopicIndex].Title}";
            subtopicTitleLabel.text = $"Title: {currentSubtopicList[subtopicIndex].Title}";
            subtopicDescriptionLabel.text = $"{currentSubtopicList[subtopicIndex].Description}";
        }
        else
        {
            subtopicLabel.text = selectedTopicTitle;
        }
    }

    public void GetSubtopics(string topicId)
    {
        currentSubtopicList.Clear();

        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        Label label = new Label();

        sendRequests.GetArray(GlobalData.url + "/getsubtopics/" + topicId, headers, label, (responseArray) => {


            if (responseArray.Count != 0)
            {
                foreach (JObject subtopicObject in responseArray)
                {
                    //Debug.Log(studentObject);
                    SubtopicData subtopic = new SubtopicData();
                    subtopic.SbtID = subtopicObject["_id"].Value<string>();
                    subtopic.Title = subtopicObject["title"].Value<string>();
                    subtopic.Description = subtopicObject["description"].Value<string>();
                    subtopic.Instructions = subtopicObject["instructions"].ToObject<string[]>();
                    currentSubtopicList.Add(subtopic);
                }
            }

            ProjectButtons(currentSubtopicList[subtopicIndex].SbtID);
        });
    }

    private void DisplayInstructionsList(ScrollView list, string instrcution, int number)
    {
        VisualElement newItem = new VisualElement();

        newItem.AddToClassList("list-primary-item");

        Label instructionData = new Label();

        instructionData.AddToClassList("list-primary-item-text");

        instructionData.text = $"{number}. {instrcution}";

        newItem.Add(instructionData);

        list.Add(newItem);
    }

    private void ProjectButtons(string subTID)
    {
        playBtn.RegisterCallback<MouseUpEvent>(evt => {
            // Define headers for the classroom request
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer <token>");

            Label label = new Label();

            sendRequests.SendGetRequest($"{GlobalData.url}/getgamestudent/{subTID}", headers, label, (responseJson) =>
            {
                GlobalData.projectID = responseJson["id"].Value<string>();
                GlobalData.projectData = responseJson["gamedata"].Value<string>();
                GlobalData.gameSubtopicID = responseJson["subtopic"].Value<string>();
                GlobalData.gameMode = "";
                
                GlobalMethods.LoadScene("Player");
            });
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TeacherSubtopic : TeacherDashboardSubjects
{
    public string selectedTopicId;
    public string selectedTopicTitle;

    private int subtopicIndex;

    private List<SubtopicData> currentSubtopicList = new List<SubtopicData>();

    private Label subtopicLabel;
    private Label subtopicTitleLabel;
    private Label subtopicDescriptionLabel;
    private ScrollView instructionList;

    void Start()
    {
        subtopicLabel = subtopicBox.Q<VisualElement>("details-label").Q<Label>();
        subtopicTitleLabel = subtopicBox.Q<VisualElement>("subtopic-detail-title").Q<Label>();
        subtopicDescriptionLabel = subtopicBox.Q<VisualElement>("subtopic-description").Q<Label>();

        var nextBtn = subtopicBox.Q<Button>("subtopic-next-btn");
        var backBtn = subtopicBox.Q<Button>("subtopic-back-btn");

        instructionList = subtopicBox.Q<ScrollView>("subtopic-instructions-list");

        nextBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(true, ref subtopicIndex, currentSubtopicList.Count);
            SelectInstructions();
        };

        backBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(false, ref subtopicIndex, currentSubtopicList.Count);
            SelectInstructions();
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

            SelectInstructions();
        });
    }

    private void SelectInstructions()
    {
        instructionList.Clear();

        if (currentSubtopicList[subtopicIndex].Instructions.Length != 0)
        {
            for (int i = 0; i < currentSubtopicList[subtopicIndex].Instructions.Length; i++)
            {
                DisplayInstructionsList(instructionList, currentSubtopicList[subtopicIndex].Instructions[i], i + 1);
            }
        }
    }

    private void DisplayInstructionsList(ScrollView list, string instrcution, int number)
    {
        VisualElement newItem = new VisualElement();

        newItem.AddToClassList("list-primary-item");

        Label instructionData = new Label();

        instructionData.AddToClassList("list-primary-item-text");

        instructionData.text = $"{number.ToString()}. {instrcution}";

        newItem.Add(instructionData);

        list.Add(newItem);
    }
}

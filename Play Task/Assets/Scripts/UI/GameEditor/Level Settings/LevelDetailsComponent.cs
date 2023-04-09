using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelDetailsComponent : MonoBehaviour
{
    //Get UI Elements
    //Parent
    public VisualElement templateDetails;

    // Default
    private Label typeLabel;
    private Label featureElementLabel;
    private Label featureLabel;
    private Label questionLabel;

    //Quiz
    private GroupBox quizGroup;

    //Puzzle
    private GroupBox dragAndDropPuzzleGroup;

    //Select Puzzle
    private GroupBox selectPuzzleGroup;
    private Label selectsCountlabel;
    private DropdownField selectsDropdownforTxt;
    private Label selectTextLabel;
    private DropdownField selectsDropdownforValue;
    private Label selectValueLabel;

    //Template Refs
    public GameObject levelGameObj;
    private Level level;
    private DragDropPuzzleTemplate DragDropPuzzleTemplate;
    private SelectPuzzleTemplate selectPuzzleTemplate;

    //Values
    //Default
    private string type;
    private string feature;
    private string question;

    //Quiz
    private string answerCount;
    private List<AnswerData> answerData;
    private List<AnswerData> answerValues;

    //Drag And Drop Puzzle
    public string slotsCount;
    public string matchesCount;
    public List<AnswerData> slotData;
    public List<AnswerData> matchData;
    public List<Dictionary<string, int>> slotMatches;

    //Select Puzzle
    public string selectsCount;
    public List<AnswerData> selectData;
    public List<AnswerData> selectValue;

    public void GetElements()
    {
        //Get Defaul Elements
        typeLabel = templateDetails.Q<VisualElement>("type").Q<VisualElement>("type-value").Q<Label>();
        featureElementLabel = templateDetails.Q<VisualElement>("type-type").Q<VisualElement>("type-type-label").Q<Label>();
        featureLabel = templateDetails.Q<VisualElement>("type-type").Q<VisualElement>("type-value").Q<Label>();
        questionLabel = templateDetails.Q<VisualElement>("question").Q<VisualElement>("question-value").Q<Label>();

        //Get Groups
        quizGroup = templateDetails.Q<GroupBox>("quiz");
        dragAndDropPuzzleGroup = templateDetails.Q<GroupBox>("drag-puzzle");
        selectPuzzleGroup = templateDetails.Q<GroupBox>("select-puzzle");

        //Set Default Values
        level = levelGameObj.GetComponent<Level>();

        type = level.levelType;
        feature = level.featureType;
        question = level.questionTxt;

        //Set Labels
        typeLabel.text = type;
        featureLabel.text = feature;
        questionLabel.text = question;

        if (type == "Quiz")
        {
            GetQuizElements();
            featureElementLabel.text = "Quiz Type";
        }
        else
        {
            if (feature == "Drag and Drop")
            {
                GetDragDropPuzzleElements();
                featureElementLabel.text = "Puzzle Type";
            }
            else if (feature == "Select")
            {
                GetSelectPuzzleElements();
                featureElementLabel.text = "Puzzle Type";
            }
        }
    }

    private void GetQuizElements()
    {
        quizGroup.style.display = DisplayStyle.Flex;
        QuizTemplate quizTemplate = level.GetTemplateObject().GetComponent<QuizTemplate>();

        //Get Elements
        Label answerCountlabel = quizGroup.Q<VisualElement>("answer-count").Q<VisualElement>("answer-count-value").Q<Label>();
        DropdownField answersDropdownforTxt = quizGroup.Q<VisualElement>("answers").Q<DropdownField>();
        Label answerTextLabel = quizGroup.Q<VisualElement>("answers").Q<VisualElement>("text").Q<Label>();
        DropdownField answersDropdownforValue = quizGroup.Q<VisualElement>("answers-type").Q<DropdownField>();
        Label answerValueLabel = quizGroup.Q<VisualElement>("answers-type").Q<VisualElement>("text").Q<Label>();

        //Get values
        answerCount = quizTemplate.answerCount.ToString();
        answerData = quizTemplate.answerData;
        answerValues = quizTemplate.answerValues;

        //Setup
        List<string> answerList = new List<string>();

        for (int i = 0; i < answerData.Count; i++)
        {
            answerList.Add(answerData[i].AnswerIndex.ToString());
        }

        answersDropdownforTxt.choices = answerList;
        answersDropdownforValue.choices = answerList;

        //Set Values
        answerCountlabel.text = answerCount;
        answersDropdownforTxt.value = answerList[0];
        answerTextLabel.text = answerData[0].AnswerTxt;
        answersDropdownforValue.value = answerList[0];
        answerValueLabel.text = answerValues[0].AnswerTxt;

        RegisterEvents(answersDropdownforTxt, answerTextLabel, answerData);
        RegisterEvents(answersDropdownforValue, answerValueLabel, answerValues);
    }

    private void GetDragDropPuzzleElements()
    {
        dragAndDropPuzzleGroup.style.display = DisplayStyle.Flex;
        DragDropPuzzleTemplate dragAndDropPuzzleTemplate = level.GetTemplateObject().GetComponent<DragDropPuzzleTemplate>();

        //Get Elements
        Label slotsCountlabel = dragAndDropPuzzleGroup.Q<VisualElement>("slots-count").Q<VisualElement>("slots-count-value").Q<Label>();
        Label matchesCountlabel = dragAndDropPuzzleGroup.Q<VisualElement>("match-count").Q<VisualElement>("matches-count-value").Q<Label>();
        DropdownField slotsDropdownforTxt = dragAndDropPuzzleGroup.Q<VisualElement>("slot-text").Q<DropdownField>();
        Label slotTextLabel = dragAndDropPuzzleGroup.Q<VisualElement>("slot-text").Q<VisualElement>("text").Q<Label>();
        DropdownField matchesDropdownforTxt = dragAndDropPuzzleGroup.Q<VisualElement>("match-text").Q<DropdownField>();
        Label matchTextLabel = dragAndDropPuzzleGroup.Q<VisualElement>("match-text").Q<VisualElement>("text").Q<Label>();
        DropdownField slotDropdown = dragAndDropPuzzleGroup.Q<VisualElement>("slot-matches").Q<DropdownField>();
        Label matchLabel = dragAndDropPuzzleGroup.Q<VisualElement>("slot-matches").Q<VisualElement>("text").Q<Label>();

        //Get values
        slotsCount = dragAndDropPuzzleTemplate.slotsCount.ToString();
        matchesCount = dragAndDropPuzzleTemplate.matchesCount.ToString();
        slotData = dragAndDropPuzzleTemplate.slotData;
        matchData = dragAndDropPuzzleTemplate.matchData;
        slotMatches = dragAndDropPuzzleTemplate.slotMatches;

        //Setup
        List<string> slotList = new List<string>();
        List<string> matchList = new List<string>();
        List<string> slotMatchesList = new List<string>();

        for (int i = 0; i < slotData.Count; i++)
        {
            slotList.Add(slotData[i].AnswerIndex.ToString());
        }
        
        for (int i = 0; i < matchData.Count; i++)
        {
            matchList.Add(matchData[i].AnswerIndex.ToString());
        }
        
        for (int i = 0; i < slotMatches.Count; i++)
        {
            slotMatchesList.Add((slotMatches[i]["Slot"] + 1).ToString());
        }

        slotsDropdownforTxt.choices = slotList;
        matchesDropdownforTxt.choices = matchList;
        slotDropdown.choices = slotMatchesList;

        //SetValues
        slotsCountlabel.text = slotsCount;
        matchesCountlabel.text = matchesCount;
        slotsDropdownforTxt.value = slotList[0];
        slotTextLabel.text = slotData[0].AnswerTxt;
        matchesDropdownforTxt.value = matchList[0];
        matchTextLabel.text = matchData[0].AnswerTxt;
        slotDropdown.value = slotMatchesList[0];
        matchLabel.text = (slotMatches[0]["Match"] + 1).ToString();

        //Events
        RegisterEvents(slotsDropdownforTxt, slotTextLabel, slotData);
        RegisterEvents(matchesDropdownforTxt, matchTextLabel, matchData);

        slotDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            matchLabel.text = (slotMatches[int.Parse(selectedValue) - 1]["Match"] + 1).ToString();
        });
    }
    
    private void GetSelectPuzzleElements()
    {
        selectPuzzleGroup.style.display = DisplayStyle.Flex;
        SelectPuzzleTemplate selectPuzzleTemplate = level.GetTemplateObject().GetComponent<SelectPuzzleTemplate>();

        //Get values
        selectsCount = selectPuzzleTemplate.selectsCount.ToString();
        selectData = selectPuzzleTemplate.selectData;
        selectValue = selectPuzzleTemplate.selectValue;
    }

    private void RegisterEvents(DropdownField dropdown, Label label, List<AnswerData> aData)
    {
        dropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            label.text = aData[int.Parse(selectedValue) - 1].AnswerTxt;
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSettings : MonoBehaviour
{
    public GameObject selectedLevelObj;
    private Level selectedLevel;

    [SerializeField] private QuizComponent quizComponent;
    [SerializeField] private DragAndDropPuzzleComponent dragAndDropPuzzleComponent;
    [SerializeField] private SelectPuzzleComponent selectPuzzleComponent;
    public LevelDetailsComponent levelDetailsComponent;

    //Value Lists
    [SerializeField] private List<string> templateTypes = new List<string>();
    [SerializeField] private List<string> quizTypes = new List<string>();
    [SerializeField] private List<string> puzzleTypes = new List<string>();

    //Quiz
    [SerializeField] private List<string> quizLogicTypes = new List<string>();

    //Values
    private string currentTemplateType;
    private string currentfeatureType;
    private string questionTxt;

    //Parent UI Elements
    public ScrollView templateSettingsListView;
    public VisualElement templateSettings;
    public VisualElement generateTemplate;

    //Default UI Elements
    private Label templateLabel;
    private DropdownField templateTypeDropdown;
    private DropdownField featureDropdown;
    private Label featureLabel;
    private TextField questionTextField;

    //Generate Template UI Elements
    private VisualElement errorListElement;
    private Button generateBtn;

    //Hidden UI Element Groups
    private GroupBox quizGroup;
    private GroupBox dragDropPuzzleGroup;
    private GroupBox selectPuzzleGroup;
    private List<GroupBox> featureGroups = new List<GroupBox>();

    public void GetElements()
    {
        selectedLevel = selectedLevelObj.GetComponent<Level>();

        if (selectedLevel.isCreated)
        {
            LevelDetailsSetup();
        }
        else
        {
            GetDefaultElements();
        }
    }

    private void GetDefaultElements()
    {
        templateSettings.style.display = DisplayStyle.Flex;
        generateTemplate.style.display = DisplayStyle.Flex;
        levelDetailsComponent.templateDetails.style.display = DisplayStyle.None;

        //Get Default Elements
        templateLabel = templateSettings.Q<VisualElement>("component-label").Q<Label>();
        templateTypeDropdown = templateSettings.Q<VisualElement>("type").Q<DropdownField>();
        featureDropdown = templateSettings.Q<VisualElement>("type-type").Q<DropdownField>();
        featureLabel = templateSettings.Q<VisualElement>("type-type").Q<VisualElement>("type-type-label").Q<Label>();
        questionTextField = templateSettings.Q<VisualElement>("question").Q<TextField>("q-text-field");

        //Get Generate Template Elements
        errorListElement = generateTemplate.Q<VisualElement>("template-generate-error");
        generateBtn = generateTemplate.Q<VisualElement>("generate").Q<Button>();

        //Get Hidden Groups
        quizGroup = templateSettings.Q<GroupBox>("quiz");
        dragDropPuzzleGroup = templateSettings.Q<GroupBox>("drag-puzzle");
        selectPuzzleGroup = templateSettings.Q<GroupBox>("select-puzzle");

        SetupDefault();
        SetupGenerate();
    }

    private void SetupDefault()
    {
        //Initial Values
        //Label
        templateLabel.text = "Template - Level " + (selectedLevel.levelIndex + 1);

        //Template Type
        templateTypeDropdown.choices = templateTypes;
        currentTemplateType = templateTypes[0];
        templateTypeDropdown.value = currentTemplateType;
        questionTextField.value = "";

        //Group Box List
        featureGroups.Add(quizGroup);
        featureGroups.Add(dragDropPuzzleGroup);
        featureGroups.Add(selectPuzzleGroup);

        RegisterDefaultEvents();
    }

    private void RegisterDefaultEvents()
    {
        templateTypeDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            SelectTemplateType(selectedValue);
        });
        
        featureDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            currentfeatureType = selectedValue;
            ActivateGroup(currentfeatureType);
        });

        questionTextField.RegisterCallback<BlurEvent>(evt =>
        {
            string typedValue = questionTextField.value;
            questionTxt = typedValue;
        });
    }

    private void SelectTemplateType(string typeName)
    {
        currentTemplateType = typeName;

        if (typeName == templateTypes[0])
        {
            featureDropdown.choices = quizTypes;
            currentfeatureType = quizTypes[0];
            featureDropdown.value = currentfeatureType;
            featureLabel.text = "Quiz Type";
        }
        else if (typeName == templateTypes[1])
        {
            featureDropdown.choices = puzzleTypes;
            currentfeatureType = puzzleTypes[0];
            featureDropdown.value = currentfeatureType;
            featureLabel.text = "Puzzle Type";
        }
    }

    private void ActivateGroup(string boxName)
    {
        foreach (GroupBox featureGroup in featureGroups)
        {
            featureGroup.style.display = DisplayStyle.None;
        }

        if (boxName == quizTypes[0])
        {
            quizGroup.style.display = DisplayStyle.Flex;
            quizComponent.Setup(quizGroup);
        }
        else if (boxName == puzzleTypes[0])
        {
            dragDropPuzzleGroup.style.display = DisplayStyle.Flex;
            dragAndDropPuzzleComponent.Setup(dragDropPuzzleGroup);
        }
        else if (boxName == puzzleTypes[1])
        {
            selectPuzzleGroup.style.display = DisplayStyle.Flex;
            selectPuzzleComponent.Setup(selectPuzzleGroup);
        }
    }

    //Generate Template
    private void SetupGenerate()
    {
        RegisterGenerateEvents();
    }

    private void RegisterGenerateEvents()
    {
        generateBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            if (!selectedLevel.isCreated)
            {
                if (currentTemplateType == templateTypes[0])
                {
                    SubmitQuizData();
                }
                else if (currentTemplateType == templateTypes[1])
                {
                    SelectPuzzleType();
                }

                selectedLevel.SaveDefaultValues(currentTemplateType, currentfeatureType, questionTxt);

                LevelDetailsSetup();
            }
        });
    }

    private void SubmitQuizData()
    {
        selectedLevel.SaveQuizValues(quizComponent.answerCount, quizComponent.answerData, quizComponent.answerValues);
    }

    private void SelectPuzzleType()
    {
        if (currentfeatureType == puzzleTypes[0])
        {
            SubmitDragAndDropPuzzleData();
        }
        else if (currentfeatureType == puzzleTypes[1])
        {
            SubmitSelectPuzzleData();
        }
    }

    private void SubmitDragAndDropPuzzleData()
    {
        selectedLevel.SaveDragDropPuzzleValues(dragAndDropPuzzleComponent.slotsCount, dragAndDropPuzzleComponent.matchesCount, dragAndDropPuzzleComponent.slotData, 
            dragAndDropPuzzleComponent.matchData, dragAndDropPuzzleComponent.slotMatches);
    }

    private void SubmitSelectPuzzleData()
    {
        selectedLevel.SaveSelectPuzzleValues(selectPuzzleComponent.selectsCount, selectPuzzleComponent.selectData, selectPuzzleComponent.selectValue);
    }

    //Display Details
    private void LevelDetailsSetup()
    {
        templateSettings.style.display = DisplayStyle.None;
        generateTemplate.style.display = DisplayStyle.None;
        levelDetailsComponent.templateDetails.style.display = DisplayStyle.Flex;
        levelDetailsComponent.levelGameObj = selectedLevelObj;
        levelDetailsComponent.GetElements();
    }
}

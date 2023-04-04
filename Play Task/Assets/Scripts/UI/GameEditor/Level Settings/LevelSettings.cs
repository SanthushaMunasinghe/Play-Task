using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSettings : MonoBehaviour
{
    public GameObject selectedLevelObj;
    private Level selectedLevel;

    //Value Lists
    [SerializeField] private List<string> templateTypes = new List<string>();
    [SerializeField] private List<string> quizTypes = new List<string>();
    [SerializeField] private List<string> puzzleTypes = new List<string>();

    //Quiz
    [SerializeField] protected List<string> quizLogicTypes = new List<string>();

    //Values
    private string currentTemplateType;
    private string currentfeatureType;

    //Parent UI Element
    public VisualElement templateSettings;

    //Default UI Elements
    private Label templateLabel;
    private DropdownField templateTypeDropdown;
    private DropdownField featureDropdown;
    private Label featureLabel;
    private TextField questionLabel;

    //Hidden UI Element Groups
    private GroupBox quizGroup;
    private GroupBox dragDropPuzzleGroup;
    private GroupBox selectPuzzleGroup;
    private List<GroupBox> featureGroups = new List<GroupBox>();

    public void GetElements()
    {
        //Get Default Elements
        templateLabel = templateSettings.Q<VisualElement>("component-label").Q<Label>();
        templateTypeDropdown = templateSettings.Q<VisualElement>("type").Q<DropdownField>();
        featureDropdown = templateSettings.Q<VisualElement>("type-type").Q<DropdownField>();
        featureLabel = templateSettings.Q<VisualElement>("type-type").Q<VisualElement>("type-type-label").Q<Label>();
        questionLabel = templateSettings.Q<VisualElement>("question").Q<TextField>("q-text-field");

        //Get Hidden Groups
        quizGroup = templateSettings.Q<GroupBox>("quiz");
        dragDropPuzzleGroup = templateSettings.Q<GroupBox>("drag-puzzle");
        selectPuzzleGroup = templateSettings.Q<GroupBox>("select-puzzle");

        SetupDefault();
    }

    private void SetupDefault()
    {
        //Initial Values
        selectedLevel = selectedLevelObj.GetComponent<Level>();

        //Label
        templateLabel.text = "Template - Level " + selectedLevel.levelIndex;

        //Template Type
        templateTypeDropdown.choices = templateTypes;
        currentTemplateType = templateTypes[0];
        templateTypeDropdown.value = currentTemplateType;

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
    }

    private void SelectTemplateType(string typeName)
    {
        if (typeName == templateTypes[1])
        {
            featureDropdown.choices = quizTypes;
            featureDropdown.value = quizTypes[0];
        }
        else if (typeName == templateTypes[2])
        {
            featureDropdown.choices = puzzleTypes;
            currentfeatureType = puzzleTypes[0];
            featureDropdown.value = currentfeatureType;
        }
        else
        {
            ResetTemplate();
        }
    }

    private void ActivateGroup(string boxName)
    {
        foreach (GroupBox featureGroup in featureGroups)
        {
            featureGroup.style.display = DisplayStyle.None;
        }

        if (boxName == quizTypes[1])
        {
            quizGroup.style.display = DisplayStyle.Flex;
        }
        else if (boxName == puzzleTypes[1])
        {
            dragDropPuzzleGroup.style.display = DisplayStyle.Flex;
        }
        else if (boxName == puzzleTypes[2])
        {
            selectPuzzleGroup.style.display = DisplayStyle.Flex;
        }
    }

    private void ResetTemplate()
    {
        foreach (GroupBox featureGroup in featureGroups)
        {
            featureGroup.style.display = DisplayStyle.None;
        }

        featureDropdown.choices = null;
        currentfeatureType = null;
        featureDropdown.value = null;
    }
}

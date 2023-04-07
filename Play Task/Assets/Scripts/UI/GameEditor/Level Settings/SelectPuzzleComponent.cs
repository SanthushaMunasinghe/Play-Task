using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectPuzzleComponent : MonoBehaviour
{
    //UI Elements
    private DropdownField matchesCountDropdown;
    private DropdownField matchDropdown;
    private TextField matchTextField;
    private DropdownField matchesSelectDropdown;
    private DropdownField matchValueDropdown;

    //Value Lists
    [SerializeField] private List<string> matchCountList = new List<string>();
    [SerializeField] private List<string> matchValues = new List<string>();

    private List<string> currentMatchList = new List<string>();

    //Values
    public int matchesCount;
    private string selectedMatchForTxt;
    private string selectedMatchForValue;
    public List<AnswerData> matchData = new List<AnswerData>();
    public List<AnswerData> matchValue = new List<AnswerData>();

    public void Setup(VisualElement parent)
    {
        //Get Elements
        matchesCountDropdown = parent.Q<VisualElement>("match-count").Q<DropdownField>();
        matchDropdown = parent.Q<VisualElement>("match-text").Q<DropdownField>();
        matchTextField = parent.Q<VisualElement>("match-text").Q<TextField>();
        matchesSelectDropdown = parent.Q<VisualElement>("match-value").Q<DropdownField>("match");
        matchValueDropdown = parent.Q<VisualElement>("match-value").Q<DropdownField>("value");

        //Set Choices
        matchesCountDropdown.choices = matchCountList;
        matchDropdown.choices = currentMatchList;
        matchesSelectDropdown.choices = currentMatchList;
        matchValueDropdown.choices = matchValues;

        ////Initial Values
        UpdateCounts(matchCountList[0]);

        //Events
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        matchesCountDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateCounts(selectedValue);
        });

        matchDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            selectedMatchForTxt = selectedValue;
            matchTextField.value = matchData[int.Parse(selectedValue) - 1].AnswerTxt;
        });

        matchTextField.RegisterCallback<BlurEvent>(evt =>
        {
            string typedValue = matchTextField.value;
            UpdateMatchData(typedValue);
        });

        matchesSelectDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            selectedMatchForValue = selectedValue;
            matchValueDropdown.value = matchValue[int.Parse(selectedValue) - 1].AnswerTxt;
        });
        
        matchValueDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateMatchValue(selectedValue);
        });
    }

    private void UpdateCounts(string mCount)
    {
        matchesCount = int.Parse(mCount);

        currentMatchList.Clear();
        matchValue.Clear();
        matchData.Clear();

        for (int i = 0; i < matchesCount; i++)
        {
            currentMatchList.Add((i + 1).ToString());
        }

        foreach (string count in currentMatchList)
        {
            AnswerData mData = new AnswerData();
            mData.AnswerIndex = int.Parse(count);
            mData.AnswerTxt = "";
            matchData.Add(mData);
        }

        foreach (string count in currentMatchList)
        {
            AnswerData mValue = new AnswerData();
            mValue.AnswerIndex = int.Parse(count);
            mValue.AnswerTxt = matchValues[0];
            matchValue.Add(mValue);
        }

        selectedMatchForTxt = matchData[0].AnswerIndex.ToString();
        selectedMatchForValue = matchValue[0].AnswerIndex.ToString();

        //Set Dropdown Values
        matchesCountDropdown.value = matchesCount.ToString();
        matchDropdown.value = selectedMatchForTxt;
        matchTextField.value = matchData[0].AnswerTxt;
        matchesSelectDropdown.value = selectedMatchForValue;
        matchValueDropdown.value = matchValues[0];
    }

    private void UpdateMatchData(string textValue)
    {
        foreach (AnswerData mData in matchData)
        {
            if (mData.AnswerIndex == int.Parse(selectedMatchForTxt))
            {
                mData.AnswerTxt = textValue;
            }
        }
    }
    
    private void UpdateMatchValue(string textValue)
    {
        foreach (AnswerData mValue in matchValue)
        {
            if (mValue.AnswerIndex == int.Parse(selectedMatchForValue))
            {
                mValue.AnswerTxt = textValue;
            }
        }
    }
}

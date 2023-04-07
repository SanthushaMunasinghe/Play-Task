using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropPuzzleComponent : MonoBehaviour
{
    //UI Elements
    private DropdownField slotsCountDropdown;
    private DropdownField matchesCountDropdown;
    private DropdownField slotDropdown;
    private TextField slotTextField;
    private DropdownField matchDropdown;
    private TextField matchTextField;
    private DropdownField slotsSelectDropdown;
    private DropdownField matchesSelectDropdown;

    //Value Lists
    [SerializeField] private List<string> slotCountList = new List<string>();
    [SerializeField] private List<string> matchCountList = new List<string>();

    private List<string> currentSlotList = new List<string>();
    private List<string> currentMatchList = new List<string>();

    //Values
    public int slotsCount;
    public int matchesCount;
    private string selectedSlotForTxt;
    private string selectedMatchForTxt;
    private string selectedSlotForMatch;
    private string selectedMatchForSlot;
    public List<AnswerData> slotData = new List<AnswerData>();
    public List<AnswerData> matchData = new List<AnswerData>();
    public List<Dictionary<string, int>> slotMatches = new List<Dictionary<string, int>>();

    public void Setup(VisualElement parent)
    {
        //Get Elements
        slotsCountDropdown = parent.Q<VisualElement>("slots-count").Q<DropdownField>();
        matchesCountDropdown = parent.Q<VisualElement>("match-count").Q<DropdownField>();
        slotDropdown = parent.Q<VisualElement>("slot-text").Q<DropdownField>();
        slotTextField = parent.Q<VisualElement>("slot-text").Q<TextField>();
        matchDropdown = parent.Q<VisualElement>("match-text").Q<DropdownField>();
        matchTextField = parent.Q<VisualElement>("match-text").Q<TextField>();
        slotsSelectDropdown = parent.Q<VisualElement>("slot-matches").Q<DropdownField>("slot");
        matchesSelectDropdown = parent.Q<VisualElement>("slot-matches").Q<DropdownField>("match");

        //Set Choices
        slotsCountDropdown.choices = slotCountList;
        matchesCountDropdown.choices = matchCountList;
        slotDropdown.choices = currentSlotList;
        matchDropdown.choices = currentMatchList;
        slotsSelectDropdown.choices = currentSlotList;
        matchesSelectDropdown.choices = currentMatchList;

        //Initial Values
        UpdateCounts(slotCountList[0], matchCountList[0]);

        //Events
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        slotsCountDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateCounts(selectedValue, matchesCount.ToString());
        });
        
        matchesCountDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateCounts(slotsCount.ToString(), selectedValue);
        });

        slotDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            selectedSlotForTxt = selectedValue;
            slotTextField.value = slotData[int.Parse(selectedValue) - 1].AnswerTxt;
        });

        slotTextField.RegisterCallback<BlurEvent>(evt =>
        {
            string typedValue = slotTextField.value;
            UpdateSlotData(typedValue);
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

        slotsSelectDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            selectedSlotForMatch = selectedValue;
            matchesSelectDropdown.value = (slotMatches[int.Parse(selectedSlotForMatch) - 1]["Match"] + 1).ToString();
        });

        matchesSelectDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateSlotMatch(selectedValue);
        });
    }

    private void UpdateCounts(string sCount, string mCount)
    {
        slotsCount = int.Parse(sCount);
        matchesCount = int.Parse(mCount);

        if (matchesCount < slotsCount)
        {
            foreach (string mC in matchCountList)
            {
                if (int.Parse(mC) >= slotsCount)
                {
                    matchesCount = int.Parse(mC);
                    break;
                }
            }
        }

        slotsCountDropdown.value = slotsCount.ToString();
        matchesCountDropdown.value = matchesCount.ToString();

        currentSlotList.Clear();
        currentMatchList.Clear();
        slotData.Clear();
        matchData.Clear();
        slotMatches.Clear();

        for (int i = 0; i < slotsCount; i++)
        {
            currentSlotList.Add((i + 1).ToString());
        }

        for (int i = 0; i < matchesCount; i++)
        {
            currentMatchList.Add((i + 1).ToString());
        }

        selectedSlotForTxt = currentSlotList[0];
        selectedMatchForTxt = currentMatchList[0];
        selectedSlotForMatch = currentSlotList[0];
        selectedMatchForSlot = currentMatchList[0];

        foreach (string count in currentSlotList)
        {
            AnswerData sData = new AnswerData();
            sData.AnswerIndex = int.Parse(count);
            sData.AnswerTxt = "";
            slotData.Add(sData);
        }

        foreach (string count in currentMatchList)
        {
            AnswerData mData = new AnswerData();
            mData.AnswerIndex = int.Parse(count);
            mData.AnswerTxt = "";
            matchData.Add(mData);
        }

        for (int i = 0; i < currentSlotList.Count; i++)
        {
            Dictionary<string, int> currentSlotMatch = new Dictionary<string, int>();
            currentSlotMatch.Add("Slot", i);
            currentSlotMatch.Add("Match", i);
            slotMatches.Add(currentSlotMatch);
        }

        selectedSlotForMatch = (slotMatches[0]["Slot"] + 1).ToString();
        selectedMatchForSlot = (slotMatches[0]["Match"] + 1).ToString();

        //Set Dropdown Values
        slotsCountDropdown.value = slotsCount.ToString();
        matchesCountDropdown.value = matchesCount.ToString();
        slotDropdown.value = selectedMatchForTxt;
        slotTextField.value = slotData[0].AnswerTxt;
        matchDropdown.value = selectedMatchForTxt;
        matchTextField.value = matchData[0].AnswerTxt;
        slotsSelectDropdown.value = selectedSlotForMatch;
        matchesSelectDropdown.value = selectedMatchForSlot;
    }

    private void UpdateSlotData(string textValue)
    {
        foreach (AnswerData sData in slotData)
        {
            if (sData.AnswerIndex == int.Parse(selectedSlotForTxt))
            {
                sData.AnswerTxt = textValue;
            }
        }
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

    private void UpdateSlotMatch(string matchValue)
    {
        foreach (Dictionary<string, int> sM in slotMatches)
        {
            if (sM["Slot"] == int.Parse(selectedSlotForMatch) - 1)
            {
                sM["Match"] = int.Parse(matchValue) - 1;
            }
        }
    }
}

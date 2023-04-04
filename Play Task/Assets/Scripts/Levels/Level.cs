using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelIndex;

    //Initial Lists
    public string levelType = "None";
    public string featureType = "None";
    public string questionTxt;

    //Quiz
    public int answerCount = 0;
    public string quizLogic = "";
    public List<AnswerData> answerData = new List<AnswerData>();
    public List<AnswerData> answerValues = new List<AnswerData>();

    //Drag and Drop Puzzle
    public int slotsCount = 0;
    public int matchesCount = 0;
    public List<AnswerData> matchData = new List<AnswerData>();
    public List<Dictionary<string, int>> slotMatches = new List<Dictionary<string, int>>();

    //Select Puzzle
    public int selectsCount = 0;
    public List<AnswerData> selectData = new List<AnswerData>();
    public List<AnswerData> selectValue = new List<AnswerData>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Set Default Data
    public void SetLevelType(string lvlType)
    {
        levelType = lvlType;
    }

    public void SetFeatureType(string ftType)
    {
        featureType = ftType;
    }

    public void SetQuestionText(string qTxt)
    {
        questionTxt = qTxt;
    }

    //Set Quiz Data
    public void SetAnswerCount(int count)
    {
        answerCount = count;
    }

    public void SetQuizLogic(string qLogic)
    {
        quizLogic = qLogic;
    }

    public void AddToAnswerDataList(AnswerData aData)
    {
        foreach (AnswerData data in answerData)
        {
            if (data.AnswerIndex == aData.AnswerIndex)
            {
                answerData.Remove(data);
            }

            answerData.Add(aData);
        }
    }

    public void AddToAnswerValuesList(AnswerData aValue)
    {
        foreach (AnswerData data in answerValues)
        {
            if (data.AnswerIndex == aValue.AnswerIndex)
            {
                answerValues.Remove(data);
            }

            answerValues.Add(aValue);
        }
    }

    //Set Drag and Drop Puzzle Data
    public void SetSlotsCount(int count)
    {
        slotsCount = count;
    }

    public void SetMatchesCount(int count)
    {
        matchesCount = count;
    }

    public void AddToMatchDataList(AnswerData mData)
    {
        foreach (AnswerData data in matchData)
        {
            if (data.AnswerIndex == mData.AnswerIndex)
            {
                matchData.Remove(data);
            }

            matchData.Add(mData);
        }
    }

    public void AddToSlotMatches (Dictionary<string, int> slotMatch)
    {
        foreach (Dictionary<string, int> sltMtch in slotMatches)
        {
            if (sltMtch["Slot"] == slotMatch["Slot"])
            {
                slotMatches.Remove(sltMtch);
            }

            slotMatches.Add(slotMatch);
        }
    }

    //Set SelectPuzzleData
    public void SetSelectsCount(int count)
    {
        selectsCount = count;
    }

    public void AddToSelectsDataList(AnswerData sData)
    {
        foreach (AnswerData data in selectData)
        {
            if (data.AnswerIndex == sData.AnswerIndex)
            {
                selectData.Remove(data);
            }

            selectData.Add(sData);
        }
    }

    public void AddToSelectsValuesList(AnswerData sValue)
    {
        foreach (AnswerData data in selectValue)
        {
            if (data.AnswerIndex == sValue.AnswerIndex)
            {
                selectValue.Remove(data);
            }

            selectValue.Add(sValue);
        }
    }
}

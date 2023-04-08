using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelIndex;

    //Objects
    [SerializeField] private GameObject slotObject;
    [SerializeField] private GameObject matchObject;

    //Initial Lists
    private string levelType;
    private string featureType;
    private string questionTxt;

    //Quiz
    private int answerCount = 0;
    private List<AnswerData> answerData;
    private List<AnswerData> answerValues;

    //Drag and Drop Puzzle
    private int slotsCount = 0;
    private int matchesCount = 0;
    private List<AnswerData> slotData;
    private List<AnswerData> matchData;
    private List<Dictionary<string, int>> slotMatches;

    //Select Puzzle
    private int selectsCount = 0;
    private List<AnswerData> selectData;
    private List<AnswerData> selectValue;

    public void SaveDefaultValues(string type, string fType, string qTxt = "")
    {
        levelType = type;
        featureType = fType;
        questionTxt = qTxt;

        Debug.Log("Level : " + levelType);
        Debug.Log("Question : " + questionTxt);
    }

    public void SaveQuizValues(int aCount, List<AnswerData> aData, List<AnswerData> aValues)
    {
        answerCount = aCount;
        answerData = aData;
        answerValues = aValues;

        Debug.Log("Answer Count : " + answerCount);
        Debug.Log("AnswerData : " + answerData.Count);
        Debug.Log("Answer Values : " + answerValues.Count);
    }
    
    public void SaveDragDropPuzzleValues(int sCount, int mCount, List<AnswerData> sData, List<AnswerData> mData, List<Dictionary<string, int>> sMatches)
    {
        slotsCount = sCount;
        matchesCount = mCount;
        slotData = sData;
        matchData = mData;
        slotMatches = sMatches;

        Debug.Log("Slots Count : " + slotsCount);
        Debug.Log("Matches Count : " + matchesCount);
        Debug.Log("SlotData : " + slotData.Count);
        Debug.Log("MatchData : " + matchData.Count);
        Debug.Log("Slot Matches : " + slotMatches.Count);
    }
    
    public void SaveSelectPuzzleValues(int sCount, List<AnswerData> sData, List<AnswerData> sValues)
    {
        selectsCount = sCount;
        selectData = sData;
        selectValue = sValues;

        Debug.Log("Selects Count : " + selectsCount);
        Debug.Log("SelectData : " + selectData);
        Debug.Log("SelectValue : " + selectValue);
    }
}

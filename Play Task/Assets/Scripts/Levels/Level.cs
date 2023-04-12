using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelIndex;
    public bool isCreated = false;

    private GameObject templateObject;

    //Template Objects
    [SerializeField] private GameObject quizTemplateObject;
    [SerializeField] private GameObject dragDropPuzzleTemplateObject;
    [SerializeField] private GameObject selectPuzzleTemplateObject;

    //Initial values
    public string levelType;
    public string featureType;
    public string questionTxt;

    public void SaveDefaultValues(string type, string fType, string qTxt = "")
    {
        levelType = type;
        featureType = fType;
        questionTxt = qTxt;
        isCreated = true;
    }

    public void SaveQuizValues(int aCount, List<AnswerData> aData, List<AnswerData> aValues)
    {
        templateObject = Instantiate(quizTemplateObject, Vector2.zero, Quaternion.identity);
        templateObject.transform.parent = transform;

        QuizTemplate currentTemplate = templateObject.GetComponent<QuizTemplate>();
        currentTemplate.answerCount = aCount;
        currentTemplate.answerData = aData;
        currentTemplate.answerValues = aValues;
    }
    
    public void SaveDragDropPuzzleValues(int sCount, int mCount, List<AnswerData> sData, List<AnswerData> mData, List<Dictionary<string, int>> sMatches)
    {
        templateObject = Instantiate(dragDropPuzzleTemplateObject, Vector2.zero, Quaternion.identity);
        templateObject.transform.parent = transform;

        DragDropPuzzleTemplate currentTemplate = templateObject.GetComponent<DragDropPuzzleTemplate>();

        currentTemplate.slotsCount = sCount;
        currentTemplate.matchesCount = mCount;
        currentTemplate.slotData = sData;
        currentTemplate.matchData = mData;
        currentTemplate.slotMatches = sMatches;
    }
    
    public void SaveSelectPuzzleValues(int sCount, List<AnswerData> sData, List<AnswerData> sValues)
    {
        templateObject = Instantiate(selectPuzzleTemplateObject, Vector2.zero, Quaternion.identity);
        templateObject.transform.parent = transform;

        SelectPuzzleTemplate currentTemplate = templateObject.GetComponent<SelectPuzzleTemplate>();

        currentTemplate.selectsCount = sCount;
        currentTemplate.selectData = sData;
        currentTemplate.selectValue = sValues;
    }

    public GameObject GetTemplateObject()
    {
        return templateObject;
    }

    public List<GameObject> GetTemplateObjectList()
    {
        if (featureType == "Drag and Drop")
        {
            return templateObject.GetComponent<DragDropPuzzleTemplate>().currentObjectsList;
        }
        else if (featureType == "Select")
        {
            return templateObject.GetComponent<SelectPuzzleTemplate>().currentObjectsList;
        }
        else
        {
            return null;
        }
    }
}

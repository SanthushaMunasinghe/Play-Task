using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGamePlay : MonoBehaviour
{
    //Initial
    public GameInfoTab gameInfoTab;

    public List<AnswerData> answerData;
    public List<AnswerData> answerValues;

    void Start()
    {
        for (int i = 0; i < answerData.Count; i++)
        {
            gameInfoTab.UpdateAnswerList(answerData[i].AnswerIndex, answerData[i].AnswerTxt, answerValues[i].AnswerTxt);
        }
    }

    void Update()
    {
        
    }
}

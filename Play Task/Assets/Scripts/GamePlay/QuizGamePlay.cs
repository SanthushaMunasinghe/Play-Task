using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGamePlay : MonoBehaviour
{
    //Initial
    public GameInfoTab gameInfoTab;
    public GamePlayLevel gamePlayLevel;

    public List<AnswerData> answerData;
    public List<AnswerData> answerValues;

    public void StartQuiz()
    {
        gameInfoTab.answerElement.Clear();

        for (int i = 0; i < answerData.Count; i++)
        {
            gameInfoTab.UpdateAnswerList(answerData[i].AnswerIndex, answerData[i].AnswerTxt, answerValues[i].AnswerTxt);
        }
    }

    public void ClickAnswer(int indexValue)
    {

    }
}

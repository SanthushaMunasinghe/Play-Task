using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayLevel : MonoBehaviour
{
    //Initial 
    public GamePlayLevelManager gamePlayLevelManager;
    public ILevelData thisLevelData;

    public float startTime;
    public float endTime;
    public int score;

    //Default
    public int levelIndex;
    private string levelType;
    private string featureType;
    private string questionTxt;

    public void StartLevel()
    {
        //Set Default
        levelIndex = thisLevelData.LevelIndex;
        levelType = thisLevelData.LevelType;
        featureType = thisLevelData.FeatureType;
        questionTxt = thisLevelData.QuestionTxt;

        if (levelType == "Quiz")
        {
            gamePlayLevelManager.gameInfoTab.UpdateInfo(questionTxt, thisLevelData.AnswerData, thisLevelData.AnswerValues, true);
            CreateQuizLevel();
        }
        else
        {
            gamePlayLevelManager.gameInfoTab.UpdateInfo(questionTxt, null, null, false);

            if (featureType == "Drag and Drop")
            {
                CreateDragDropLevel();
            }
            else
            {
                CreateSelectLevel();
            }
        }

        gamePlayLevelManager.gameDisplay.UpdateLevelText(levelIndex, gamePlayLevelManager.generatedLevelObjs.Count);
    }

    private void CreateQuizLevel()
    {
        Debug.Log("Quiz");
    }
    
    private void CreateDragDropLevel()
    {
        Debug.Log("DD");
    }
    
    private void CreateSelectLevel()
    {
        Debug.Log("Select");
    }

}

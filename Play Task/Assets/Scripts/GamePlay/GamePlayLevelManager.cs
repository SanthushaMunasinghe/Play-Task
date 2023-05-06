using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayLevelManager : MonoBehaviour
{
    //Initial
    [SerializeField] private GameDataHandler gameDataHandler;
    public GameDisplay gameDisplay;
    public GameInfoTab gameInfoTab;

    public List<Sprite> assetSpritesList = new List<Sprite>();

    [SerializeField] private GameObject lvlGameObject;
    public GameObject gamePlayLvlObjPrefab;
    public GameObject gamePlayTextObject;

    //Quiz
    public GameObject quizPrefab;

    //Select
    public GameObject selectPrefab;
    public GameObject objectPointer;

    public GameObject dragDropPrefab;

    //Running
    public List<GameObject> generatedLevelObjs = new List<GameObject>();
    private int currentlvlIndex = 0;

    private float startTime = 0;
    private float endTime = 0;
    public float duration = 0;

    public float gameScore = 0;
    public float finalScore = 0;

    public string startDateTime;
    public string endDateTime;

    public List<GameplayLevelData> gameplayLevelDataList = new List<GameplayLevelData>();

    void Start()
    {
        gameDisplay.GetElements();
        gameInfoTab.GetElements();

        gameDataHandler.GetGameData();

        startTime = Time.time;
        startDateTime = DateTime.Now.ToString();

        GenerateLevels(gameDataHandler.currentLevels);
    }

    public void GenerateLevels(List<ILevelData> currentLvlList)
    {
        foreach (ILevelData currentLvl in currentLvlList)
        {
            GameObject lvlClone = Instantiate(lvlGameObject, transform.position, Quaternion.identity);
            lvlClone.transform.parent = transform;

            //Set Default Values
            GamePlayLevel gamePlayLevel = lvlClone.GetComponent<GamePlayLevel>();

            gamePlayLevel.thisLevelData = currentLvl;

            gamePlayLevel.gamePlayLevelManager = gameObject.GetComponent<GamePlayLevelManager>();

            gamePlayLevel.levelIndex = currentLvl.LevelIndex;
            lvlClone.name = "Level" + gamePlayLevel.levelIndex;

            generatedLevelObjs.Add(lvlClone);
        }

        generatedLevelObjs[currentlvlIndex].GetComponent<GamePlayLevel>().StartLevel();
    }

    public void UpdateLevel()
    {
        generatedLevelObjs[currentlvlIndex].SetActive(false);
        currentlvlIndex++;

        if (currentlvlIndex < generatedLevelObjs.Count)
        {
            generatedLevelObjs[currentlvlIndex].GetComponent<GamePlayLevel>().StartLevel();
        }
        else
        {
            //Calculate Gameplay Data
            endTime = Time.time;
            duration = endTime - startTime;

            endDateTime = DateTime.Now.ToString();

            finalScore = (gameScore / generatedLevelObjs.Count) * 100;

            //Assign Current Gameplay Data
            gameDataHandler.gameplayData.StartDateTime = startDateTime;
            gameDataHandler.gameplayData.EndDateTime = endDateTime;
            gameDataHandler.gameplayData.Duration = duration;
            gameDataHandler.gameplayData.FinalScore = finalScore;
            gameDataHandler.gameplayData.GameLevelData = gameplayLevelDataList;

            gameDataHandler.SetGameplayData();
        }

        gameDisplay.nextLvlBtn.style.display = DisplayStyle.None;
    }
}

public interface IGameplayData
{
    string StartDateTime { get; set; }
    string EndDateTime { get; set; }
    float Duration { get; set; }
    float FinalScore { get; set; }

    List<GameplayLevelData> GameLevelData { get; set; }
}

public class GameplayData : IGameplayData
{
    public string StartDateTime { get; set; }
    public string EndDateTime { get; set; }
    public float Duration { get; set; }
    public float FinalScore { get; set; }

    public List<GameplayLevelData> GameLevelData { get; set; }
}


public interface IGameplayLevelData
{
    int LevelIndex { get; set; }
    float Score { get; set; }
    float Duration { get; set; }
}

public class GameplayLevelData : IGameplayLevelData
{
    public int LevelIndex { get; set; }
    public float Score { get; set; }
    public float Duration { get; set; }
}

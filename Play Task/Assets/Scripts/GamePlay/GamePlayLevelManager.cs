using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameInfoTab.answerElement.Clear();
        currentlvlIndex++;

        if (currentlvlIndex < generatedLevelObjs.Count)
        {
            generatedLevelObjs[currentlvlIndex].GetComponent<GamePlayLevel>().StartLevel();
        }
        else
        {
            endTime = Time.time;
            duration = endTime - startTime;

            endDateTime = DateTime.Now.ToString();

            finalScore = (gameScore / generatedLevelObjs.Count) * 100;

            Debug.Log($"Score: {finalScore}%");
        }
    }
}

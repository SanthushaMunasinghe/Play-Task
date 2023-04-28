using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayLevelManager : MonoBehaviour
{
    //Initial
    [SerializeField] private GameDataHandler gameDataHandler;
    public GameDisplay gameDisplay;
    public GameInfoTab gameInfoTab;

    [SerializeField] private GameObject lvlGameObject;

    //Running
    public List<GameObject> generatedLevelObjs = new List<GameObject>();
    private int currentlvlIndex = 0;

    void Start()
    {
        gameDisplay.GetElements();
        gameInfoTab.GetElements();

        gameDataHandler.GetGameData();

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

            lvlClone.name = "Level" + gamePlayLevel.levelIndex;

            generatedLevelObjs.Add(lvlClone);
        }

        generatedLevelObjs[currentlvlIndex].GetComponent<GamePlayLevel>().StartLevel();
    }

    public void UpdateLevel()
    {
        currentlvlIndex++;
        generatedLevelObjs[currentlvlIndex].GetComponent<GamePlayLevel>().StartLevel();
    }
}

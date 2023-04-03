using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListManager : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefab;

    public List<GameObject> lvlObjectList = new List<GameObject>();
    public int levelsCount = 0;

    void Start()
    {
        CreateLevel();
    }

    void Update()
    {
        
    }

    public void CreateLevel()
    {
        levelsCount++;
        GameObject Level = Instantiate(levelPrefab, Vector2.zero, Quaternion.identity);
        Level.transform.parent = this.transform;
        SetLevelIndex(Level, levelsCount - 1);
        lvlObjectList.Add(Level);
    }

    public void DeleteLevel(GameObject level)
    {
        levelsCount--;

        foreach (GameObject obj in lvlObjectList)
        {
            int levelIndex = obj.GetComponent<Level>().levelIndex - 1;

            if (levelIndex == level.GetComponent<Level>().levelIndex)
            {
                obj.GetComponent<Level>().levelIndex -= 1;
            }
        }

        DestroyImmediate(level);
        lvlObjectList.Remove(level);
    }

    public void SetLevelIndex(GameObject lvlObject, int lvlIndex)
    {
        lvlObject.GetComponent<Level>().levelIndex = lvlIndex;
        lvlObject.name = "Level " + (lvlIndex + 1);
    }

    public void ChangeLevel(GameObject lvlObj, int currentValue, int newValue)
    {
        foreach (GameObject obj in lvlObjectList)
        {
            if (obj.GetComponent<Level>().levelIndex == newValue - 1)
            {
                obj.GetComponent<Level>().levelIndex = currentValue;
            }
        }

        lvlObj.GetComponent<Level>().levelIndex = newValue - 1;
    }
}

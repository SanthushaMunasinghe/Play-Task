using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropPuzzleTemplate : MonoBehaviour
{
    public int slotsCount = 0;
    public int matchesCount = 0;
    public List<AnswerData> slotData;
    public List<AnswerData> matchData;
    public List<Dictionary<string, int>> slotMatches;

    [SerializeField] private GameObject slotObject;
    [SerializeField] private GameObject matchObject;

    public List<GameObject> currentObjectsList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            Vector2 spawnPos = new Vector2(i - 7, 1);
            GenerateObj(slotObject, "Slot-" + (i + 1), spawnPos);
        }

        for (int i = 0; i < matchesCount; i++)
        {
            Vector2 spawnPos = new Vector2(i - 7, -1);
            GenerateObj(matchObject, "Match-" + (i + 1), spawnPos);
        }

    }

    private void GenerateObj(GameObject obj, string name, Vector2 pos)
    {
        GameObject objClone = Instantiate(obj, Vector2.zero, Quaternion.identity);
        objClone.name = name;
        objClone.transform.parent = transform.parent;
        objClone.GetComponent<ObjectTransform>().UpdatePosition(pos.x, pos.y);
        currentObjectsList.Add(objClone);
    }
}

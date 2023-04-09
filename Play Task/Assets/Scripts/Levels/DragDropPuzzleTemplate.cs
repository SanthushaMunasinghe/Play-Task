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

    private void Start()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            GenerateObj(slotObject, "Slot-" + (i + 1));
        }

        for (int i = 0; i < matchesCount; i++)
        {
            GenerateObj(matchObject, "Match-" + (i + 1));
        }
    }

    private void GenerateObj(GameObject obj, string name)
    {
        GameObject objClone = Instantiate(obj, Vector2.zero, Quaternion.identity);
        objClone.name = name;
        objClone.transform.parent = transform.parent;
    }
}

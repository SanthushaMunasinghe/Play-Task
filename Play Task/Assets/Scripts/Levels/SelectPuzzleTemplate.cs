using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPuzzleTemplate : MonoBehaviour
{
    public int selectsCount = 0;
    public List<AnswerData> selectData;
    public List<AnswerData> selectValue;

    [SerializeField] private GameObject selectsObject;

    public List<GameObject> currentObjectsList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < selectsCount; i++)
        {
            Vector2 spawnPos = new Vector2(i - 7, 0);
            GenerateObj(selectsObject, "Select-" + (i + 1), spawnPos);
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

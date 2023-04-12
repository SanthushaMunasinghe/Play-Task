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
            GenerateObj(selectsObject, "Select-" + (i + 1));
        }
    }

    private void GenerateObj(GameObject obj, string name)
    {
        GameObject objClone = Instantiate(obj, Vector2.zero, Quaternion.identity);
        objClone.name = name;
        objClone.transform.parent = transform.parent;
        currentObjectsList.Add(objClone);
    }
}

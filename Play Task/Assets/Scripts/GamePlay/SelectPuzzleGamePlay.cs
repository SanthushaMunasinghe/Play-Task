using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPuzzleGamePlay : MonoBehaviour
{
    //Initial
    public GamePlayLevel gamePlayLevel;
    private List<GameObject> templateObjList = new List<GameObject>();
    public List<ILevelObjectData> templateObjects;

    private int selectedObjectIndex = 0;

    public void StartPuzzle()
    {
        foreach (ILevelObjectData tempObj in templateObjects)
        {
            gamePlayLevel.CreateGamePlayeLevelObject(tempObj, ref templateObjList);
        }

        foreach (GameObject tempObj in templateObjList)
        {
            GameObject pointerClone = Instantiate(gamePlayLevel.gamePlayLevelManager.objectPointer, tempObj.transform.position, Quaternion.identity);
            pointerClone.transform.parent = tempObj.transform;
            pointerClone.SetActive(false);
        }
    }

    private void SelectObject(int index)
    {
        if (templateObjList.Count != 0)
        {
            for (int i = 0; i < templateObjList.Count; i++)
            {
                GameObject selectedChild = templateObjList[i].transform.GetChild(0).gameObject;

                if (selectedChild != null)
                {
                    if (i == index)
                    {
                        selectedChild.SetActive(true);
                    }
                    else
                    {
                        selectedChild.SetActive(false);
                    }
                }
            }
        }
    }

    private void SwitchObject()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectedObjectIndex < templateObjList.Count - 1)
            {
                selectedObjectIndex++;
            }
            else
            {
                selectedObjectIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedObjectIndex > 0)
            {
                selectedObjectIndex--;
            }
            else
            {
                selectedObjectIndex = templateObjList.Count - 1;
            }
        }

        SelectObject(selectedObjectIndex);
    }

    private void ConfirmSelect()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gamePlayLevel.ConditionTrigger(selectedObjectIndex);
            if (gamePlayLevel.thisLevelData.SelectValue[selectedObjectIndex].AnswerTxt == "Correct")
            {
                gamePlayLevel.levelScore = 1;
            }
            else
            {
                gamePlayLevel.levelScore = 0;
            }
            gamePlayLevel.EndLevel();
        }
    }

    private void Update()
    {
        SwitchObject();
        ConfirmSelect();
    }
}

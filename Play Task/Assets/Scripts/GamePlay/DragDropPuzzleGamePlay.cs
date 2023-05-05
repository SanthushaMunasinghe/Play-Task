using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropPuzzleGamePlay : MonoBehaviour
{
    //Initial
    public GamePlayLevel gamePlayLevel;
    private List<GameObject> templateObjList = new List<GameObject>();
    public List<ILevelObjectData> templateObjects;

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
}

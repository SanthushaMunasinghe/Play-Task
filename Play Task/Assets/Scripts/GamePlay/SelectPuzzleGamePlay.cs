using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPuzzleGamePlay : MonoBehaviour
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
    }
}

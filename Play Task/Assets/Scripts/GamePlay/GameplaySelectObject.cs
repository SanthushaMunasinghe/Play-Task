using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySelectObject : MonoBehaviour
{
    public void RegisterSelectEvent(GamePlayLevel gpLvl, string value)
    {
        if (value == "Correct")
        {
            gpLvl.levelScore = 1;
        }
        else
        {
            gpLvl.levelScore = 0;
        }
        gpLvl.EndLevel();
    }
}

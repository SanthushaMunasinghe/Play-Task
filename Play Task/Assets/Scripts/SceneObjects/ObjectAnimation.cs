using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : LevelObject
{
    // Update
    public void UpdateType(string type)
    {
        animationType = type;
    }
    
    public void UpdateDuration(float dura)
    {
        duration = dura;
    }

    public void UpdateStartVector(float x, float y)
    {
        startVec = new Vector2(x, y);
    }
    
    public void UpdateEndVector(float x, float y)
    {
        endVec = new Vector2(x, y);
    }

    public void UpdatePlay(bool isTrue)
    {
        isPlay = isTrue;
        playInRun = isTrue;
    }
    
    public void UpdateLoop(bool isTrue)
    {
        isLoop = isTrue;
    }
    
    public void UpdatePlayInRun(bool isTrue)
    {
        playInRun = isTrue;
    }

    //Get
    public string GetAnimationType()
    {
        return animationType;
    }

    public float GetDuration()
    {
        return duration;
    }
    public Vector2 GetStartVector()
    {
        return startVec;
    }

    public Vector2 GetEndVector()
    {
        return endVec;
    }

    public bool GetIsPlay()
    {
        return isPlay;
    }
    
    public bool GetIsLoop()
    {
        return isLoop;
    }
    
    public bool GetPlayInRun()
    {
        return playInRun;
    }
}

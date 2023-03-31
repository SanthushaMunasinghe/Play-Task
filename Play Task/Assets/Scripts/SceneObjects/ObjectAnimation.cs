using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : LevelObject
{
    // Update
    public void UpdateType(string type)
    {
        animationType = type;
        SetAnimationType(animationType);
    }
    
    public void UpdateDuration(float dura)
    {
        duration = dura;
        SetDuration(duration);
    }

    public void UpdateStartVector(float x, float y)
    {
        startVec = new Vector2(x, y);
        SetStartVector(startVec);
    }
    
    public void UpdateEndVector(float x, float y)
    {
        endVec = new Vector2(x, y);
        SetEndVector(endVec);
    }

    public void UpdatePlay(bool isTrue)
    {
        isPlay = isTrue;
        SetPlay(isPlay);
    }
    
    public void UpdateLoop(bool isTrue)
    {
        isLoop = isTrue;
        SetLoop(isLoop);
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
}

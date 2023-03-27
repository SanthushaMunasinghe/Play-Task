using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransform : LevelObject
{
    // Update
    public void UpdatePosition(float x, float y)
    {
        position = new Vector2(x, y);
        SetPosition(position);
    }
    
    public void UpdateScale(float x, float y)
    {
        scale = new Vector2(x, y);
        SetScale(scale);
    }
    
    public void UpdateRotation(float rotZ)
    {
        rotation = rotZ;
        SetRotation(rotation);
    }

    //Get
    public Vector2 GetPosition()
    {
        return position;
    }
    
    public Vector2 GetScale()
    {
        return scale;
    }

    public float GetRotation()
    {
        return rotation;
    }
}

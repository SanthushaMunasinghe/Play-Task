using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysics : LevelObject
{
    // Update
    public void UpdatePhysicsPositionX(bool isTrue)
    {
        freezPositionX = isTrue;
        PhysicsPositionX(freezPositionX);
    }

    public void UpdatePhysicsPositionY(bool isTrue)
    {
        freezPositionY = isTrue;
        PhysicsPositionY(freezPositionY);
    }

    public void UpdatePhysicsRotation(bool isTrue)
    {
        freezRotation = isTrue;
        PhysicsRotation(freezRotation);
    }

    //Get
    public bool GetPhysicsPositionX()
    {
        return freezPositionX;
    }
    
    public bool GetPhysicsPositionY()
    {
        return freezPositionY;
    }

    public bool GetPhysicsRotation()
    {
        return freezRotation;
    }

    public bool GetPhysicsGravity()
    {
        return gravity;
    }
}

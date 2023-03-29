using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysics : LevelObject
{
    // Update
    public void UpdatePhysicsPositionX(bool isTrue)
    {
        freezPosX = isTrue;
    }

    public void UpdatePhysicsPositionY(bool isTrue)
    {
        freezPosY = isTrue;
    }

    public void UpdatePhysicsRotation(bool isTrue)
    {
        freezRot = isTrue;
    }
    
    public void UpdateCollision(bool isTrue)
    {
        collision = isTrue;
    }
    
    public void UpdatePhysicsGravity(bool isTrue)
    {
        gravity = isTrue;
    }

    //Get
    public bool GetPhysicsPositionX()
    {
        return freezPosX;
    }
    
    public bool GetPhysicsPositionY()
    {
        return freezPosY;
    }

    public bool GetPhysicsRotation()
    {
        return freezRot;
    }
    
    public bool GetCollision()
    {
        return collision;
    }

    public bool GetPhysicsGravity()
    {
        return gravity;
    }
}

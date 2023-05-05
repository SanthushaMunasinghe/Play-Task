using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayLevelObject : MonoBehaviour
{
    public ILevelObjectData levelObjectData;

    //Physics Trigger Data
    public string physicsType;
    public float durationInRun;
    public float forceVectorX;
    public float forceVectorY;

    private float currentDuration = 0;
    bool isImpulse = false;

    //Animation Trigger Data
    public bool playInRun;

    public void SetPhysicsTrigger()
    {
        if (physicsType == "Force")
        {
            currentDuration = durationInRun;
        }
        else if (physicsType == "Impulse")
        {
            isImpulse = true;
        }
    }

    void FixedUpdate()
    {
        if (currentDuration > 0)
        {
            Vector2 forceVec = new Vector2(forceVectorX, forceVectorY);
            gameObject.GetComponent<Rigidbody2D>().AddForce(forceVec * 10);
            currentDuration -= Time.deltaTime;
        }

        if (isImpulse)
        {
            Vector2 forceVec = new Vector2(forceVectorX, forceVectorY);
            gameObject.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
            isImpulse = false;
        }
    }
}

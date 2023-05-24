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
            Debug.Log("True");
        }
        else if (physicsType == "Impulse")
        {
            isImpulse = true;
        }

        if (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (currentDuration > 0)
        {
            Vector2 forceVec = new Vector2(forceVectorX, forceVectorY);
            gameObject.GetComponent<Rigidbody2D>().AddForce(forceVec * 10);
            Debug.Log("Force");
        }

        if (isImpulse)
        {
            Vector2 forceVec = new Vector2(forceVectorX, forceVectorY);
            gameObject.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
            isImpulse = false;
        }
    }
}

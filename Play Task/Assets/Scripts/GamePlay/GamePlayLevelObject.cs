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

    //Animation Trigger Data
    public bool playInRun;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

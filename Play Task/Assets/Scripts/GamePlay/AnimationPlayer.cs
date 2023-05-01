using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public string animationType;
    public float duration;
    public float startVecX;
    public float startVecY;
    public float endVecX;
    public float endVecY;
    public bool isPlay;
    public bool isLoop;

    private float currentTime = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPlay)
        {
            if (currentTime < duration)
            {
                PlayAnimation();
            }
            else if (isLoop)
            {
                currentTime = 0.0f;
            }
        }
    }

    private void PlayAnimation()
    {
        currentTime += Time.deltaTime;
        Vector3 currentVec = Vector3.Lerp(new Vector2(startVecX, startVecY), new Vector2(endVecX, endVecY), currentTime / duration);

        if (animationType == "Position")
        {
            transform.position = currentVec;
        }
        else if (animationType == "Scale")
        {
            transform.localScale = currentVec;
        }
    }
}

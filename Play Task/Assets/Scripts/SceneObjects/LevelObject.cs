using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    //Transform
    private Transform objectTF;
    protected Vector2 position;
    protected Vector2 scale;
    protected float rotation;

    //Sprite
    private SpriteRenderer objectSprite;
    protected Sprite sprite;
    protected Color color;
    protected float opcity;

    void Awake()
    {
        InitialTransform();
        InitialSpriteRenderer();
    }

    //Initial Values
    private void InitialTransform()
    {
        objectTF = gameObject.transform;

        position = Vector2.zero;
        scale = new Vector2(1, 1);
        rotation = 0;

        SetPosition(position);
        SetScale(scale);
        SetRotation(rotation);
    }

    private void InitialSpriteRenderer()
    {
        objectSprite = gameObject.GetComponent<SpriteRenderer>();
        sprite = objectSprite.sprite;
        color = objectSprite.color;
        opcity = 1;
    }

    //Set Transform
    protected void SetPosition(Vector2 pos)
    {
        objectTF.position = pos;
    }
    
    protected void SetScale(Vector2 sca)
    {
        objectTF.localScale = sca;
    }

    protected void SetRotation(float rotZ)
    {
        objectTF.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    //Set SpriteRenderer
    protected void SetSprite(Sprite spr)
    {
        objectSprite.sprite = spr;
    }
    
    protected void SetColor(Color clr)
    {
        objectSprite.color = clr;
    }
    
    protected void SetOpcaity(float alpha)
    {
        Color clr = objectSprite.color;
        clr.a = alpha;

        objectSprite.color = clr;
    }
}

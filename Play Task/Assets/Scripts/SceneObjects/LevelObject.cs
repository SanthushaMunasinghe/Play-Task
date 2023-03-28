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
    protected float opacity;

    //Collider
    private PolygonCollider2D polygonCollider2D;

    //Rigidbody
    private Rigidbody2D rb;
    private bool freezPositionX = true;
    private bool freezPositionY = true;
    private bool freezRotation = true;
    private float gravityX;
    private float gravityY;

    void Awake()
    {
        InitialTransform();
        InitialSpriteRenderer();
        InitialPhysics();
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
    private void InitialSpriteRenderer()
    {
        objectSprite = gameObject.GetComponent<SpriteRenderer>();
        sprite = objectSprite.sprite;
        color = objectSprite.color;
        opacity = 1;

        SetColor(color);
        SetOpcaity(opacity);
    }

    protected void SetSprite(Sprite spr)
    {
        objectSprite.sprite = spr;
        DestroyImmediate(polygonCollider2D);
        polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
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

    //Set Rigidbody
    private void InitialPhysics()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        freezPositionX = true;
        freezPositionY = true;
        freezRotation = true;
        gravityX = 0;
        gravityY = 9.8f;

        SetPositionX(freezPositionX);
        SetPositionY(freezPositionY);
        SetRotation(freezRotation);
    }

    private void SetPositionX(bool isTrue)
    {
        if (isTrue)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
        }
    }
    
    private void SetPositionY(bool isTrue)
    {
        if (isTrue)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void SetRotation(bool isTrue)
    {
        if (isTrue)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void SetGravity(float forceValue)
    {
        if (forceValue > 0)
        {

        }
    }
}

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
    protected bool freezPosX = true;
    protected bool freezPosY = true;
    protected bool freezRot = true;
    protected bool collision = true;
    protected bool gravity = false;

    //Animation
    protected string animationType;
    protected float duration;
    protected Vector2 startVec;
    protected Vector2 endVec;
    protected bool isPlay;
    protected bool isLoop;

    protected bool playInRun;

    void Awake()
    {
        InitialTransform();
        InitialSpriteRenderer();
        InitialPhysics();
        InitialAnimation();
    }

    //Initial Values
    private void InitialTransform()
    {
        objectTF = gameObject.transform;

        position = Vector2.zero;
        scale = new Vector2(0.25f, 0.25f);
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

        freezPosX = true;
        freezPosY = true;
        freezRot = true;
        collision = true;
        gravity = false;
    }

    protected void PhysicsPositionX(bool isTrue)
    {
        if (isTrue)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
    }
    
    protected void PhysicsPositionY(bool isTrue)
    {
        if (isTrue)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    protected void PhysicsRotation(bool isTrue)
    {
        if (isTrue)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }
    }

    protected void SetCollision(bool isTrue)
    {
        if (isTrue)
        {
            polygonCollider2D.isTrigger = false;
        }
        else
        {
            polygonCollider2D.isTrigger = true;
        }
    }

    protected void PhysicsGravity(bool isTrue)
    {
        if (isTrue)
        {
            rb.gravityScale = 1;
        }
        else
        {
            rb.gravityScale = 0;
        }
    }

    //Set Animation
    private void InitialAnimation()
    {
        duration = 0;
        startVec = objectTF.position;
        endVec = objectTF.position;
        isPlay = false;
        isLoop = false;

        playInRun = false;
    }
}

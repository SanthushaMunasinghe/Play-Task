using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    //Text
    [SerializeField] private GameObject textObj;
    private GameObject spawnedTextObject;
    private TextMeshPro textComponent;
    protected bool enableTxt;
    protected string textValue;
    protected Color textColor;
    protected bool isBold;
    protected float fontSize;


    //Physics
    protected bool freezPosX;
    protected bool freezPosY;
    protected bool freezRot;
    protected bool collision;
    protected bool gravity;

    //Runtime Physics
    protected string physicsType;
    protected float durationInRun;
    protected Vector2 forceVector;

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
        InitialText();
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

    //Set Text
    private void InitialText()
    {
        enableTxt = false;
        textValue = "";
        textColor = Color.black;
        isBold = false;
        fontSize = 5.0f;
    }

    protected void SetEnableText(bool isTrue)
    {
        if (isTrue)
        {
            spawnedTextObject = Instantiate(textObj, transform.position, Quaternion.identity);
            textComponent = spawnedTextObject.GetComponent<TextMeshPro>();
        }
        else if (spawnedTextObject != null)
        {
            DestroyImmediate(spawnedTextObject);
        }
    }

    protected void SetTextValue(string value)
    {
        textComponent.text = value;
    }
    
    protected void SetTextColor(Color col)
    {
        textComponent.color = col;
    }
    
    protected void SetBold(bool isTrue)
    {
        if (isTrue)
        {
            textComponent.fontStyle = FontStyles.Bold;
        }
        else
        {
            textComponent.fontStyle = FontStyles.Normal;
        }
    }

    protected void SetFontSize(float size)
    {
        textComponent.fontSize = size;
    }

    //Set Rigidbody
    private void InitialPhysics()
    {
        freezPosX = true;
        freezPosY = true;
        freezRot = true;
        collision = true;
        gravity = false;

        physicsType = "";
        durationInRun = 0;
        forceVector = Vector2.zero;
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

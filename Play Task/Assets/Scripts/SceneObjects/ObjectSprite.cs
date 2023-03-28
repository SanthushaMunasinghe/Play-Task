using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSprite : LevelObject
{
    // Update
    public void UpdateSprite(Sprite spr)
    {
        sprite = spr;
        SetSprite(spr);
    }

    public void UpdateColor(Color clr)
    {
        color = clr;
        SetColor(color);
    }
    
    public void UpdateOpacity(float opa)
    {
        opacity = opa * 0.01f;
        SetOpcaity(opacity);
    }

    //Get
    public Sprite GetSprite()
    {
        return sprite;
    }

    public Color GetColor()
    {
        return color;
    }

    public float GetOpacity()
    {
        return opacity;
    }
}

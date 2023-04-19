using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : LevelObject
{
    //Update
    public void UpdateEnableText(bool isTrue)
    {
        enableTxt = isTrue;
        SetEnableText(enableTxt);
    }

    public void UpdateTextScale(float x, float y)
    {
        textScale = new Vector2(x, y);
        SetTextScale(textScale);
    }

    public void UpdateTextValue(string value)
    {
        textValue = value;
        SetTextValue(textValue);
    }

    public void UpdateTextColor(Color col)
    {
        textColor = col;
        SetTextColor(textColor);
    }
    
    public void UpdateTextBold(bool IsTrue)
    {
        isBold = IsTrue;
        SetBold(isBold);
    }

    public void UpdateTFontSize(float size)
    {
        fontSize = size;
        SetFontSize(size);
    }

    //Get
    public bool GetEnableText()
    {
        return enableTxt;
    }

    public Vector2 GetTextScale()
    {
        return textScale;
    }

    public string GetTextValue()
    {
        return textValue;
    }

    public Color GetTextColor()
    {
        return textColor;
    }

    public bool GetIsBold()
    {
        return isBold;
    }

    public float GetFontSize()
    {
        return fontSize;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayLevel : MonoBehaviour
{
    //Initial 
    public GamePlayLevelManager gamePlayLevelManager;
    public ILevelData thisLevelData;

    public float startTime;
    public float endTime;
    public int score;

    //Default
    public int levelIndex;
    private string levelType;
    private string featureType;
    private string questionTxt;

    public void StartLevel()
    {
        //Set Default
        levelIndex = thisLevelData.LevelIndex;
        levelType = thisLevelData.LevelType;
        featureType = thisLevelData.FeatureType;
        questionTxt = thisLevelData.QuestionTxt;

        //Create Template
        if (levelType == "Quiz")
        {
            gamePlayLevelManager.gameInfoTab.UpdateInfo(questionTxt, thisLevelData.AnswerData, thisLevelData.AnswerValues, true);
            CreateQuizLevel();
        }
        else
        {
            gamePlayLevelManager.gameInfoTab.UpdateInfo(questionTxt, null, null, false);

            if (featureType == "Drag and Drop")
            {
                CreateDragDropLevel();
            }
            else
            {
                CreateSelectLevel();
            }
        }

        gamePlayLevelManager.gameDisplay.UpdateLevelText(levelIndex, gamePlayLevelManager.generatedLevelObjs.Count);

        //Create GameplayLevelObjects
        foreach (ILevelObjectData objData in thisLevelData.LevelObjects)
        {
            CreateGamePlayeLevelObject(objData);
        }
    }

    private void CreateQuizLevel()
    {
        Debug.Log("Quiz");
    }
    
    private void CreateDragDropLevel()
    {
        Debug.Log("DD");
    }
    
    private void CreateSelectLevel()
    {
        Debug.Log("Select");
    }

    public void CreateGamePlayeLevelObject(ILevelObjectData lvlObjData)
    {
        //CREATE and Set Transform
        GameObject lvlObjClone = Instantiate(gamePlayLevelManager.gamePlayLvlObjPrefab, 
            Vector2.zero, 
            Quaternion.Euler(0, 0, lvlObjData.Rotation));

        lvlObjClone.name = lvlObjData.ObjectName;
        lvlObjClone.transform.parent = transform;

        lvlObjClone.transform.position = new Vector2(lvlObjData.PositionX, lvlObjData.PositionY);

        lvlObjClone.transform.localScale = new Vector2(lvlObjData.ScaleX, lvlObjData.ScaleY);

        //Set Sprite
        Sprite selectedSprite = null;

        foreach (Sprite sprite in gamePlayLevelManager.assetSpritesList)
        {
            if (sprite.name == lvlObjData.Sprite)
            {
                selectedSprite = sprite;
                break;
            }
        }

        if (selectedSprite != null)
        {
            lvlObjClone.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }

        Color imgColor;
        if (ColorUtility.TryParseHtmlString(lvlObjData.ImageColor, out imgColor))
        {
            lvlObjClone.GetComponent<SpriteRenderer>().color = imgColor;
            Debug.Log(imgColor);
        }

        //Set Text
        if (lvlObjData.EnableTxt)
        {
            GameObject spawnedTextObject = Instantiate(gamePlayLevelManager.gamePlayTextObject, transform.position, Quaternion.identity);
            spawnedTextObject.transform.SetParent(lvlObjClone.transform, false);
            TextMeshPro textComponent = spawnedTextObject.GetComponent<TextMeshPro>();

            spawnedTextObject.GetComponent<RectTransform>().sizeDelta = new Vector2(lvlObjData.TextScaleX, lvlObjData.TextScaleY);

            textComponent.text = lvlObjData.TextValue;

            Color txtColor;
            if (ColorUtility.TryParseHtmlString(lvlObjData.TextColor, out txtColor))
            {
                textComponent.color = txtColor;
            }

            if (lvlObjData.IsBold)
            {
                textComponent.fontStyle = FontStyles.Bold;
            }
            else
            {
                textComponent.fontStyle = FontStyles.Normal;
            }

            textComponent.fontSize = lvlObjData.FontSize;
        }


        //Set Physics
        Rigidbody2D rbComponent = lvlObjClone.GetComponent<Rigidbody2D>();

        PolygonCollider2D objCollider = lvlObjClone.AddComponent<PolygonCollider2D>();

        if (lvlObjData.FreezPosX)
        {
            rbComponent.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
        
        if (lvlObjData.FreezPosY)
        {
            rbComponent.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        if (lvlObjData.FreezRot)
        {
            rbComponent.constraints |= RigidbodyConstraints2D.FreezeRotation;
        }

        if (lvlObjData.Collision)
        {
            objCollider.isTrigger = false;
        }

        if (lvlObjData.Gravity)
        {
            rbComponent.gravityScale = 1;
        }
    }
}

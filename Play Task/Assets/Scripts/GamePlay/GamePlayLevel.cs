using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayLevel : MonoBehaviour
{
    //Initial 
    public GamePlayLevelManager gamePlayLevelManager;
    public ILevelData thisLevelData;

    public List<GameObject> gameLvlObjList = new List<GameObject>();

    public float levelScore;

    //Default
    public int levelIndex;
    private string levelType;
    private string featureType;
    private string questionTxt;

    public List<IAnimationData> animDataList;
    public List<IPhysicsData> phyDataList;

    public void StartLevel()
    {
        //Set Default
        levelType = thisLevelData.LevelType;
        featureType = thisLevelData.FeatureType;
        questionTxt = thisLevelData.QuestionTxt;

        animDataList = thisLevelData.AnimationTriggerList;
        phyDataList = thisLevelData.PhysicsTriggerList;

        //Create GameplayLevelObjects
        foreach (ILevelObjectData objData in thisLevelData.LevelObjects)
        {
            CreateGamePlayeLevelObject(objData, ref gameLvlObjList);
        }

        //Create Template
        if (levelType == "Quiz")
        {
            gamePlayLevelManager.gameInfoTab.UpdateQuestion(questionTxt);
            CreateQuizLevel();
        }
        else
        {
            gamePlayLevelManager.gameInfoTab.UpdateQuestion(questionTxt);

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
    }

    private void CreateQuizLevel()
    {
        GameObject quizObj = Instantiate(gamePlayLevelManager.quizPrefab, transform.position, Quaternion.identity);
        quizObj.transform.parent = transform;

        QuizGamePlay quizGamePlay = quizObj.GetComponent<QuizGamePlay>();

        quizGamePlay.gamePlayLevel = GetComponent<GamePlayLevel>();

        quizGamePlay.gameInfoTab = gamePlayLevelManager.gameInfoTab;
        quizGamePlay.answerData = thisLevelData.AnswerData;
        quizGamePlay.answerValues = thisLevelData.AnswerValues;

        quizGamePlay.StartQuiz();
    }
    
    private void CreateDragDropLevel()
    {
        Debug.Log("DD");
    }
    
    private void CreateSelectLevel()
    {
        GameObject selectObj = Instantiate(gamePlayLevelManager.selectPrefab, transform.position, Quaternion.identity);
        selectObj.transform.parent = transform;

        SelectPuzzleGamePlay selectGamePlay = selectObj.GetComponent<SelectPuzzleGamePlay>();

        selectGamePlay.gamePlayLevel = GetComponent<GamePlayLevel>();
        selectGamePlay.templateObjects = thisLevelData.TemplateObjects;

        selectGamePlay.StartPuzzle();
    }

    public void CreateGamePlayeLevelObject(ILevelObjectData lvlObjData, ref List<GameObject> objList)
    {
        //CREATE and Set Rotation
        GameObject lvlObjClone = Instantiate(gamePlayLevelManager.gamePlayLvlObjPrefab, 
            Vector2.zero, 
            Quaternion.Euler(0, 0, lvlObjData.Rotation));

        //Set Name and Parent
        lvlObjClone.name = lvlObjData.ObjectName;
        lvlObjClone.transform.parent = transform;
        
        //Set Transform
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
            imgColor.a = lvlObjData.Opacity;
            lvlObjClone.GetComponent<SpriteRenderer>().color = imgColor;
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

        lvlObjClone.AddComponent<PolygonCollider2D>();

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

        if (!lvlObjData.Collision)
        {
            lvlObjClone.GetComponent<PolygonCollider2D>().isTrigger = true;
        }

        if (lvlObjData.Gravity)
        {
            rbComponent.gravityScale = 1;
        }

        //Set Animation
        AnimationPlayer animPlayer = lvlObjClone.GetComponent<AnimationPlayer>();

        animPlayer.animationType = lvlObjData.AnimationType;
        animPlayer.duration = lvlObjData.Duration;
        animPlayer.startVecX = lvlObjData.StartVecX;
        animPlayer.startVecY = lvlObjData.StartVecY;
        animPlayer.endVecX = lvlObjData.EndVecX;
        animPlayer.endVecY = lvlObjData.EndVecY;
        animPlayer.isPlay = lvlObjData.IsPlay;
        animPlayer.isLoop = lvlObjData.IsLoop;

        //SET TRIGGERS

        //Physics Trigger
        GamePlayLevelObject gamePlayLevelObject = lvlObjClone.GetComponent<GamePlayLevelObject>();

        gamePlayLevelObject.physicsType = lvlObjData.PhysicsType;
        gamePlayLevelObject.durationInRun = lvlObjData.DurationInRun;
        gamePlayLevelObject.forceVectorX = lvlObjData.ForceVectorX;
        gamePlayLevelObject.forceVectorY = lvlObjData.ForceVectorY;

        //Animation Trigger
        gamePlayLevelObject.playInRun = lvlObjData.playInRun;

        objList.Add(lvlObjClone);
    }

    public void ConditionTrigger(int indexValue)
    {
        //Set Physics
        foreach (IPhysicsData data in phyDataList)
        {
            if (data.ConditionIndex == indexValue)
            {
                foreach (GameObject obj in gameLvlObjList)
                {
                    if (obj.name == data.PhysicsObject)
                    {
                        obj.GetComponent<GamePlayLevelObject>().SetPhysicsTrigger();
                    }
                }
            }
        }

        //Set Animation
        foreach (IAnimationData data in animDataList)
        {
            if (data.ConditionIndex == indexValue)
            {
                foreach (GameObject obj in gameLvlObjList)
                {
                    if (obj.name == data.AnimationObject)
                    {
                        obj.GetComponent<AnimationPlayer>().isPlay = obj.GetComponent<GamePlayLevelObject>().playInRun;
                    }
                }
            }
        }
    }

    public void EndLevel()
    {
        gamePlayLevelManager.gameScore += levelScore;

        gamePlayLevelManager.UpdateLevel();
    }
}

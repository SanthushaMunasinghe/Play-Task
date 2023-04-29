using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectDataManager : MonoBehaviour
{
    [SerializeField] private SendRequests sendPostRequest;

    [SerializeField] private LevelListManager levelListManager;

    private string gameDataString = "";
    private string projectID = "";

    void Start()
    {
        projectID = GlobalData.projectID;
        gameDataString = GlobalData.projectData;
    }

    public void SaveData()
    {
        // Define headers, and payload for the request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");
        gameDataString = CreateGameDataString();
        string payload = $"{{\"gamedata\":{JsonConvert.SerializeObject(gameDataString)}}}";

        Label label = new Label();

        GlobalMethods.DisplayMessage(label, "Please Wait...");
        sendPostRequest.SendPostPutRequest(GlobalData.url + "/updategame/" + projectID, GlobalData.methodPut, headers, payload, label, (responseJson) =>
        {
            Debug.Log(responseJson["success"].Value<string>());
        });
    }

    private string CreateGameDataString()
    {
        List<ILevelData> projDataList = PrepareProjectData();
        string json = JsonConvert.SerializeObject(projDataList, Formatting.Indented);
        return json;
    }

    private List<ILevelData> PrepareProjectData()
    {
        List<ILevelData> projectLevels = new List<ILevelData>();

        foreach (GameObject lvlObj in levelListManager.lvlObjectList)
        {
            if (lvlObj.GetComponent<Level>().isCreated)
            {
                projectLevels.Add(GetLevelData(lvlObj.GetComponent<Level>()));
            }
        }

        return projectLevels;
    }

    private ILevelData GetLevelData(Level lvl)
    {
        ILevelData levelData = new ILevelData();

        //Get Default
        levelData.LevelIndex = lvl.levelIndex;
        levelData.LevelType = lvl.levelType;
        levelData.FeatureType = lvl.featureType;
        levelData.QuestionTxt = lvl.questionTxt;

        if (levelData.FeatureType == "Drag and Drop")
        {
            DragDropPuzzleTemplate dragDropPuzzleTemplate = lvl.templateObject.GetComponent<DragDropPuzzleTemplate>();

            levelData.SlotsCount = dragDropPuzzleTemplate.slotsCount;
            levelData.MatchesCount = dragDropPuzzleTemplate.matchesCount;
            levelData.SlotData = dragDropPuzzleTemplate.slotData;
            levelData.MatchData = dragDropPuzzleTemplate.matchData;
            levelData.SlotMatches = dragDropPuzzleTemplate.slotMatches;

            List<ILevelObjectData> templateObjs = new List<ILevelObjectData>();

            foreach (GameObject tempObj in lvl.GetTemplateObjectList())
            {
                templateObjs.Add(NewLevelObject(tempObj));
            }

            levelData.TemplateObjects = templateObjs;

            List<ILevelObjectData> levelObjs = new List<ILevelObjectData>();

            foreach (GameObject tempObj in lvl.levelObjectList)
            {
                levelObjs.Add(NewLevelObject(tempObj));
            }

            levelData.LevelObjects = levelObjs;
        }
        else if (levelData.FeatureType == "Select")
        {
            SelectPuzzleTemplate selectPuzzleTemplate = lvl.templateObject.GetComponent<SelectPuzzleTemplate>();

            levelData.SelectsCount = selectPuzzleTemplate.selectsCount;
            levelData.SelectData = selectPuzzleTemplate.selectData;
            levelData.SelectValue = selectPuzzleTemplate.selectValue;

            List<ILevelObjectData> templateObjs = new List<ILevelObjectData>();

            foreach (GameObject tempObj in lvl.GetTemplateObjectList())
            {
                templateObjs.Add(NewLevelObject(tempObj));
            }

            levelData.TemplateObjects = templateObjs;

            List<ILevelObjectData> levelObjs = new List<ILevelObjectData>();

            foreach (GameObject tempObj in lvl.levelObjectList)
            {
                levelObjs.Add(NewLevelObject(tempObj));
            }

            levelData.LevelObjects = levelObjs;
        }
        else
        {
            QuizTemplate quizTemplate = lvl.templateObject.GetComponent<QuizTemplate>();

            levelData.AnswerCount = quizTemplate.answerCount;
            levelData.AnswerData = quizTemplate.answerData;
            levelData.AnswerValues = quizTemplate.answerValues;

            List<ILevelObjectData> levelObjs = new List<ILevelObjectData>();

            foreach (GameObject tempObj in lvl.levelObjectList)
            {
                levelObjs.Add(NewLevelObject(tempObj));
            }

            levelData.LevelObjects = levelObjs;
        }

        //Get Conditions
        levelData.LevelConditiontList = lvl.levelConditiontList;

        //Get Animation Triggers
        List<IAnimationData> animationList = new List<IAnimationData>();

        foreach (AnimationTriggerData aniData in lvl.levelAnimationList)
        {
            IAnimationData anim = new IAnimationData();
            anim.ConditionIndex = aniData.ConditionIndex;
            anim.AnimationObject = aniData.AnimationObject.name;

            animationList.Add(anim);
        }

        levelData.AnimationTriggerList = animationList;

        //Get Physics Triggers
        List<IPhysicsData> physicsList = new List<IPhysicsData>();

        foreach (PhysicsTriggerData phyData in lvl.levelPhysicsList)
        {
            IPhysicsData phy = new IPhysicsData();
            phy.ConditionIndex = phyData.ConditionIndex;
            phy.PhysicsObject = phyData.PhysicsObject.name;

            physicsList.Add(phy);
        }

        levelData.PhysicsTriggerList = physicsList;

        return levelData;
    }

    private ILevelObjectData NewLevelObject(GameObject levelObject)
    {
        ILevelObjectData newLvlObj = new ILevelObjectData();


        newLvlObj.ObjectName = levelObject.name;

        //Get Transform Data
        ObjectTransform objectTransform = levelObject.GetComponent<ObjectTransform>();
        newLvlObj.PositionX = objectTransform.GetPosition().x;
        newLvlObj.PositionY = objectTransform.GetPosition().y;
        newLvlObj.ScaleX = objectTransform.GetScale().x;
        newLvlObj.ScaleY = objectTransform.GetScale().y;
        newLvlObj.Rotation = objectTransform.GetRotation();

        //Get Image Data
        ObjectSprite objectSprite = levelObject.GetComponent<ObjectSprite>();
        newLvlObj.Sprite = objectSprite.GetSprite().name;
        newLvlObj.ImageColor = "#" + ColorUtility.ToHtmlStringRGBA(objectSprite.GetColor());
        newLvlObj.Opacity = objectSprite.GetOpacity();

        //Get Text Data
        ObjectText objectText = levelObject.GetComponent<ObjectText>();
        newLvlObj.EnableTxt = objectText.GetEnableText();
        newLvlObj.TextScaleX = objectText.GetTextScale().x;
        newLvlObj.TextScaleY = objectText.GetTextScale().y;
        newLvlObj.TextValue = objectText.GetTextValue();
        newLvlObj.TextColor = "#" + ColorUtility.ToHtmlStringRGBA(objectText.GetTextColor());
        newLvlObj.IsBold = objectText.GetIsBold();
        newLvlObj.FontSize = objectText.GetFontSize();

        //Get Physocs Data
        ObjectPhysics objectPhysics = levelObject.GetComponent<ObjectPhysics>();
        newLvlObj.FreezPosX = objectPhysics.GetPhysicsPositionX();
        newLvlObj.FreezPosY = objectPhysics.GetPhysicsPositionY();
        newLvlObj.FreezRot = objectPhysics.GetPhysicsRotation();
        newLvlObj.Collision = objectPhysics.GetCollision();
        newLvlObj.Gravity = objectPhysics.GetPhysicsGravity();

        newLvlObj.PhysicsType = objectPhysics.GetPhysicsType();
        newLvlObj.Duration = objectPhysics.GetDurationInRun();
        newLvlObj.ForceVectorX = objectPhysics.GetForceVector().x;
        newLvlObj.ForceVectorY = objectPhysics.GetForceVector().y;

        //Get Animation Data
        ObjectAnimation objectAnimation = levelObject.GetComponent<ObjectAnimation>();
        newLvlObj.AnimationType = objectAnimation.GetAnimationType();
        newLvlObj.Duration = objectAnimation.GetDuration();
        newLvlObj.StartVecX = objectAnimation.GetStartVector().x;
        newLvlObj.StartVecY = objectAnimation.GetStartVector().y;
        newLvlObj.EndVecX = objectAnimation.GetEndVector().x;
        newLvlObj.EndVecY = objectAnimation.GetEndVector().y;
        newLvlObj.IsPlay = objectAnimation.GetIsPlay();
        newLvlObj.IsLoop = objectAnimation.GetIsLoop();

        newLvlObj.playInRun = objectAnimation.GetPlayInRun();

        return newLvlObj;
    }
}

//INTERFACES

//Level Data
public interface ILevel
{
    //Initial Values
    int LevelIndex { get; set; }
    string LevelType { get; set; }
    string FeatureType { get; set; }
    string QuestionTxt { get; set; }

    //Quiz
    int AnswerCount { get; set; }
    List<AnswerData> AnswerData { get; set; }
    List<AnswerData> AnswerValues { get; set; }

    //Drag Drop Puzzle
    int SlotsCount { get; set; }
    int MatchesCount { get; set; }
    List<AnswerData> SlotData { get; set; }
    List<AnswerData> MatchData { get; set; }
    List<Dictionary<string, int>> SlotMatches { get; set; }

    //Select Puzzle
    int SelectsCount { get; set; }
    List<AnswerData> SelectData { get; set; }
    List<AnswerData> SelectValue { get; set; }

    List<ILevelObjectData> TemplateObjects { get; set; }
    List<ILevelObjectData> LevelObjects { get; set; }

    //Conditions
    List<string> LevelConditiontList { get; set; }
    List<IAnimationData> AnimationTriggerList { get; set; }
    List<IPhysicsData> PhysicsTriggerList { get; set; }
}

public class ILevelData : ILevel
{
    public int LevelIndex { get; set; }
    public string LevelType { get; set; }
    public string FeatureType { get; set; }
    public string QuestionTxt { get; set; }

    public int AnswerCount { get; set; }
    public List<AnswerData> AnswerData { get; set; }
    public List<AnswerData> AnswerValues { get; set; }

    public int SlotsCount { get; set; }
    public int MatchesCount { get; set; }
    public List<AnswerData> SlotData { get; set; }
    public List<AnswerData> MatchData { get; set; }
    public List<Dictionary<string, int>> SlotMatches { get; set; }

    public int SelectsCount { get; set; }
    public List<AnswerData> SelectData { get; set; }
    public List<AnswerData> SelectValue { get; set; }

    public List<ILevelObjectData> TemplateObjects { get; set; }
    public List<ILevelObjectData> LevelObjects { get; set; }

    public List<string> LevelConditiontList { get; set; }
    public List<IAnimationData> AnimationTriggerList { get; set; }
    public List<IPhysicsData> PhysicsTriggerList { get; set; }
}

//Answer Data
public interface IAnswerContainer
{
    int AnswerIndex { get; set; }
    string AnswerTxt { get; set; }
}

public class AnswerData : IAnswerContainer
{
    public int AnswerIndex { get; set; }
    public string AnswerTxt { get; set; }
}

//Trigger Data
public interface IAnimation
{
    int ConditionIndex { get; set; }
    string AnimationObject { get; set; }
}

public class IAnimationData : IAnimation
{
    public int ConditionIndex { get; set; }
    public string AnimationObject { get; set; }
}

public interface IPhysics
{
    int ConditionIndex { get; set; }
    string PhysicsObject { get; set; }
}

public class IPhysicsData : IPhysics
{
    public int ConditionIndex { get; set; }
    public string PhysicsObject { get; set; }
}

//LevelObject Data
public interface ILevelObject
{
    string ObjectName { get; set; }

    //Transform
    float PositionX { get; set; }
    float PositionY { get; set; }
    float ScaleX { get; set; }
    float ScaleY { get; set; }
    float Rotation { get; set; }

    //Image
    string Sprite { get; set; }
    string ImageColor { get; set; }
    float Opacity { get; set; }

    //Text
    bool EnableTxt { get; set; }
    float TextScaleX { get; set; }
    float TextScaleY { get; set; }
    string TextValue { get; set; }
    string TextColor { get; set; }
    bool IsBold { get; set; }
    float FontSize { get; set; }

    //Physics
    bool FreezPosX { get; set; }
    bool FreezPosY { get; set; }
    bool FreezRot { get; set; }
    bool Collision { get; set; }
    bool Gravity { get; set; }

    //Runtime Physics
    string PhysicsType { get; set; }
    float DurationInRun { get; set; }
    float ForceVectorX { get; set; }
    float ForceVectorY { get; set; }

    //Animation
    string AnimationType { get; set; }
    float Duration { get; set; }
    float StartVecX { get; set; }
    float StartVecY { get; set; }
    float EndVecX { get; set; }
    float EndVecY { get; set; }
    bool IsPlay { get; set; }
    bool IsLoop { get; set; }

    //Runtime Animation
    bool playInRun { get; set; }
}

public class ILevelObjectData : ILevelObject
{
    public string ObjectName { get; set; }

    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float ScaleX { get; set; }
    public float ScaleY { get; set; }
    public float Rotation { get; set; }

    public string Sprite { get; set; }
    public string ImageColor { get; set; }
    public float Opacity { get; set; }

    public bool EnableTxt { get; set; }
    public float TextScaleX { get; set; }
    public float TextScaleY { get; set; }
    public string TextValue { get; set; }
    public string TextColor { get; set; }
    public bool IsBold { get; set; }
    public float FontSize { get; set; }

    public bool FreezPosX { get; set; }
    public bool FreezPosY { get; set; }
    public bool FreezRot { get; set; }
    public bool Collision { get; set; }
    public bool Gravity { get; set; }

    public string PhysicsType { get; set; }
    public float DurationInRun { get; set; }
    public float ForceVectorX { get; set; }
    public float ForceVectorY { get; set; }

    public string AnimationType { get; set; }
    public float Duration { get; set; }
    public float StartVecX { get; set; }
    public float StartVecY { get; set; }
    public float EndVecX { get; set; }
    public float EndVecY { get; set; }
    public bool IsPlay { get; set; }
    public bool IsLoop { get; set; }

    public bool playInRun { get; set; }
}
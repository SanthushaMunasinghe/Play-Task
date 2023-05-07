using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameResults : MonoBehaviour
{
    private UIDocument prDoc;

    //UI Elements Results View
    protected VisualElement gameResulsView;

    protected Label startTimeElement;
    protected Label endTimeElement;
    protected Label finalDurationElement;
    protected Label finalScoreElement;

    protected ScrollView levelResultsView;

    //UI Elements
    protected VisualElement gameToolbar;

    private Button exitBtn;

    void Awake()
    {
        prDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = prDoc.rootVisualElement;

        gameResulsView = root.Q<VisualElement>("results-view");
        levelResultsView = gameResulsView.Q<ScrollView>();
        gameToolbar = root.Q<VisualElement>("topbar").Q<VisualElement>("project-toolbar");

        exitBtn = gameToolbar.Q<Button>("exit-btn");

        exitBtn.RegisterCallback<MouseUpEvent>(evt => {
            if (GlobalData.gameMode == "Test")
            {
                GlobalMethods.LoadScene("TeacherDashboardSubject");
            }
            else
            {
                GlobalMethods.LoadScene("StudentDashboardSubject");
            }
        });

        DisplayFinalResults();

        foreach (GameplayLevelData data in GlobalData.currentGameplayData.GameLevelData)
        {
            DisplayLevelResult(data);
        }
    }

    private void DisplayFinalResults()
    {
        //Get Elements
        startTimeElement = gameResulsView.Q<VisualElement>("game-results").Q<VisualElement>("start-time").Q<Label>();
        endTimeElement = gameResulsView.Q<VisualElement>("game-results").Q<VisualElement>("end-time").Q<Label>();
        finalDurationElement = gameResulsView.Q<VisualElement>("game-results").Q<VisualElement>("duration").Q<Label>();
        finalScoreElement = gameResulsView.Q<VisualElement>("game-results").Q<VisualElement>("final-score").Q<Label>();

        //Set Values
        startTimeElement.text = $"Start Time: {GlobalData.currentGameplayData.StartDateTime}";
        endTimeElement.text = $"End Time: {GlobalData.currentGameplayData.EndDateTime}";
        finalDurationElement.text = $"Duration: {GlobalData.currentGameplayData.Duration} Sec";
        finalScoreElement.text = $"Final Score: {GlobalData.currentGameplayData.FinalScore}%";
    }

    private void DisplayLevelResult(GameplayLevelData gpLvlData)
    {
        //CREATE
        //Label
        VisualElement levelLabelElement = new VisualElement();
        Label levelLabel = new Label();

        //Body
        VisualElement levelResultsElement = new VisualElement();
        //-Duration
        VisualElement durationElement = new VisualElement();
        //--
        VisualElement durationDetailsLabelElement = new VisualElement();
        Label durationDetailsLabel = new Label();
        //-Score
        VisualElement scoreElement = new VisualElement();
        //--
        VisualElement scoreDetailsLabelElement = new VisualElement();
        Label scoreDetailsLabel = new Label();

        //ADD CLASSES
        levelLabelElement.AddToClassList("box-label-primary");
        levelLabelElement.AddToClassList("result-label");

        levelResultsElement.AddToClassList("results-body");

        durationElement.AddToClassList("list-primary-item");
        durationElement.AddToClassList("results-body-content");
        durationDetailsLabelElement.AddToClassList("list-primary-item-text");
        durationDetailsLabel.AddToClassList("student-item-label");

        scoreElement.AddToClassList("list-primary-item");
        scoreElement.AddToClassList("results-body-content");
        scoreDetailsLabelElement.AddToClassList("list-primary-item-text");
        scoreDetailsLabel.AddToClassList("student-item-label");

        //ADD VALUES
        levelLabel.text = $"Level {gpLvlData.LevelIndex + 1}";
        durationDetailsLabel.text = $"Duration: {gpLvlData.Duration}";
        scoreDetailsLabel.text = $"Duration: {gpLvlData.Score} Sec";

        //ADD TO
        //durationDetailsLabelElement
        durationDetailsLabelElement.Add(durationDetailsLabel);
        //durationElement
        durationElement.Add(durationDetailsLabelElement);

        //scoreDetailsLabelElement
        scoreDetailsLabelElement.Add(scoreDetailsLabel);
        //scoreElement
        scoreElement.Add(scoreDetailsLabelElement);

        //levelResultsElement
        levelResultsElement.Add(durationElement);
        levelResultsElement.Add(scoreElement);

        //levelLabelElement
        levelLabelElement.Add(levelLabel);

        //levelResultsView
        levelResultsView.Add(levelLabelElement);
        levelResultsView.Add(levelResultsElement);
    }
}

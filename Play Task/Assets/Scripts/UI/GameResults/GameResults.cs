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

    //UI Elements
    protected VisualElement gameToolbar;

    private Button exitBtn;

    void Awake()
    {
        prDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = prDoc.rootVisualElement;

        gameResulsView = root.Q<VisualElement>("results-view");
        gameToolbar = root.Q<VisualElement>("topbar").Q<VisualElement>("project-toolbar");

        exitBtn = gameToolbar.Q<Button>("exit-btn");

        exitBtn.RegisterCallback<MouseUpEvent>(evt => {
            GlobalMethods.LoadScene("TeacherDashboardSubject");
        });

        DisplayFinalResults();
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
        finalDurationElement.text = $"Duration: {GlobalData.currentGameplayData.Duration}Seconds";
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
        Label durationLabel = new Label();
        //-Score
        VisualElement scoreElement = new VisualElement();
        Label scoreLabel = new Label();

        //ADD CLASSES
        levelLabelElement.AddToClassList("box-label-primary");
        levelLabelElement.AddToClassList("result-label");

        levelResultsElement.AddToClassList("results-body");

        durationElement.AddToClassList("list-primary-item");
        durationElement.AddToClassList("results-body-content");

        scoreElement.AddToClassList("list-primary-item");
        scoreElement.AddToClassList("results-body-content");
    }
}

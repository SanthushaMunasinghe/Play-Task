using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayer : MonoBehaviour
{
    private UIDocument pwDoc;

    //UI Elements Scene View
    protected VisualElement gameDisplay;

    //UI Elements Assets
    protected VisualElement infoTab;
    protected VisualElement gameToolbar;

    private Button exitBtn;

    void Awake()
    {
        pwDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = pwDoc.rootVisualElement;

        gameDisplay = root.Q<VisualElement>("game-display");
        infoTab = root.Q<VisualElement>("info-section");
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
    }
}

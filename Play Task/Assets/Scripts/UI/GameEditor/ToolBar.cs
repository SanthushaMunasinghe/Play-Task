using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolBar : EditorWindow
{
    [SerializeField] private LevelListManager levelListManager;

    [SerializeField] private LevelSettings levelSettings;

    [SerializeField] private ProjectDataManager projectDataManager;

    //New Toolbar UI Elements
    private Button newObjectBtn;
    private Button newLeveltBtn;
    private Button saveBtn;
    private Button exitBtn;

    void Start()
    {
        newObjectBtn = newToolbar.Q<Button>("new-object-btn");
        newLeveltBtn = newToolbar.Q<Button>("new-level-btn");
        saveBtn = projectToolbar.Q<Button>("save-btn");
        exitBtn = projectToolbar.Q<Button>("exit-btn");

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        newObjectBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            if (levelSettings.selectedLevelObj && levelSettings.selectedLevelObj.GetComponent<Level>().isCreated)
            {
                levelListManager.CreateLevelobject(levelSettings.selectedLevelObj);
            }
        });

        newLeveltBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            levelListManager.CreateLevel();
        });

        saveBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            if (levelListManager.levelsCount != 0)
            {
                projectDataManager.SaveData();
            }
        });

        exitBtn.RegisterCallback<MouseUpEvent>(evt => {
            GlobalMethods.LoadScene("TeacherDashboardSubject");
        });
    }
}

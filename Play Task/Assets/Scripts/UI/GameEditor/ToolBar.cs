using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolBar : EditorWindow
{
    [SerializeField] private LevelListManager levelListManager;

    [SerializeField] private LevelSettings levelSettings;

    //New Toolbar UI Elements
    private Button newObjectBtn;
    private Button newLeveltBtn;

    void Start()
    {
        newObjectBtn = newToolbar.Q<Button>("new-object-btn");
        newLeveltBtn = newToolbar.Q<Button>("new-level-btn");

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
    }
}

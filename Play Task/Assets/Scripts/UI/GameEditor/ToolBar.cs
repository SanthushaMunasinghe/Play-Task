using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolBar : EditorWindow
{
    [SerializeField] private LevelListManager levelListManager;

    //New Toolbar UI Elements
    private Button newProjectBtn;
    private Button newObjectBtn;
    private Button newLeveltBtn;

    void Start()
    {
        newProjectBtn = newToolbar.Q<Button>("new-project-btn");
        newObjectBtn = newToolbar.Q<Button>("new-object-btn");
        newLeveltBtn = newToolbar.Q<Button>("new-level-btn");

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        newProjectBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            Debug.Log("New Projects");
        });

        newObjectBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            Debug.Log("New Object");
        });
        
        newLeveltBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            levelListManager.CreateLevel();
        });
    }
}

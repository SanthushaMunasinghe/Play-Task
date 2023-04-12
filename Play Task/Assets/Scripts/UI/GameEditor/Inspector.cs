using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inspector : EditorWindow
{
    //Child Elements
    [SerializeField] private ObjectSettings objectSettings;
    [SerializeField] private LevelSettings levelSettings;

    public void SelectObject(GameObject obj)
    {
        objectSettings.componentList = inspectorTab.Q<VisualElement>("object-components");
        objectSettings.selectedObject = obj;
        objectSettings.GetElements();
    }

    public void SelectLevel(GameObject obj)
    {
        levelSettings.templateSettings = inspectorTab.Q<VisualElement>("template-settings");
        levelSettings.generateTemplate = inspectorTab.Q<VisualElement>("generate-template");
        levelSettings.levelDetailsComponent.templateDetails = inspectorTab.Q<VisualElement>("template-details");
        levelSettings.selectedLevelObj = obj;
        levelSettings.GetElements();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inspector : EditorWindow
{
    [SerializeField] private LevelListManager levelListManager;

    //Child Elements
    [SerializeField] private ObjectSettings objectSettings;
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private AnimationTriggerSettings animationTriggerSettings;

    //UI Element List
    private List<VisualElement> inspectorElements = new List<VisualElement>();

    void Start()
    {
        //Object Components
        objectSettings.componentList = inspectorTab.Q<VisualElement>("object-components");

        inspectorElements.Add(objectSettings.componentList);

        //Template Components
        levelSettings.templateSettingsListView = inspectorTab.Q<ScrollView>("template-settings-list");
        levelSettings.templateSettings = inspectorTab.Q<VisualElement>("template-settings");
        levelSettings.generateTemplate = inspectorTab.Q<VisualElement>("generate-template");
        levelSettings.levelDetailsComponent.templateDetails = inspectorTab.Q<VisualElement>("template-details");

        inspectorElements.Add(levelSettings.templateSettingsListView);

        //Condition Action Components
        animationTriggerSettings.animatonTriggerList = inspectorTab.Q<ScrollView>("animation-trigger");

        inspectorElements.Add(animationTriggerSettings.animatonTriggerList);
    }

    public void SelectObject(GameObject obj)
    {
        HideElements();

        objectSettings.componentList.style.display = DisplayStyle.Flex;

        objectSettings.selectedObject = obj;
        objectSettings.GetElements();
    }

    public void SelectLevel(GameObject obj)
    {
        HideElements();
        HideLevels();

        obj.SetActive(true);

        levelSettings.templateSettingsListView.style.display = DisplayStyle.Flex;

        levelSettings.selectedLevelObj = obj;
        levelSettings.GetElements();
    }

    public void SelectAnimationTriggerAction(Level lvl, int index)
    {
        HideElements();

        animationTriggerSettings.animatonTriggerList.style.display = DisplayStyle.Flex;
        animationTriggerSettings.selectedLevel = lvl;
        animationTriggerSettings.selectedAnimIndex = index;

        animationTriggerSettings.GetElements();
    }

    private void HideElements()
    {
        foreach (VisualElement element in inspectorElements)
        {
            element.style.display = DisplayStyle.None;
        }
    }

    private void HideLevels()
    {
        foreach (GameObject obj in levelListManager.lvlObjectList)
        {
            obj.SetActive(false);
        }
    }
}

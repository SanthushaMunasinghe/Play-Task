using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EditorWindow : MonoBehaviour
{
    private UIDocument ewDoc;

    //UI Elements Scene View
    protected VisualElement sceneView;

    //UI Elements Assets
    protected VisualElement assetsTab;

    //UI Elements Hierarchy
    protected VisualElement hiearchyTab;

    //UI Elements Inspector
    protected VisualElement inspectorTab;

    //UI Elements Toolbar
    protected VisualElement newToolbar;
    protected VisualElement projectToolbar;

    //UI Elements Levels Tab
    protected VisualElement levelListTab;

    void Awake()
    {
        ewDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = ewDoc.rootVisualElement;

        sceneView = root.Q<VisualElement>("scene-view");
        assetsTab = root.Q<VisualElement>("assets-tab").Q<VisualElement>("tab-body");
        inspectorTab = root.Q<VisualElement>("inspector-tab").Q<VisualElement>("tab-body");
        hiearchyTab = root.Q<VisualElement>("hierarchy-tab").Q<VisualElement>("tab-body");
        newToolbar = root.Q<VisualElement>("topbar").Q<VisualElement>("new-toolbar");
        projectToolbar = root.Q<VisualElement>("topbar").Q<VisualElement>("project-toolbar");
        levelListTab = root.Q<VisualElement>("levels-tab").Q<VisualElement>("tab-body");
    }
}

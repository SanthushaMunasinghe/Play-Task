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

    //UI Elements Inspector
    protected VisualElement inspectorTab;

    void Awake()
    {
        ewDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = ewDoc.rootVisualElement;

        sceneView = root.Q<VisualElement>("scene-view");
        assetsTab = root.Q<VisualElement>("assets-tab").Q<VisualElement>("tab-body");
        inspectorTab = root.Q<VisualElement>("inspector-tab").Q<VisualElement>("tab-body");
    }
}

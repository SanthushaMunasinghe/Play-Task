using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EditorWindow : MonoBehaviour
{
    private UIDocument ewDoc;

    //UI Elements Scene View
    protected VisualElement sceneView;

    void Awake()
    {
        ewDoc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

        var root = ewDoc.rootVisualElement;

        sceneView = root.Q<VisualElement>("scene-view");
    }

    void Update()
    {
        
    }
}

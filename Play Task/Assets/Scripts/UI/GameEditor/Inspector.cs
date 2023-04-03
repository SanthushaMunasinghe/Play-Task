using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inspector : EditorWindow
{
    //Child Elements
    [SerializeField] private ObjectSettings objectSettings;

    void Start()
    {
        objectSettings.componentList = inspectorTab.Q<VisualElement>("object-components");
    }

    public void SelectObject(GameObject obj)
    {
        objectSettings.selectedObject = obj;
        objectSettings.GetElements();
    }
}

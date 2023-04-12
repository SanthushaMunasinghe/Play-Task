using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hierarchy : EditorWindow
{
    //Selected Object
    private GameObject selectedLvlObj;
    private Level selectedLvl;

    //UI Elements
    private Label templateObjListLabel;
    private ScrollView templateObjList;
    private ScrollView objList;

    void Start()
    {
        //Get Elements
        templateObjListLabel = hiearchyTab.Q<VisualElement>("template-label").Q<Label>();
        templateObjList = hiearchyTab.Q<ScrollView>("template-object-list");
        objList = hiearchyTab.Q<ScrollView>("object-list");
    }

    public void SelectLevel(GameObject obj)
    {
        selectedLvlObj = obj;
        selectedLvl = selectedLvlObj.GetComponent<Level>();

        Setup();
    }

    private void Setup()
    {
        //Set Values
        templateObjListLabel.text = "Level " + (selectedLvl.levelIndex + 1);

        List<GameObject> templategameObjList = selectedLvl.GetTemplateObjectList();
        if (templategameObjList != null)
        {
            foreach (GameObject item in templategameObjList)
            {
                AddToListView(templateObjList, item);
            }
        }
    }

    private void AddToListView(ScrollView scrollView, GameObject obj)
    {
        VisualElement newItem = new VisualElement();
        Label nweLabel = new Label();

        newItem.AddToClassList("editor-list-item");
        nweLabel.AddToClassList("editor-list-text");

        nweLabel.text = obj.name;

        newItem.Add(nweLabel);

        newItem.RegisterCallback<MouseUpEvent>(evt =>
        {
            foreach (VisualElement childElement in scrollView.Children())
            {
                childElement.RemoveFromClassList("editor-list-item-selected");
            }

            newItem.AddToClassList("editor-list-item-selected");
        });

        scrollView.Add(newItem);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationTriggerSettings : MonoBehaviour
{
    public int selectedAnimIndex;
    public Level selectedLevel;

    //Value Lists
    private List<GameObject> levelObjList = new List<GameObject>();
    private List<string> levelObjNamesList = new List<string>();

    //Initial Values
    private GameObject animObject;
    private bool isPlay;

    //UI Elements

    //Parent
    public ScrollView animatonTriggerList;
    private  VisualElement animatonTriggerElement;

    //Elements
    DropdownField animationObjDropdown;
    VisualElement isPlayElement;
    Label isPlayText;

    public void GetElements()
    {
        //Get Elements
        animatonTriggerElement = animatonTriggerList.Q<VisualElement>("animation-trigger-component").Q<VisualElement>("component-body");

        animationObjDropdown = animatonTriggerElement.Q<VisualElement>("select-animation").Q<DropdownField>();
        isPlayElement = animatonTriggerElement.Q<VisualElement>("play").Q<VisualElement>("x-value");
        isPlayText = isPlayElement.Q<Label>();

        UpdateTriggerSettings();
    }

    public void UpdateTriggerSettings()
    {
        //Get Values
        if (selectedLevel.GetTemplateObjectList() != null)
        {
            foreach (GameObject tmpObj in selectedLevel.GetTemplateObjectList())
            {
                levelObjList.Add(tmpObj);
            }
        }

        if (selectedLevel.levelObjectList != null)
        {
            foreach (GameObject lvlObj in selectedLevel.levelObjectList)
            {
                levelObjList.Add(lvlObj);
            }
        }

        animationObjDropdown.choices = levelObjNamesList;

        if (levelObjList.Count > 0)
        {
            foreach (GameObject lvlObj in levelObjList)
            {
                levelObjNamesList.Add(lvlObj.name);
            }

            if (selectedLevel.levelAnimationList[selectedAnimIndex].AnimationObject != null)
            {
                animObject = levelObjList[levelObjList.IndexOf(selectedLevel.levelAnimationList[selectedAnimIndex].AnimationObject)];
            }
            else
            {
                animObject = null;
            }

            if (animObject != null)
            {
                isPlay = animObject.GetComponent<ObjectAnimation>().GetPlayInRun();
                animationObjDropdown.value = levelObjNamesList[levelObjNamesList.IndexOf(animObject.name)];
            }
            else
            {
                isPlay = false;
                animationObjDropdown.value = "";
            }

            //Set Initial Values
            isPlayText.text = GlobalMethods.SetBoolValue(isPlay);

            RegiterEvents();
        }
    }

    private void RegiterEvents()
    {
        animationObjDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;

            if (animationObjDropdown.index != -1)
            {
                animObject = levelObjList[levelObjNamesList.IndexOf(selectedValue)];
                selectedLevel.levelAnimationList[selectedAnimIndex].AnimationObject = animObject;
                isPlay = animObject.GetComponent<ObjectAnimation>().GetPlayInRun();
                isPlayText.text = GlobalMethods.SetBoolValue(isPlay);
            }
        });

        isPlayElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            if (animationObjDropdown.index != -1)
            {
                isPlay = !isPlay;
                animObject.GetComponent<ObjectAnimation>().UpdatePlayInRun(isPlay);
                isPlayText.text = GlobalMethods.SetBoolValue(isPlay);
            }
        });
    }
}

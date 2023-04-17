using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsTriggerSettings : MonoBehaviour
{
    public int selectedPhysicsIndex;
    public Level selectedLevel;

    //Value Lists
    private List<GameObject> levelObjList = new List<GameObject>();
    private List<string> levelObjNamesList = new List<string>();
    [SerializeField] private List<string> physicsTypes = new List<string>();

    //Initial Values
    private GameObject physicsObject;
    private string physicsType;
    private float duration;
    private float forceX;
    private float forceY;

    //UI Elements

    //Parent
    public ScrollView physicsTriggerList;
    private VisualElement physicsTriggerElement;

    //Elements
    DropdownField physicsObjDropdown;
    DropdownField physicsTypeDropdown;
    VisualElement durationElement;
    TextField durationField;
    TextField forceXField;
    TextField forceYField;

    public void GetElements()
    {
        //Get Elements
        physicsTriggerElement = physicsTriggerList.Q<VisualElement>("physics-component").Q<VisualElement>("component-body");

        physicsObjDropdown = physicsTriggerElement.Q<VisualElement>("select-physics").Q<DropdownField>();
        physicsTypeDropdown = physicsTriggerElement.Q<VisualElement>("physics-type").Q<DropdownField>();
        durationElement = physicsTriggerElement.Q<VisualElement>("duration");
        durationField = physicsTriggerElement.Q<VisualElement>("duration").Q<TextField>();
        forceXField = physicsTriggerElement.Q<VisualElement>("force").Q<VisualElement>("x-value").Q<TextField>();
        forceYField = physicsTriggerElement.Q<VisualElement>("force").Q<VisualElement>("y-value").Q<TextField>();

        durationElement.style.display = DisplayStyle.None;

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

        physicsObjDropdown.choices = levelObjNamesList;
        physicsTypeDropdown.choices = physicsTypes;

        if (levelObjList.Count > 0)
        {
            foreach (GameObject lvlObj in levelObjList)
            {
                levelObjNamesList.Add(lvlObj.name);
            }

            if (selectedLevel.levelPhysicsList[selectedPhysicsIndex].PhysicsObject != null)
            {
                physicsObject = levelObjList[levelObjList.IndexOf(selectedLevel.levelPhysicsList[selectedPhysicsIndex].PhysicsObject)];
            }
            else
            {
                physicsObject = null;
            }

            if (physicsObject != null)
            {
                physicsType = physicsObject.GetComponent<ObjectPhysics>().GetPhysicsType();
                duration = physicsObject.GetComponent<ObjectPhysics>().GetDurationInRun();
                forceX = physicsObject.GetComponent<ObjectPhysics>().GetForceVector().x;
                forceY = physicsObject.GetComponent<ObjectPhysics>().GetForceVector().y;

                physicsObjDropdown.value = levelObjNamesList[levelObjNamesList.IndexOf(physicsObject.name)];
            }
            else
            {
                physicsType = physicsTypes[0];
                duration = 0;
                forceX = 0;
                forceY = 0;
                
                physicsObjDropdown.value = "";
            }

            //Set Initial Values
            physicsTypeDropdown.value = physicsType;
            ToggleDuration(physicsType);
            durationField.value = duration.ToString();
            forceXField.value = forceX.ToString();
            forceYField.value = forceY.ToString();

            RegiterEvents();
        }
    }

    private void RegiterEvents()
    {
        physicsObjDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;

            if (physicsObjDropdown.index != -1)
            {
                physicsObject = levelObjList[levelObjNamesList.IndexOf(selectedValue)];
                selectedLevel.levelPhysicsList[selectedPhysicsIndex].PhysicsObject = physicsObject;
                physicsObject.GetComponent<ObjectPhysics>().UpdatePhysicsType(physicsType);
            }
        });

        physicsTypeDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            physicsType = selectedValue;
            ToggleDuration(physicsType);

            if (physicsObject != null)
            {
                physicsObject.GetComponent<ObjectPhysics>().UpdatePhysicsType(physicsType);
            }
        });

        durationField.RegisterCallback<BlurEvent>(evt =>
        {
            DurationInputHandler(durationField, ref duration);
        });
        
        forceXField.RegisterCallback<BlurEvent>(evt =>
        {
            ForceInputHndler(forceXField, ref forceX);
        });
        
        forceYField.RegisterCallback<BlurEvent>(evt =>
        {
            ForceInputHndler(forceYField, ref forceY);
        });
    }

    private void ToggleDuration(string value)
    {
        if (value == physicsTypes[0])
        {
            durationElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            durationElement.style.display = DisplayStyle.None;
            duration = 0;
            if (physicsObject != null)
            {
                physicsObject.GetComponent<ObjectPhysics>().UpdatePhysicsDuration(duration);
            }
        }
    }

    private void DurationInputHandler(TextField textField, ref float inputValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && physicsObject != null)
        {
            inputValue = float.Parse(value);
            physicsObject.GetComponent<ObjectPhysics>().UpdatePhysicsDuration(inputValue);
        }
        else
        {
            textField.value = inputValue.ToString();
        }
    }

    private void ForceInputHndler(TextField textField, ref float posValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && physicsObject != null)
        {
            posValue = float.Parse(value);
            physicsObject.GetComponent<ObjectPhysics>().UpdateForceVector(forceX, forceY);
        }
        else
        {
            textField.value = posValue.ToString();
        }
    }
}

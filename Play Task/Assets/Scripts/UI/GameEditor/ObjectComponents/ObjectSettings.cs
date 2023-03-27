using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSettings : MonoBehaviour
{
    [SerializeField] private TransformComponent transformComponent;

    public VisualElement componentList;

    //Refer Object
    public GameObject selectedObject;
    public ObjectTransform objectTransform;

    //Components UI
    public VisualElement transformComponentElement;
    public VisualElement imageComponent;
    public VisualElement physicsComponent;
    public VisualElement animationComponent;

    public void AssignObjectComponents()
    {
        objectTransform = selectedObject.GetComponent<ObjectTransform>();
    }

    public void GetElements()
    {
        AssignObjectComponents();

        transformComponentElement = componentList.Q<VisualElement>("transform-component");
        imageComponent = componentList.Q<VisualElement>("image-component");
        physicsComponent = componentList.Q<VisualElement>("physics-component");
        animationComponent = componentList.Q<VisualElement>("animation-component");

        //Get Transform Text Fields
        GetTransformElements();
    }

    private void GetTransformElements ()
    {
        //Get Fields
        transformComponent.positionXField = transformComponentElement.Q<VisualElement>("position").Q<VisualElement>("x-value").Q<TextField>();
        transformComponent.positionYField = transformComponentElement.Q<VisualElement>("position").Q<VisualElement>("y-value").Q<TextField>();

        transformComponent.scaleXField = transformComponentElement.Q<VisualElement>("scale").Q<VisualElement>("x-value").Q<TextField>();
        transformComponent.scaleYField = transformComponentElement.Q<VisualElement>("scale").Q<VisualElement>("y-value").Q<TextField>();

        transformComponent.rotationField = transformComponentElement.Q<VisualElement>("rotation").Q<VisualElement>("x-value").Q<TextField>();

        transformComponent.Setup();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSettings : MonoBehaviour
{
    [SerializeField] private TransformComponent transformComponent;
    [SerializeField] private ImageComponent imageComponent;
    [SerializeField] private PhysicsComponent physicsComponent;

    public VisualElement componentList;

    //Refer Object
    public GameObject selectedObject;
    public ObjectTransform objectTransform;
    public ObjectSprite objectSprite;
    public ObjectPhysics objectPhysics;

    //Components UI
    public VisualElement transformComponentElement;
    public VisualElement imageComponentElement;
    public VisualElement physicsComponentElement;
    public VisualElement animationComponentElement;

    public void AssignObjectComponents()
    {
        objectTransform = selectedObject.GetComponent<ObjectTransform>();
        objectSprite = selectedObject.GetComponent<ObjectSprite>();
        objectPhysics = selectedObject.GetComponent<ObjectPhysics>();
    }

    public void GetElements()
    {
        AssignObjectComponents();

        transformComponentElement = componentList.Q<VisualElement>("transform-component");
        imageComponentElement = componentList.Q<VisualElement>("image-component");
        physicsComponentElement = componentList.Q<VisualElement>("physics-component");
        animationComponentElement = componentList.Q<VisualElement>("animation-component");

        //Get Input Elements
        GetTransformElements();
        GetImageElements();
    }

    private void GetTransformElements ()
    {
        transformComponent.positionXField = transformComponentElement.Q<VisualElement>("position").Q<VisualElement>("x-value").Q<TextField>();
        transformComponent.positionYField = transformComponentElement.Q<VisualElement>("position").Q<VisualElement>("y-value").Q<TextField>();

        transformComponent.scaleXField = transformComponentElement.Q<VisualElement>("scale").Q<VisualElement>("x-value").Q<TextField>();
        transformComponent.scaleYField = transformComponentElement.Q<VisualElement>("scale").Q<VisualElement>("y-value").Q<TextField>();

        transformComponent.rotationField = transformComponentElement.Q<VisualElement>("rotation").Q<VisualElement>("x-value").Q<TextField>();

        transformComponent.Setup();
    }

    private void GetImageElements()
    {
        imageComponent.imageField = imageComponentElement.Q<VisualElement>("image");
        imageComponent.imageLabel = imageComponent.imageField.Q<VisualElement>("image-value").Q<Label>();
        imageComponent.colorField = imageComponentElement.Q<VisualElement>("color-value");
        imageComponent.opacityField = imageComponentElement.Q<VisualElement>("opacity-value").Q<TextField>();

        imageComponent.Setup();
    }
}

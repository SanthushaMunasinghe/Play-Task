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
        GetPhysicsElements();
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

    private void GetPhysicsElements()
    {
        physicsComponent.axisElementX = physicsComponentElement.Q<VisualElement>("enable-axis").Q<VisualElement>("x-value");
        physicsComponent.axisElementY = physicsComponentElement.Q<VisualElement>("enable-axis").Q<VisualElement>("y-value");
        physicsComponent.rotationElement = physicsComponentElement.Q<VisualElement>("rotation").Q<VisualElement>("value");
        physicsComponent.collisionElement = physicsComponentElement.Q<VisualElement>("collision").Q<VisualElement>("value");
        physicsComponent.gravityElement = physicsComponentElement.Q<VisualElement>("gravity").Q<VisualElement>("value");

        physicsComponent.axisLabelX = physicsComponent.axisElementX.Q<Label>();
        physicsComponent.axisLabelY = physicsComponent.axisElementY.Q<Label>();
        physicsComponent.rotationLabel = physicsComponent.rotationElement.Q<Label>();
        physicsComponent.collisionLabel = physicsComponent.collisionElement.Q<Label>();
        physicsComponent.gravityLabel = physicsComponent.gravityElement.Q<Label>();

        physicsComponent.Setup();
    }
}

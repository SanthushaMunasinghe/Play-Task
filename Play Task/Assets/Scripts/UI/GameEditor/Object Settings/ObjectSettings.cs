using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSettings : MonoBehaviour
{
    [SerializeField] private TransformComponent transformComponent;
    [SerializeField] private ImageComponent imageComponent;
    [SerializeField] private PhysicsComponent physicsComponent;
    [SerializeField] private AnimationComponent animationComponent;

    public VisualElement componentList;

    //Refer Object
    public GameObject selectedObject;
    public ObjectTransform objectTransform;
    public ObjectSprite objectSprite;
    public ObjectPhysics objectPhysics;
    public ObjectAnimation objectAnimation;

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
        objectAnimation = selectedObject.GetComponent<ObjectAnimation>();
    }

    public void GetElements()
    {
        transformComponentElement = componentList.Q<VisualElement>("transform-component");
        imageComponentElement = componentList.Q<VisualElement>("image-component");
        physicsComponentElement = componentList.Q<VisualElement>("physics-component");
        animationComponentElement = componentList.Q<VisualElement>("animation-component");

        if (selectedObject != null)
        {
            AssignObjectComponents();

            //Get Input Elements
            GetTransformElements();
            GetImageElements();
            GetPhysicsElements();
            GetAnimationElements();
        }
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

    private void GetAnimationElements()
    {
        animationComponent.animationTypesDropdown = animationComponentElement.Q<VisualElement>("type").Q<DropdownField>();
        animationComponent.durationField = animationComponentElement.Q<VisualElement>("duration").Q<VisualElement>("value").Q<TextField>();
        animationComponent.startXField = animationComponentElement.Q<VisualElement>("start").Q<VisualElement>("x-value").Q<TextField>();
        animationComponent.startYField = animationComponentElement.Q<VisualElement>("start").Q<VisualElement>("y-value").Q<TextField>();
        animationComponent.endXField = animationComponentElement.Q<VisualElement>("end").Q<VisualElement>("x-value").Q<TextField>();
        animationComponent.endYField = animationComponentElement.Q<VisualElement>("end").Q<VisualElement>("y-value").Q<TextField>();
        animationComponent.playElement = animationComponentElement.Q<VisualElement>("isplay").Q<VisualElement>("value");
        animationComponent.loopElement = animationComponentElement.Q<VisualElement>("isloop").Q<VisualElement>("value");
        animationComponent.playLabel = animationComponent.playElement.Q<Label>();
        animationComponent.loopLabel = animationComponent.loopElement.Q<Label>();

        animationComponent.Setup();
    }
}
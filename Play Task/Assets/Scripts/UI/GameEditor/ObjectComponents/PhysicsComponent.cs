using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsComponent : MonoBehaviour
{
    [SerializeField] private ObjectSettings objectSettings;

    //UI
    //Element
    public VisualElement axisElementX;
    public VisualElement axisElementY;
    public VisualElement rotationElement;
    public VisualElement collisionElement;
    public VisualElement gravityElement;
    //Label
    public Label axisLabelX;
    public Label axisLabelY;
    public Label rotationLabel;
    public Label collisionLabel;
    public Label gravityLabel;

    //Values
    private bool axisX;
    private bool axisY;
    private bool rot;
    private bool col;
    private bool grav;

    public void Setup()
    {
        //Set Initial Values
        axisX = objectSettings.objectPhysics.GetPhysicsPositionX();
        axisY = objectSettings.objectPhysics.GetPhysicsPositionY();
        rot = objectSettings.objectPhysics.GetPhysicsRotation();
        col = objectSettings.objectPhysics.GetCollision();
        grav = objectSettings.objectPhysics.GetPhysicsGravity();

        //Display Initial Values
        axisLabelX.text = "X: " + GlobalMethods.SetValue(!axisX);
        axisLabelY.text = "Y: " + GlobalMethods.SetValue(!axisY);
        rotationLabel.text = GlobalMethods.SetValue(rot);
        collisionLabel.text = GlobalMethods.SetValue(col);
        gravityLabel.text = GlobalMethods.SetValue(grav);

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        axisElementX.RegisterCallback<MouseUpEvent>(evt =>
        {
            axisX = GlobalMethods.SwitchBool(axisX);
            objectSettings.objectPhysics.UpdatePhysicsPositionX(axisX);
            axisLabelX.text = GlobalMethods.SetValue(!axisX);
        });
        
        axisElementY.RegisterCallback<MouseUpEvent>(evt =>
        {
            axisY = GlobalMethods.SwitchBool(axisY);
            objectSettings.objectPhysics.UpdatePhysicsPositionY(axisY);
            axisLabelY.text = GlobalMethods.SetValue(!axisY);
        });

        rotationElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            rot = GlobalMethods.SwitchBool(rot);
            objectSettings.objectPhysics.UpdatePhysicsRotation(rot);
            rotationLabel.text = GlobalMethods.SetValue(rot);
        });

        collisionElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            col = GlobalMethods.SwitchBool(col);
            objectSettings.objectPhysics.UpdateCollision(col);
            collisionLabel.text = GlobalMethods.SetValue(col);
        });

        gravityElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            grav = GlobalMethods.SwitchBool(grav);
            objectSettings.objectPhysics.UpdatePhysicsGravity(grav);
            gravityLabel.text = GlobalMethods.SetValue(grav);
        });
    }
}

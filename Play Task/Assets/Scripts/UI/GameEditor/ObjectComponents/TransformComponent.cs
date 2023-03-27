using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformComponent : MonoBehaviour
{
    [SerializeField] private ObjectSettings objectSettings;

    //Transform UI
    public TextField positionXField;
    public TextField positionYField;
    public TextField scaleXField;
    public TextField scaleYField;
    public TextField rotationField;

    //Transform
    private string positionValueX;
    private string positionValueY;
    private string scaleValueX;
    private string scaleValueY;
    private string rotationValue;

    public void Setup()
    {
        positionValueX = objectSettings.objectTransform.GetPosition().x.ToString();
        positionValueY = objectSettings.objectTransform.GetPosition().y.ToString();
        scaleValueX = objectSettings.objectTransform.GetScale().x.ToString();
        scaleValueY = objectSettings.objectTransform.GetScale().y.ToString();
        rotationValue = objectSettings.objectTransform.GetRotation().ToString();

        positionXField.value = positionValueX;
        positionYField.value = positionValueY;
        scaleXField.value = scaleValueX;
        scaleYField.value = scaleValueY;
        rotationField.value = rotationValue;

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        //Position
        positionXField.RegisterCallback<BlurEvent>(evt =>
        {
            PositionInputHndler(positionXField, ref positionValueX);
        });

        positionYField.RegisterCallback<BlurEvent>(evt =>
        {
            PositionInputHndler(positionYField, ref positionValueY);
        });

        //Scale
        scaleXField.RegisterCallback<BlurEvent>(evt =>
        {
            ScaleInputHandler(scaleXField, ref scaleValueX);
        });

        scaleYField.RegisterCallback<BlurEvent>(evt =>
        {
            ScaleInputHandler(scaleYField, ref scaleValueY);
        });

        //Rotation
        rotationField.RegisterCallback<BlurEvent>(evt =>
        {
            RotationInputHandler(rotationField, ref rotationValue);
        });
    }

    private void PositionInputHndler(TextField textField, ref string posValue)
    {
        string value = textField.value;

        bool isValid = ValidateTransformInput(value);

        if (isValid)
        {
            posValue = value;
            objectSettings.objectTransform.UpdatePosition(float.Parse(positionValueX), float.Parse(positionValueY));
        }
        else
        {
            textField.value = posValue;
        }
    }

    private void ScaleInputHandler(TextField textField, ref string scaleValue)
    {
        string value = textField.value;

        bool isValid = ValidateTransformInput(value);

        if (isValid)
        {
            scaleValue = value;
            objectSettings.objectTransform.UpdateScale(float.Parse(scaleValueX), float.Parse(scaleValueY));
        }
        else
        {
            textField.value = scaleValue;
        }
    }

    private void RotationInputHandler(TextField textField, ref string rotValue)
    {
        string value = textField.value;

        bool isValid = ValidateTransformInput(value);

        if (isValid)
        {
            rotValue = value;
            objectSettings.objectTransform.UpdateRotation(float.Parse(rotationValue));
        }
        else
        {
            textField.value = rotValue;
        }
    }

    public bool ValidateTransformInput(string input)
    {
        if (float.TryParse(input, out float floatValue))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

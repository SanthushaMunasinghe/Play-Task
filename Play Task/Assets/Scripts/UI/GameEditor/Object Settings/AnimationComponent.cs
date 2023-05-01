using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField] private ObjectSettings objectSettings;

    [SerializeField] private List<string> animationTypes;

    //Animation UI
    public DropdownField animationTypesDropdown;
    public TextField durationField;
    public TextField startXField;
    public TextField startYField;
    public TextField endXField;
    public TextField endYField;
    public VisualElement playElement;
    public VisualElement loopElement;
    public Label playLabel;
    public Label loopLabel;

    //Animation Values
    private string animTypeValue;
    private float durationValue;
    private float startX;
    private float startY;
    private float endX;
    private float endY;
    private bool isPlay;
    private bool isLoop;

    private Vector2 originalPos;
    private Vector2 originalScale;

    public void Setup()
    {
        animationTypesDropdown.choices = animationTypes;

        //Initial Values
        //Set Default Type
        if (objectSettings.objectAnimation.GetAnimationType() == "")
        {
            animTypeValue = animationTypes[0];
            objectSettings.objectAnimation.UpdateType(animTypeValue);
        }
        else
        {
            animTypeValue = objectSettings.objectAnimation.GetAnimationType();
        }

        durationValue = objectSettings.objectAnimation.GetDuration();
        startX = objectSettings.objectAnimation.GetStartVector().x;
        startY = objectSettings.objectAnimation.GetStartVector().y;
        endX = objectSettings.objectAnimation.GetEndVector().x;
        endY = objectSettings.objectAnimation.GetEndVector().y;
        isPlay = objectSettings.objectAnimation.GetIsPlay();
        isLoop = objectSettings.objectAnimation.GetIsLoop();

        //Defauult UI Fields
        animationTypesDropdown.value = animTypeValue;
        durationField.value = durationValue.ToString();
        startXField.value = startX.ToString();
        startYField.value = startY.ToString();
        endXField.value = endX.ToString();
        endYField.value = endY.ToString();
        playLabel.text = GlobalMethods.SetBoolValue(isPlay);
        loopLabel.text = GlobalMethods.SetBoolValue(isLoop);

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        animationTypesDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;

            animTypeValue = selectedValue;
            objectSettings.objectAnimation.UpdateType(animTypeValue);

            if (animTypeValue == animationTypes[0])
            {
                ResetValues();
            }
        });

        durationField.RegisterCallback<BlurEvent>(evt =>
        {
            DurationInputHandler(durationField, ref durationValue);
        });

        //Vector Value Change
        startXField.RegisterValueChangedCallback(evt => {
            StartInputHndler(startXField, ref startX);
        });
        
        startYField.RegisterValueChangedCallback(evt => {
            StartInputHndler(startYField, ref startY);
        });

        endXField.RegisterValueChangedCallback(evt => {
            EndInputHandler(endXField, ref endX);
        });

        endYField.RegisterValueChangedCallback(evt => {
            EndInputHandler(endYField, ref endY);
        });

        //Save Vector
        startXField.RegisterCallback<BlurEvent>(evt =>
        {
            SaveStartInput(startXField, ref startX);
        });

        startYField.RegisterCallback<BlurEvent>(evt =>
        {
            SaveStartInput(startYField, ref startY);
        });

        endXField.RegisterCallback<BlurEvent>(evt =>
        {
            SaveEndInput(endXField, ref endX);
        });

        endYField.RegisterCallback<BlurEvent>(evt =>
        {
            SaveEndInput(endYField, ref endY);
        });

        //Booleans
        playElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            if (animTypeValue != animationTypes[0] && durationValue > 0)
            {
                isPlay = GlobalMethods.SwitchBool(isPlay);
                objectSettings.objectAnimation.UpdatePlay(isPlay);
                playLabel.text = GlobalMethods.SetBoolValue(isPlay);
            }
        });

        loopElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            if (animTypeValue != animationTypes[0] && durationValue > 0)
            {
                isLoop = GlobalMethods.SwitchBool(isLoop);
                objectSettings.objectAnimation.UpdateLoop(isLoop);
                loopLabel.text = GlobalMethods.SetBoolValue(isLoop);
            }
        });
    }

    private void DurationInputHandler(TextField textField, ref float inputValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && animTypeValue != animationTypes[0])
        {
            inputValue = float.Parse(value);
            objectSettings.objectAnimation.UpdateDuration(inputValue);
        }
        else
        {
            textField.value = inputValue.ToString();
        }
    }

    private void StartInputHndler(TextField textField, ref float inputValue)
    {
        originalPos = objectSettings.selectedObject.transform.position;
        originalScale = objectSettings.selectedObject.transform.localScale;

        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && animTypeValue != animationTypes[0] && durationValue > 0)
        {
            inputValue = float.Parse(value);

            if (animTypeValue == animationTypes[1])
                objectSettings.objectTransform.UpdatePosition(startX, startY);
            else if (animTypeValue == animationTypes[2])
                objectSettings.objectTransform.UpdateScale(startX, startY);
        }
    }

    private void SaveStartInput(TextField textField, ref float inputValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && animTypeValue != animationTypes[0] && durationValue > 0)
        {
            inputValue = float.Parse(value);objectSettings.objectAnimation.UpdateStartVector(startX, startY);
        }
        else
        {
            textField.value = inputValue.ToString();
        }

        objectSettings.selectedObject.transform.position = originalPos;
        objectSettings.selectedObject.transform.localScale = originalScale;
    }

    private void EndInputHandler(TextField textField, ref float inputValue)
    {
        originalPos = objectSettings.selectedObject.transform.position;
        originalScale = objectSettings.selectedObject.transform.localScale;

        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && animTypeValue != animationTypes[0] && durationValue > 0)
        {
            inputValue = float.Parse(value);

            if (animTypeValue == animationTypes[1])
                objectSettings.objectTransform.UpdatePosition(endX, endY);
            else if (animTypeValue == animationTypes[2])
                objectSettings.objectTransform.UpdateScale(endX, endY);
        }
    }

    private void SaveEndInput(TextField textField, ref float inputValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid && animTypeValue != animationTypes[0] && durationValue > 0)
        {
            inputValue = float.Parse(value); objectSettings.objectAnimation.UpdateEndVector(endX, endY);
        }
        else
        {
            textField.value = inputValue.ToString();
        }

        objectSettings.selectedObject.transform.position = originalPos;
        objectSettings.selectedObject.transform.localScale = originalScale;
    }

    private void ResetValues()
    {
        //Reset Values
        durationValue = 0;
        startX = 0;
        startY = 0;
        endX = 0;
        endY = 0;
        isPlay = false;
        isLoop = false;

        //Update Values
        objectSettings.objectAnimation.UpdateDuration(durationValue);
        objectSettings.objectAnimation.UpdateStartVector(startX, startY);
        objectSettings.objectAnimation.UpdateEndVector(endX, endY);
        objectSettings.objectAnimation.UpdatePlay(isPlay);
        objectSettings.objectAnimation.UpdateLoop(isLoop);

        //Display Values
        durationField.value = durationValue.ToString();
        startXField.value = startX.ToString();
        startYField.value = startY.ToString();
        endXField.value = endX.ToString();
        endYField.value = endY.ToString();
        playLabel.text = GlobalMethods.SetBoolValue(isPlay);
        loopLabel.text = GlobalMethods.SetBoolValue(isLoop);
    }
}

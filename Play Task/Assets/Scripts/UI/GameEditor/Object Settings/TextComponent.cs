using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextComponent : MonoBehaviour
{
    [SerializeField] private ObjectSettings objectSettings;

    //Color Setup
    [SerializeField] private List<Color> colorList = new List<Color>();
    [SerializeField] private List<VisualElement> colorSlots = new List<VisualElement>();

    //Text UI
    public VisualElement enableTextElement;
    public Label enableTextLabel;
    public TextField scaleXField;
    public TextField scaleYField;
    public TextField textValueField;
    public VisualElement colorField;
    public TextField fontSizeField;
    public VisualElement fontBoldElement;
    public Label fontBoldLabel;

    //Text Values
    private bool isEnable;
    private string scaleValueX;
    private string scaleValueY;
    private string textValue;
    private Color currentColor;
    private float currentfontSize;
    private bool isBold;

    public void Setup()
    {
        // Set Initial Values
        isEnable = objectSettings.objectText.GetEnableText(); 
        scaleValueX = objectSettings.objectText.GetTextScale().x.ToString();
        scaleValueY = objectSettings.objectText.GetTextScale().y.ToString();
        textValue = objectSettings.objectText.GetTextValue();
        currentColor = objectSettings.objectText.GetTextColor();
        currentfontSize = objectSettings.objectText.GetFontSize();
        isBold = objectSettings.objectText.GetIsBold();

        // Input Fields
        enableTextLabel.text = GlobalMethods.SetBoolValue(isEnable);
        scaleXField.value = scaleValueX;
        scaleYField.value = scaleValueY;
        textValueField.value = textValue;
        fontSizeField.value = currentfontSize.ToString();
        fontBoldLabel.text = GlobalMethods.SetBoolValue(isBold);

        RegisterEvents();

        colorField.Clear();
        foreach (Color color in colorList)
        {
            AddColors(color);
        }
    }

    private void RegisterEvents()
    {
        enableTextElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            isEnable = !isEnable;
            objectSettings.objectText.UpdateEnableText(isEnable);
            enableTextLabel.text = GlobalMethods.SetBoolValue(isEnable);
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

        textValueField.RegisterCallback<BlurEvent>(evt =>
        {
            textValue = textValueField.value;
            objectSettings.objectText.UpdateTextValue(textValue);
        });

        fontSizeField.RegisterCallback<BlurEvent>(evt =>
        {
            FontSizeInputHndler(fontSizeField, ref currentfontSize);
        });

        fontBoldElement.RegisterCallback<MouseUpEvent>(evt =>
        {
            isBold = !isBold;
            objectSettings.objectText.UpdateTextBold(isBold);
            fontBoldLabel.text = GlobalMethods.SetBoolValue(isBold);
        });
    }

    private void AddColors(Color clr)
    {
        VisualElement newItem = new VisualElement();

        newItem.AddToClassList("color-slot");

        if (clr == currentColor)
        {
            newItem.AddToClassList("color-slot-selected");
        }

        newItem.style.backgroundColor = clr;

        colorSlots.Add(newItem);

        newItem.RegisterCallback<MouseUpEvent>(evt => {
            foreach (VisualElement slot in colorSlots)
            {
                slot.RemoveFromClassList("color-slot-selected");
            }

            currentColor = clr;
            objectSettings.objectText.UpdateTextColor(clr);
            newItem.AddToClassList("color-slot-selected");
        });

        colorField.Add(newItem);
    }

    private void ScaleInputHandler(TextField textField, ref string scaleValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid)
        {
            scaleValue = value;
            objectSettings.objectText.UpdateTextScale(float.Parse(scaleValueX), float.Parse(scaleValueY));
        }
        else
        {
            textField.value = scaleValue;
        }
    }

    private void FontSizeInputHndler(TextField textField, ref float fsValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid)
        {
            if (GlobalMethods.ValidateMinMax(0, 100, float.Parse(value)))
            {
                fsValue = float.Parse(value);
                objectSettings.objectText.UpdateTFontSize(currentfontSize);
            }
            else
            {
                textField.value = fsValue.ToString();
            }
        }
        else
        {
            textField.value = fsValue.ToString();
        }
    }
}

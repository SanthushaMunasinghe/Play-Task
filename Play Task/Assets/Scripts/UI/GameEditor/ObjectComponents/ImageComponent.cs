using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ImageComponent : MonoBehaviour
{
    [SerializeField] private ObjectSettings objectSettings;
    [SerializeField] private Assets assets;

    //Color Setup
    [SerializeField] private List<Color> colorList = new List<Color>();
    [SerializeField] private List<VisualElement> colorSlots = new List<VisualElement>();

    //Image UI
    public VisualElement imageField;
    public Label imageLabel;
    public VisualElement colorField;
    public TextField opacityField;

    //Image Values
    public Sprite currentImage;
    private Color currentColor;
    private float currentOpacity;

    public void Setup()
    {
        // Set Initial Values
        currentImage = objectSettings.objectSprite.GetSprite();
        currentColor = objectSettings.objectSprite.GetColor();
        currentOpacity = objectSettings.objectSprite.GetOpacity() * 100;

        objectSettings.objectSprite.UpdateSprite(currentImage);

        // Input Fields
        imageLabel.text = currentImage.name;
        opacityField.value = currentOpacity.ToString();

        RegisterEvents();

        foreach (Color color in colorList)
        {
            AddColors(color);
        }
    }

    private void RegisterEvents()
    {
        //Opacity
        opacityField.RegisterCallback<BlurEvent>(evt =>
        {
            OpacityInputHndler(opacityField, ref currentOpacity);
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
            objectSettings.objectSprite.UpdateColor(clr);
            newItem.AddToClassList("color-slot-selected");
        });

        colorField.Add(newItem);
    }

    public void SelectSprite(Sprite spr)
    {
        currentImage = spr;
        objectSettings.objectSprite.UpdateSprite(spr);
        imageLabel.text = currentImage.name;
    }

    private void OpacityInputHndler(TextField textField, ref float opaValue)
    {
        string value = textField.value;

        bool isValid = GlobalMethods.ValidateTransformInput(value);

        if (isValid)
        {
            opaValue = float.Parse(value);
            objectSettings.objectSprite.UpdateOpacity(currentOpacity);
        }
        else
        {
            textField.value = opaValue.ToString();
        }
    }
}

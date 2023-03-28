using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Assets : EditorWindow
{
    [SerializeField] private ImageComponent imageComponent;

    public List<Sprite> assetSpritesList = new List<Sprite>();

    private VisualElement assetListUI;

    void Start()
    {
        assetListUI = assetsTab.Q<ScrollView>("asset-list").Q<VisualElement>("list");

        if (assetSpritesList.Count != 0)
        {
            foreach (Sprite assetSprite in assetSpritesList)
            {
                CreateList(assetSprite, assetSprite.name);
            }
        }
    }

    private void CreateList(Sprite sprite, string spriteName)
    {
        VisualElement newItem = new VisualElement();
        VisualElement newImage = new VisualElement();
        Label newLabel = new Label();

        newItem.AddToClassList("asset-item");
        newImage.AddToClassList("asset-image");
        newLabel.AddToClassList("asset-label");

        newImage.style.backgroundImage = new StyleBackground(sprite.texture);

        newLabel.text = spriteName;

        newItem.Add(newImage);
        newItem.Add(newLabel);

        newItem.RegisterCallback<MouseUpEvent>(evt => {
            imageComponent.SelectSprite(sprite);
        });

        assetListUI.Add(newItem);
    }
}

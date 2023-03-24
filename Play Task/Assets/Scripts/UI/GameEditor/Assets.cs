using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Assets : EditorWindow
{
    public List<Sprite> assetSpritesList = new List<Sprite>();

    private VisualElement assetListUI;

    void Start()
    {
        assetListUI = assetsTab.Q<ScrollView>("asset-list").Q<VisualElement>("list");

        if (assetSpritesList.Count != 0)
        {
            foreach (Sprite assetSprite in assetSpritesList)
            {
                CreateList(assetSprite);
            }
        }
    }

    private void CreateList(Sprite sprite)
    {
        VisualElement newItem = new VisualElement();
        newItem.AddToClassList("asset-item");

        // Set the background image of the new item to the provided sprite
        newItem.style.backgroundImage = new StyleBackground(sprite.texture);

        // Add the new item to the asset list UI
        assetListUI.Add(newItem);
    }
}

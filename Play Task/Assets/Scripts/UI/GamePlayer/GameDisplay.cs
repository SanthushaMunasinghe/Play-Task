using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDisplay : GamePlayer
{
    [SerializeField] private Camera displayCamera;
    [SerializeField] private SpriteRenderer boundary;

    //UI Elements
    public Label levelElementLabel;

    void Start()
    {
        gameDisplay.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            float width = gameDisplay.layout.width;
            float height = gameDisplay.layout.height;

            // Calculate the aspect ratio
            float screenRatio = width / height;

            float targetRatio = boundary.bounds.size.x / boundary.bounds.size.y;

            if (screenRatio >= targetRatio)
            {
                displayCamera.orthographicSize = boundary.bounds.size.y / 2;
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                displayCamera.orthographicSize = boundary.bounds.size.y / 2 * differenceInSize;
            }
        });
    }

    public void GetElements()
    {
        levelElementLabel = gameDisplay.Q<VisualElement>("LevelElement").Q<Label>();
    }

    public void UpdateLevelText(int txt, int count)
    {
        levelElementLabel.text = "Level " + (txt + 1).ToString() + "/" + count;
    }
}

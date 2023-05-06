using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDisplay : GamePlayer
{
    [SerializeField] private GamePlayLevelManager gamePlayLevelManager;

    [SerializeField] private Camera displayCamera;
    [SerializeField] private SpriteRenderer boundary;

    [SerializeField] private LayerMask m_layerMask;
    private RaycastHit2D hit;
    private Vector3 worldPos;

    //UI Elements
    public Label levelElementLabel;
    public Button nextLvlBtn;

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

        nextLvlBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            gamePlayLevelManager.UpdateLevel();
        });
    }

    public void GetElements()
    {
        levelElementLabel = gameDisplay.Q<VisualElement>("LevelElement").Q<Label>();
        nextLvlBtn = gameDisplay.Q<VisualElement>("NextElement").Q<Button>();

        nextLvlBtn.style.display = DisplayStyle.None;
    }

    public void ActivateNextButton()
    {
        nextLvlBtn.style.display = DisplayStyle.Flex;
    }

    public void UpdateLevelText(int txt, int count)
    {
        levelElementLabel.text = "Level " + (txt + 1).ToString() + "/" + count;
    }
}

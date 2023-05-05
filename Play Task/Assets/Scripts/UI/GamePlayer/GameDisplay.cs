using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDisplay : GamePlayer
{
    [SerializeField] private Camera displayCamera;
    [SerializeField] private SpriteRenderer boundary;

    [SerializeField] private LayerMask m_layerMask;
    private RaycastHit2D hit;
    private Vector3 worldPos;

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

        //gameDisplay.RegisterCallback<MouseDownEvent>(evt =>
        //{
        //    worldPos = displayCamera.ScreenToWorldPoint(evt.mousePosition);
        //    Debug.Log(worldPos.y + 1.0f);
        //    hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y + 1.5f), Vector2.zero);
        //    if (hit.collider != null)
        //    {
        //        Debug.Log(hit.collider.gameObject.name);
        //    }
        //});
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

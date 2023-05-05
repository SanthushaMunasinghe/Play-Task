using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDisplay : GamePlayer
{
    [SerializeField] private Camera displayCamera;
    [SerializeField] private SpriteRenderer boundary;

    [SerializeField] private LayerMask m_layerMask;
    private RaycastHit m_hit;

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

        gameDisplay.RegisterCallback<PointerUpEvent>(evt =>
        {
            Ray ray = displayCamera.ScreenPointToRay(evt.position);
            if (Physics.Raycast(ray, out m_hit, Mathf.Infinity, m_layerMask))
            {
                Collider hitCollider = m_hit.collider;
                Debug.Log(m_hit.point);
                Debug.Log("Selected object: " + hitCollider.gameObject.name);
            }
            else
            {
                Debug.Log(m_hit.point);
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

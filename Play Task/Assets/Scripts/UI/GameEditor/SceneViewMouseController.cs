using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneViewMouseController : EditorWindow
{
    [SerializeField] private Camera sceneCam;

    [SerializeField] private float panningSpeedX = 1.0f;
    [SerializeField] private float panningSpeedY = 1.0f;

    [SerializeField] private float zoomSpeed = 1.0f;

    void OnEnable()
    {
        sceneView.RegisterCallback<MouseDownEvent>(OnMouseDrag);
        sceneView.RegisterCallback<WheelEvent>(OnMouseWheel);
    }

    void Update()
    {
        
    }

    private void OnMouseDrag(MouseDownEvent evt)
    {
        if (evt.button == 0)
        {
            Vector2 mouseDownPosition = evt.mousePosition;
            sceneView.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            sceneView.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }
    }

    void OnMouseMove(MouseMoveEvent evt)
    {
        Vector2 mouseDelta = evt.mouseDelta;
        // Move camera based on mouse delta
        sceneCam.transform.Translate(-mouseDelta.x * panningSpeedX * Time.deltaTime, mouseDelta.y * panningSpeedY * Time.deltaTime, 0);
    }

    void OnMouseUp(MouseUpEvent evt)
    {
        sceneView.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        sceneView.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    void OnMouseWheel(WheelEvent evt)
    {
        float delta = evt.delta.y;
        float zoomDelta = delta * zoomSpeed;
        sceneCam.orthographicSize = Mathf.Clamp(sceneCam.orthographicSize + zoomDelta, 1, 1000);
        evt.StopPropagation();
    }
}

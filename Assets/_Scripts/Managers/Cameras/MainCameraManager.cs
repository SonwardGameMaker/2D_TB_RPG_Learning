using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    [Header("Camera Size")]
    [SerializeField] float _minCameraSize;
    [SerializeField] float _maxCameraSize;
    [SerializeField] float _defaultCameraSize;
    [SerializeField] float _zoomStep;

    public void Setup()
    {
        if (Camera.main.orthographicSize != _defaultCameraSize)
            Camera.main.orthographicSize = _defaultCameraSize;
    }

    public void ZoomInCamera()
        => ZoomCamera(-_zoomStep);

    public void ZoomOutCamera()
        => ZoomCamera(_zoomStep);

    private void ZoomCamera(float amount)
    {
        if (Camera.main.orthographicSize + amount <= _maxCameraSize
            && Camera.main.orthographicSize + amount >= _minCameraSize)
            Camera.main.orthographicSize += amount;
    }
}

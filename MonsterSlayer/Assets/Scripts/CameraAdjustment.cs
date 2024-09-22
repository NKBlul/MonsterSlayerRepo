using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main Camera
    public float designWidth = 1080f;  // The width of your reference resolution
    public float designHeight = 1920f;  // The height of your reference resolution
    public float adjustableScale = 200f;

    void Start()
    {
        // Calculate the orthographic size adjustment factor
        if (mainCamera != null)
        {
            AdjustCameraSize();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdjustCameraSize();
            Debug.Log("YET");
        }
    }

    void AdjustCameraSize()
    {
        // Calculate the aspect ratio for the design resolution
        float targetAspect = designWidth / designHeight;

        // Calculate the aspect ratio for the current screen
        float screenAspect = (float)Screen.width / (float)Screen.height;

        // Calculate the scale factor to adjust the orthographic size
        float scale = targetAspect / screenAspect;

        // Adjust the orthographic size based on the aspect ratio
        mainCamera.orthographicSize = designHeight / adjustableScale * scale;
    }

}

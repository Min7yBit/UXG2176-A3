using UnityEngine;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public enum CameraMode { FirstPerson, ThirdPerson, Fixed }
    public CameraMode currentMode = CameraMode.ThirdPerson;

    public List<Camera> thirdPersonCameras = new List<Camera>();

    public Camera firstPersonCam;
    public Camera thirdPersonCam;
    public Camera activeFixedCam;



    void Start()
    {
        SetCameraMode(currentMode);
    }

    void Update()
    {
        // Toggle manually
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetCameraMode(CameraMode.FirstPerson);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetCameraMode(CameraMode.ThirdPerson);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetCameraMode(CameraMode.Fixed);
    }

    public void SetCameraMode(CameraMode mode)
    {
        currentMode = mode;

        firstPersonCam.enabled = (mode == CameraMode.FirstPerson);
        thirdPersonCam.enabled = (mode == CameraMode.ThirdPerson);

        if (mode == CameraMode.Fixed && activeFixedCam != null)
            activeFixedCam.enabled = true;
        else
        {
            // disable all fixed cams
            foreach (var cam in FindObjectsByType<Camera>(FindObjectsSortMode.None))
            {
                if (cam.CompareTag("FixedCamera"))
                    cam.enabled = false;
            }
        }
    }

    // Called by triggers when entering fixed camera zones
    public void SwitchToFixedCamera(Camera newCam)
    {
        if (activeFixedCam != null)
            activeFixedCam.enabled = false;

        activeFixedCam = newCam;
        SetCameraMode(CameraMode.Fixed);
    }
}

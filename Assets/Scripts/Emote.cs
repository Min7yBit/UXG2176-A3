using UnityEngine;

public class Emote : MonoBehaviour
{
    public Animator animator;
    public Camera cam;
    public CameraControl cameraControl;
    public PlayerMovement playerMovement;

    public bool isDancing = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isDancing)
        {
            isDancing = true;
            playerMovement.CanMove = false;
            cameraControl.SwitchToFixedCamera(cam);

            animator.SetBool("Dance", true);
        }
        else if (Input.GetKeyDown(KeyCode.X) && isDancing)
        {
            isDancing = false;
            playerMovement.CanMove = true;
            cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);

            animator.SetBool("Dance", false);
        }
    }
}

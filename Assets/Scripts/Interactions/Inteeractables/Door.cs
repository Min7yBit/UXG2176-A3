using System.Collections;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;
    public bool ShowPrompt { get; set; } = true;
    public CameraControl cameraControl;
    public Camera cam;
    public GameObject lockReflection;

    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private GameObject interactPromptGO;

    public string itemName;

    private PlayerMovement playerMovement;
    private bool interactable = true;
    [SerializeField]private Inventory inventory;
    [SerializeField] private Collider mirrorShardCol;

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {        
        if (inventory.ContainsItem(itemName))
        {
            InInteract = true;
            lockReflection.SetActive(true);
            mirrorShardCol.enabled = true;
            interactable = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            this.playerMovement = playerMovement;
            this.playerMovement.CanMove = false;
            cameraControl.SwitchToFixedCamera(cam);
        }
        else
        {
            Debug.Log("I can't see the lock...");
            ShowMessage("I can't see the lock...", 2);
        }
    }
    private void Update()
    {
        if (InInteract)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                InInteract = false;
                mirrorShardCol.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                interactable = true;
                playerMovement.CanMove = true;
                cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
            }
        }
    }

    public TextMeshProUGUI messageText;

    public void ShowMessage(string msg, float duration = 2f)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        StartCoroutine(FadeMessage(duration));
    }

    private IEnumerator FadeMessage(float duration)
    {
        // Reset alpha to fully visible
        Color c = messageText.color;
        c.a = 1;
        messageText.color = c;

        // Fade out over time
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, elapsed / duration);
            messageText.color = c;
            yield return null;
        }

        // Hide once fully faded
        messageText.gameObject.SetActive(false);
    }
}

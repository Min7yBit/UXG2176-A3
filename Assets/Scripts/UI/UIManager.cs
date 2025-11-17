using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventory;
    public Inventory playerInventory;
    public CombineSystem combineSystem;

    private bool isPaused = false;

    void Update()
    {
        // ESC behaviour: close inventory first, pause second
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventory.activeSelf)
            {
                ToggleInventory();
                return;
            }

            TogglePauseMenu();
        }

        // I key (only allowed when not paused)
        if (Input.GetKeyDown(KeyCode.I) && !isPaused)
        {
            ToggleInventory();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void ToggleInventory()
    {
        bool newState = !inventory.activeSelf;

        inventory.SetActive(newState);

        Cursor.lockState = newState ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = newState || isPaused;

        if (newState)
        {
            playerInventory.ResetUI();
            combineSystem.ResetCombineSystem();
        }
    }
}

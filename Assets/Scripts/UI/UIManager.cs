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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
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
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked; //if paused, unlock cursor
    }
    public void ToggleInventory()
    {        
        inventory.SetActive(!inventory.activeSelf);
        Cursor.lockState = inventory.activeSelf ? CursorLockMode.None : CursorLockMode.Locked; //if inventory open, unlock cursor
        playerInventory.ResetUI();
        combineSystem.ResetCombineSystem();
    }
}

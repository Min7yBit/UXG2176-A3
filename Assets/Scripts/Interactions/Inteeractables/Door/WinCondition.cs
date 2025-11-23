using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public bool allowInteract = false;
    public LockCombination lockCombination1;
    public LockCombination lockCombination2;
    public LockCombination lockCombination3;
    public UIManager manager;

    private void Update()
    {
        if (allowInteract)
        {
            lockCombination1.CanInteract = true;
            lockCombination2.CanInteract = true;
            lockCombination3.CanInteract = true;
        }
        else
        {
            lockCombination1.CanInteract = false;
            lockCombination2.CanInteract = false;
            lockCombination3.CanInteract = false;
        }
    }

    public void ResetAllCombinations()
    {
        lockCombination1.ResetCombination();
        lockCombination2.ResetCombination();
        lockCombination3.ResetCombination();
    }

    public void CheckWinCondition()
    {
        if (lockCombination1.IsCorrectCombination() && lockCombination2.IsCorrectCombination() && lockCombination3.IsCorrectCombination())
        {
            Debug.Log("Win Condition Met! You unlocked the door!");
            manager.ShowWinMenu();
            // Additional win logic can be added here
        }
    }
}

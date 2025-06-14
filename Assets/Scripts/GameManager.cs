using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] TimeOfDayManager timeOfDayManager;
    bool _isInventoryOpen = false;

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        inventory.SetActive(true);
        timeOfDayManager.Pause("INVENTORY");
        playerInputHandler.SwapActionMap("UI");

        StartCoroutine(FrameWait());
        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            _isInventoryOpen = true;
        }
    }

    public void OnInventoryClose(InputAction.CallbackContext context)
    {
        if (!context.started || !_isInventoryOpen)
            return;

        _isInventoryOpen = false;
        inventory.SetActive(false);
        timeOfDayManager.Unpause("INVENTORY");
        playerInputHandler.SwapActionMap("Player");
    }
}

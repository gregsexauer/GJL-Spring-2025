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
        Debug.Log("open");

        inventory.SetActive(true);
        timeOfDayManager.isPaused = true;
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
        if (!context.started)
            return;
        if (!_isInventoryOpen)
            return;

        Debug.Log("close");
        _isInventoryOpen = false;
        inventory.SetActive(false);
        timeOfDayManager.isPaused = false;
        playerInputHandler.SwapActionMap("Player");
    }
}

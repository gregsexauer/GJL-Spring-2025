using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartOfLevelScreen : MonoBehaviour
{
    [SerializeField] TimeOfDayManager timeOfDayManager;
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] PlayerInput playerInput;

    private void Awake()
    {
        timeOfDayManager.Pause("LEVEL_START");
    }

    public void OnPressPlay()
    {
        timeOfDayManager.Unpause("LEVEL_START");
        playerInputHandler.SwapActionMap("Player");
        gameObject.SetActive(false);
    }
}

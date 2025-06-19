using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [Header("Player Input Values")]
    [field: SerializeField] public Vector2 MoveInput { get; private set; }
    [field: SerializeField] public Vector2 LookInput { get; private set; }
    [field: SerializeField] public bool JumpInput { get; private set; }
    [field: SerializeField] public bool SprintInput { get; private set; }
    [field: SerializeField] public bool InteractInput { get; private set; }

    private void Start()
    {
        StartCoroutine(WaitThenDisableActions());
        IEnumerator WaitThenDisableActions()
        {
            yield return null;
            foreach (var map in playerInput.actions.actionMaps)
                map.Disable();
            SwapActionMap("UI");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = context.performed;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput = context.performed;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractInput = context.performed;
    }


    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void SwapActionMap(string newMap)
    {
        switch (newMap)
        {
            case "Player":
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case "UI":
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
        }

        playerInput.SwitchCurrentActionMap(newMap);
    }
}

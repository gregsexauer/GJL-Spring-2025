using UnityEngine;
using TMPro;

public class PlayerInteractableHandler : MonoBehaviour
{
    [SerializeField] float interactionRange = 2.0f;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] Transform cameraFollow;
    [SerializeField] TextMeshProUGUI interactionTooltip;
    [SerializeField] Color nonActiveInteractionTooltipTextColor;

    bool _interactedLastFrame = false;
    PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponentInParent<PlayerInputHandler>();
    }

    private void Update()
    {
        Ray ray = new Ray(cameraFollow.position, cameraFollow.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red, .2f);

        // If the interaction ray hits something
        if (Physics.Raycast(ray, out hit, interactionRange, interactionLayer))
        {
            // And if it's an Interactable
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                interactionTooltip.text = interactable.TooltipText;
                interactionTooltip.color = interactable.IsActive ? Color.white : nonActiveInteractionTooltipTextColor;

                // And if the player presses the interact input while the Interactable is Active
                if (_inputHandler.InteractInput && !_interactedLastFrame && interactable.IsActive)
                {
                    // Interact and return
                    interactable.Interact();
                    _interactedLastFrame = true;
                    return;
                }
            }
            // What we hit isn't an Interactable - clear the text
            else
            {
                interactionTooltip.text = "";
            }
        }
        // No hit - clear the text
        else
        {
            interactionTooltip.text = "";
        }

        if (_interactedLastFrame && !_inputHandler.InteractInput)
            _interactedLastFrame = false;
    }
}

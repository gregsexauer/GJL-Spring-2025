using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    [field: Header("Interactable")]
    [field: SerializeField] public bool IsActive { get; private set; } = true;
    [field: SerializeField] public string TooltipText { get; private set; } = "Talk";

    [Header("Dialogue")]
    [SerializeField] string nodeName;

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(nodeName);
    }
}

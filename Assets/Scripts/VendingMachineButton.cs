using UnityEngine;

public class VendingMachineButton : MonoBehaviour, IInteractable
{
    [SerializeField] VendingMachineControls controls;
    [SerializeField] string input;
    public bool IsActive { get; private set; } = true;
    public string TooltipText { get; private set; } = "Press";

    public void Interact()
    {
        controls.Input(input);
        GetComponent<AudioSource>().Play();
    }
}

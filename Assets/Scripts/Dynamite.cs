using UnityEngine;
using Yarn.Unity;

public class Dynamite : MonoBehaviour
{
    public bool IsCigar = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite cigarSprite;
    
    [YarnCommand("Swap")]
    public void Swap()
    {
        spriteRenderer.sprite = cigarSprite;
        GetComponent<DialogueInteractable>().IsActive = false;
        IsCigar = true;
    }
}

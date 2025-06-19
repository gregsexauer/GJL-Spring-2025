using UnityEngine;
using Yarn.Unity;

public class Dynamite : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite cigarSprite;
    
    [YarnCommand("Swap")]
    public void Swap()
    {
        spriteRenderer.sprite = cigarSprite;
        GetComponent<DialogueInteractable>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}

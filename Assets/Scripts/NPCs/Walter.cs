using UnityEngine;
using Yarn.Unity;

public class Walter : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite happySprite;

    [YarnCommand("Finish_Quest_Walter")]
    public void FinishQuest()
    {
        spriteRenderer.sprite = happySprite;
        GetComponent<DialogueInteractable>().IsActive = false;
        gameManager.CompleteQuest("Walter");
    }    
}

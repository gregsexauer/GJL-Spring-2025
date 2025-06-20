using UnityEngine;
using Yarn.Unity;

public class Walter : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite happySprite;

    [YarnCommand("Finish_Quest")]
    public void FinishQuest()
    {
        spriteRenderer.sprite = happySprite;
        gameManager.CompleteQuest("Walter");
    }    
}

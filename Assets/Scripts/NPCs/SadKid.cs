using DG.Tweening;
using UnityEngine;
using Yarn.Unity;

public class SadKid : MonoBehaviour
{
    [SerializeField] GameObject umbrella;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite holdingUmbrella;
    [SerializeField] Sprite happySprite;
    [SerializeField] GameManager gameManager;
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] SpriteRenderer cloud;

    [YarnCommand("Give_Umbrella")]
    public void GiveUmbrella()
    {
        umbrella.SetActive(true);
        spriteRenderer.sprite = holdingUmbrella;
    }

    [YarnCommand("Finish_Quest_Kid")]
    public void FinishQuest()
    {
        spriteRenderer.sprite = happySprite;
        GetComponent<DialogueInteractable>().IsActive = false;
        gameManager.CompleteQuest("Kid");
        rainParticles.Stop();
        cloud.DOBlendableColor(new Color(255, 255, 255, 0), 1f);
    }
}

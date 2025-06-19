using UnityEngine;
using Yarn.Unity;

public class SadKid : MonoBehaviour
{
    [SerializeField] GameObject umbrella;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite holdingUmbrella;

    [YarnCommand("Give_Umbrella")]
    public void GiveUmbrella()
    {
        umbrella.SetActive(true);
        spriteRenderer.sprite = holdingUmbrella;
    }
}

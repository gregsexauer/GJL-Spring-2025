using UnityEngine;

public class TallySet : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite tally1;
    [SerializeField] Sprite tally2;
    [SerializeField] Sprite tally3;
    [SerializeField] Sprite tally4;
    [SerializeField] Sprite tally5;

    public void SetTallies(int num)
    {
        switch (num)
        {
            case 0:
                spriteRenderer.sprite = null;
                break;

            case 1:
                spriteRenderer.sprite = tally1;
                break;

            case 2:
                spriteRenderer.sprite = tally2;
                break;

            case 3:
                spriteRenderer.sprite = tally3;
                break;

            case 4:
                spriteRenderer.sprite = tally4;
                break;

            case 5:
                spriteRenderer.sprite = tally5;
                break;
        }

    }
}


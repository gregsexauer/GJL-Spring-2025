using UnityEngine;

public class DangerousWires : MonoBehaviour
{
    [SerializeField] Sprite powerOffSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider daveTrigger;

    public void TurnOffElectricity()
    {
        spriteRenderer.sprite = powerOffSprite;
        daveTrigger.enabled = false;
    }
}

using UnityEngine;
using Yarn.Unity;

public class Piano : MonoBehaviour
{
    [SerializeField] Sprite fixedSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    static bool _isFixed = false;

    private void Start()
    {
        _isFixed = false;
    }

    [YarnCommand("Replace_Rope")]
    public void FixPiano()
    {
        spriteRenderer.sprite = fixedSprite;
        _isFixed = true;
    }

    [YarnFunction("Is_Piano_Fixed")]
    public static bool IsPianoFixed()
    {
        return _isFixed;
    }

    public void Drop()
    {
        if (_isFixed)
            return;

        GetComponent<Rigidbody>().useGravity = true;
    }
}

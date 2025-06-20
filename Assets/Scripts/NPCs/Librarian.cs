using UnityEngine;
using Yarn.Unity;

public class Librarian : MonoBehaviour
{
    static bool _hasGivenLibraryCard = false;

    private void Start()
    {
        _hasGivenLibraryCard = false;
    }

    [YarnFunction("Has_Given_Library_Card")]
    public static bool HasGivenLibraryCard()
    {
        return _hasGivenLibraryCard;
    }

    [YarnCommand("Give_Library_Card")]
    public void GiveLibraryCard()
    {
        _hasGivenLibraryCard = true;
    }
}

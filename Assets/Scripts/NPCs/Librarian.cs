using UnityEngine;
using Yarn.Unity;

public class Librarian : MonoBehaviour
{
    static bool _hasGivenLibraryCard = false;
    static bool _hasTalked = false;

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


    [YarnFunction("Has_Talked_Librarian")]
    public static bool HasTalkedToLibrarian()
    {
        return _hasTalked;
    }

    [YarnCommand("On_Talk_Librarian")]
    public void OnTalk()
    {
        _hasTalked = true;
    }


}

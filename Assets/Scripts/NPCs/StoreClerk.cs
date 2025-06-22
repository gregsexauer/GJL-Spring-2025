using UnityEngine;
using Yarn.Unity;

public class StoreClerk : MonoBehaviour
{
    static bool _hasTalked = false;


    [YarnFunction("Has_Talked_Clerk")]
    public static bool HasTalkedToClerk()
    {
        return _hasTalked;
    }

    [YarnCommand("On_Talk_Clerk")]
    public void OnTalk()
    {
        _hasTalked=true;
    }

}

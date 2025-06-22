using UnityEngine;
using Yarn.Unity;

public class Scout : MonoBehaviour
{
    [YarnCommand("Pet_Scout")]
    public void PetScout()
    {
        Debug.Log("pet scout :)");
    }
}

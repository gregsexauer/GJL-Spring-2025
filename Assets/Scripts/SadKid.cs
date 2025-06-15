using UnityEngine;
using Yarn.Unity;

public class SadKid : MonoBehaviour
{
    [SerializeField] GameObject umbrella;

    [YarnCommand("Give_Umbrella")]
    public void GiveUmbrella()
    {
        umbrella.SetActive(true);
    }
}

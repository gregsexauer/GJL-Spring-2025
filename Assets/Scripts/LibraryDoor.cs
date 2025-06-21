using UnityEngine;
using Yarn.Unity;
using DG.Tweening;

public class LibraryDoor : MonoBehaviour
{
    [SerializeField] Transform doorLeft;
    [SerializeField] Transform doorRight;
    [YarnCommand("Open_Door")]
    public void OpenDoor()
    {
        doorLeft.DORotate(new Vector3(0, -90, 0), .8f);
        doorRight.DORotate(new Vector3(0, 90, 0), .8f);
        GetComponentInChildren<Collider>().enabled = false;
    }
}

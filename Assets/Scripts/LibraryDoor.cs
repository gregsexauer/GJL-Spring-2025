using UnityEngine;
using Yarn.Unity;
using DG.Tweening;

public class LibraryDoor : MonoBehaviour
{
    [YarnCommand("Open_Door")]
    public void OpenDoor()
    {
        transform.DORotate(new Vector3(0, -90, 0), .8f);
    }
}

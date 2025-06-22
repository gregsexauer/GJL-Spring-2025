using DG.Tweening;
using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class Scout : MonoBehaviour
{
    [SerializeField] Transform heart;

    [YarnCommand("Pet_Scout")]
    public void PetScout()
    {
        StartCoroutine(Pet());
    }

    IEnumerator Pet()
    {
        GetComponent<DialogueInteractable>().IsActive = false;
        heart.DOScale(3, .15f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1);
        heart.DOScale(0, .25f).SetEase(Ease.OutFlash);

        yield return new WaitForSeconds(.25f);

        GetComponent<DialogueInteractable>().IsActive = true;
    }
}

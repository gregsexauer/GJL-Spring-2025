using UnityEngine;
using DG.Tweening;
using Yarn.Unity;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip overworldTheme; 
    [SerializeField] AudioSource main;
    [SerializeField] AudioSource secondary;
    [SerializeField] AudioClip kidTheme;
    [SerializeField] AudioClip walterTheme;
    [SerializeField] AudioClip shopTheme;

    public void PlayMainTheme()
    {
        main.Play();
    }

    public void StopMainTheme()
    {
        main.Stop();
    }

    [YarnCommand("Start_Kid_Theme")]
    public void StartKidTheme()
    {
        secondary.clip = kidTheme;
        main.DOFade(0, .15f).OnComplete(() => FadeInSecondary());
    }

    [YarnCommand("Start_Walter_Theme")]
    public void StartWalterTheme()
    {
        secondary.clip = walterTheme;
        main.DOFade(0, .15f).OnComplete(() => FadeInSecondary());
    }

    public void StartShopTheme()
    {
        secondary.clip = shopTheme;
        main.DOFade(0, .15f).OnComplete(() => FadeInSecondary());
    }

    public void ExitInterior()
    {
        secondary.DOFade(0, .15f).OnComplete(() => main.DOFade(1, .15f));
    }

    void FadeInSecondary()
    {
        secondary.Play();
        secondary.DOFade(1, .15f);
    }

    public void ReturnToMainTheme()
    {
        if (main.volume == 1 || secondary.clip == shopTheme || (main.volume == 0 && secondary.volume == 0)) return;
        secondary.DOFade(0, .15f).OnComplete(() => main.DOFade(1, .15f));
    }

    public void FadeOutMainTheme()
    {
        main.DOFade(0, .15f);
    }
}

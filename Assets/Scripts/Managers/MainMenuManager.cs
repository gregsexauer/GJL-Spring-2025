using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject credits;
    [SerializeField] Transform TVImage;
    [SerializeField] Transform title;
    [SerializeField] Knob knob;
    GameObject _activeSubMenu;
    AudioSource _audioSource;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        title.DOScale(2.5f, .25f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(.25f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(title.DORotate(new Vector3(0, 0, -20), 1).SetEase(Ease.Linear))
            .Append(title.DORotate(new Vector3(0, 0, 0), 1).SetEase(Ease.Linear))
            .Append(title.DORotate(new Vector3(0, 0, 20), 1).SetEase(Ease.Linear))
            .Append(title.DORotate(new Vector3(0, 0, 0), 1).SetEase(Ease.Linear));
        sequence.SetLoops(-1);
    }

    public void OnPlay()
    {
        knob.OnPlay();
        mainMenu.GetComponent<CanvasGroup>().DOFade(0, .2f);
        TVImage.DOScale(2.25f, 2f).OnComplete(() => SceneManager.LoadScene("Overworld")).SetEase(Ease.Linear);
        _audioSource.Play();
    }

    public void OnControls()
    {
        knob.OnControls();
        mainMenu.SetActive(false);
        controls.SetActive(true);
        _activeSubMenu = controls;
        _audioSource.Play();
    }

    public void OnCredits()
    {
        knob.OnCredits();
        mainMenu.SetActive(false);
        credits.SetActive(true);
        _activeSubMenu = credits;
        _audioSource.Play();
    }

    public void OnQuit()
    {
        Application.Quit();
        _audioSource.Play();
    }

    public void ExitSubMenu()
    {
        knob.OnMainMenu();
        mainMenu.SetActive(true);
        _activeSubMenu.SetActive(false);
        _audioSource.Play();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject credits;
    [SerializeField] Transform TVImage;
    [SerializeField] Knob knob;
    GameObject _activeSubMenu;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnPlay()
    {
        knob.OnPlay();
        mainMenu.GetComponent<CanvasGroup>().DOFade(0, .2f);
        TVImage.DOScale(2.25f, 2f).OnComplete(() => SceneManager.LoadScene("Overworld")).SetEase(Ease.Linear);
    }

    public void OnControls()
    {
        knob.OnControls();
        mainMenu.SetActive(false);
        controls.SetActive(true);
        _activeSubMenu = controls;
    }

    public void OnCredits()
    {
        knob.OnCredits();
        mainMenu.SetActive(false);
        credits.SetActive(true);
        _activeSubMenu = credits;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void ExitSubMenu()
    {
        knob.OnMainMenu();
        mainMenu.SetActive(true);
        _activeSubMenu.SetActive(false);
    }
}

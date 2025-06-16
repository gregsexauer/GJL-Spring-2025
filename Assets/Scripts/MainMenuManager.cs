using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject controls;
    [SerializeField] GameObject credits;
    GameObject _activeSubMenu;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void OnControls()
    {
        controls.SetActive(true);
        _activeSubMenu = controls;
    }

    public void OnCredits()
    {
        credits.SetActive(true);
        _activeSubMenu = credits;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void ExitSubMenu()
    {
        _activeSubMenu.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] TimeOfDayManager timeOfDayManager;
    [SerializeField] TextMeshProUGUI loopFailText;
    [SerializeField] CanvasGroup failCanvasGroup;
    [SerializeField] DialogueRunner dialogueRunner;
    bool _isInventoryOpen = false;

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        inventory.SetActive(true);
        timeOfDayManager.Pause("INVENTORY");
        playerInputHandler.SwapActionMap("UI");

        StartCoroutine(FrameWait());
        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            _isInventoryOpen = true;
        }
    }

    public void OnInventoryClose(InputAction.CallbackContext context)
    {
        if (!context.started || !_isInventoryOpen)
            return;

        _isInventoryOpen = false;
        inventory.SetActive(false);
        timeOfDayManager.Unpause("INVENTORY");
        playerInputHandler.SwapActionMap("Player");
    }

    [YarnCommand("Fail_Loop")]
    public void FailLoop(string reason)
    {
        timeOfDayManager.Pause("GAME_OVER");

        switch (reason)
        {
            case "Piano":
                loopFailText.text = "A falling piano has crushed Accidents Man!";
                break;

            case "Electrocution":
                loopFailText.text = "Accidents Man has been electrocuted by a fallen power wire!";
                break;

            case "Dynamite":
                loopFailText.text = "Accidents Man thought he was smoking a cigar, but it was actually a stick of dynamite!";
                break;

            case "Said Something Weird":
                loopFailText.text = "You said something weird to a child!";
                break;

            case "Libary Closed":
                loopFailText.text = "The library is closed and the child will fail their next test!";
                break;

            case "No Wallet":
                loopFailText.text = "You didn't get the wallet!";
                break;
        }

        StartCoroutine(LoopFail());
    }

    IEnumerator LoopFail()
    {
        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        yield return new WaitForSeconds(1);

        playerInputHandler.SwapActionMap("UI");

        failCanvasGroup.DOFade(1, 1);

        yield return new WaitForSeconds(1);

        failCanvasGroup.interactable = true;
        failCanvasGroup.blocksRaycasts = true;
    }

    public void RestartDay()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

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
    [SerializeField] TextMeshProUGUI daveQuestText;
    [SerializeField] TextMeshProUGUI walterQuestText;
    [SerializeField] TextMeshProUGUI kidQuestText;
    [SerializeField] CanvasGroup gameWinCanvasGroup;
    [SerializeField] AudioClip loopStartSFX;
    [SerializeField] AudioClip loopFailSFX;
    bool _gameOver = false;
    int _completedQuests = 0;
    bool _isInventoryOpen = false;
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.PlayOneShot(loopStartSFX);
    }

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        inventory.GetComponent<CanvasGroup>().alpha = 1;
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

        inventory.GetComponent<CanvasGroup>().alpha = 0;
        timeOfDayManager.Unpause("INVENTORY");
        playerInputHandler.SwapActionMap("Player");
    }

    [YarnCommand("Fail_Loop")]
    public void FailLoop(string reason)
    {
        if (_gameOver) return;
        timeOfDayManager.Pause("GAME_OVER");
        _gameOver = true;
        switch (reason)
        {
            case "Piano":
                loopFailText.text = "A falling piano has crushed Death Wish Dave!";
                break;

            case "Electrocution":
                loopFailText.text = "Death Wish Dave has been electrocuted by a fallen power wire!";
                break;

            case "Dynamite":
                loopFailText.text = "Death Wish Dave thought he was smoking a cigar, but it was actually a stick of dynamite!";
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

        _audioSource.PlayOneShot(loopFailSFX);

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

    public void CompleteQuest(string name)
    {
        _completedQuests++;
        if (name == "Walter")
            walterQuestText.text = StrikeThroughText(walterQuestText.text);
        else if (name == "Dave")
            daveQuestText.text = StrikeThroughText(daveQuestText.text);
        else if (name == "Kid")
            kidQuestText.text = StrikeThroughText(kidQuestText.text);

        if (_completedQuests == 3)
            StartCoroutine(FinishGame());
    }

    string StrikeThroughText(string text)
    {
        return "<s>" + text + "</s>";
    }

    IEnumerator FinishGame()
    {
        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        yield return new WaitForSeconds(1);

        playerInputHandler.SwapActionMap("UI");

        gameWinCanvasGroup.DOFade(1, 1);

        yield return new WaitForSeconds(1);

        gameWinCanvasGroup.interactable = true;
        gameWinCanvasGroup.blocksRaycasts = true;
    }
}

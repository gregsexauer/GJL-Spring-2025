using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class VendingMachineControls : MonoBehaviour
{
    [SerializeField] List<VendingMachineButton> buttons;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform wallet;
    [SerializeField] Transform walletEndPosition;
    [SerializeField] Item walletItem;
    [SerializeField] SpriteRenderer glass;
    [SerializeField] Sprite darkSprite;

    bool _hasWalletDropped;
    string _currentInput = "";

    public void OnInsertCoins()
    {
        foreach (VendingMachineButton button in buttons)
            button.GetComponent<Collider>().enabled = true;
    }

    public void Input(string input)
    {
        _currentInput += input;
        text.text = _currentInput;
        if (_currentInput.Length == 2)
            ValidateInput();
    }

    void ValidateInput()
    {
        foreach (VendingMachineButton button in buttons)
            button.GetComponent<Collider>().enabled = false;

        if (_currentInput == "A1"
            || _currentInput == "A2"
            || _currentInput == "A3"
            || _currentInput == "A4"
            || _currentInput == "B1"
            || _currentInput == "B2"
            || _currentInput == "B4"
            || _currentInput == "C1"
            || _currentInput == "C2"
            || _currentInput == "C3"
            || _currentInput == "C4"
            || _currentInput == "D1"
            || _currentInput == "D2"
            || _currentInput == "D3"
            || _currentInput == "D4")
        {
            gameManager.FailLoop("No Wallet");
        }
        else if (_currentInput == "B3")
        {
            _hasWalletDropped = true;
            GetComponent<AudioSource>().Play();
            wallet.DOMove(walletEndPosition.position, .5f).SetEase(Ease.Linear).OnComplete(() => RevealWallet());
            StartCoroutine(ResetText());
            IEnumerator ResetText()
            {
                yield return new WaitForSeconds(1.5f);
                _currentInput = "";
                text.text = "";
            }
        }
        else
        {
            text.text = "INVALID";
            StartCoroutine(TimeOutButtons());
        }
    }

    void RevealWallet()
    {
        wallet.gameObject.SetActive(false);
        walletItem.Reveal();
    }

    IEnumerator TimeOutButtons()
    {
        yield return new WaitForSeconds(1.5f);
        _currentInput = "";
        text.text = "";
        foreach (VendingMachineButton button in buttons)
            button.GetComponent<Collider>().enabled = true;
    }

    public void PowerOff()
    {
        if (!_hasWalletDropped)
            gameManager.FailLoop("Power Off No Wallet");
        glass.sprite = darkSprite;
    }
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VendingMachineControls : MonoBehaviour
{
    [SerializeField] List<VendingMachineButton> buttons;
    [SerializeField] TextMeshProUGUI text;
    string _currentInput = "";

    public void OnInsertCoins()
    {
        foreach (VendingMachineButton button in buttons)
            button.enabled = true;
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
            // fail, wrong item
        }
        else if (_currentInput == "B3")
        {
            // success
        }
        else
        {
            // invalid input
        }
    }
}

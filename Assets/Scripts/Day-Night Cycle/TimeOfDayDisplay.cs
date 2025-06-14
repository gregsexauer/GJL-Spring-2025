using UnityEngine;
using TMPro;

public class TimeOfDayDisplay : MonoBehaviour
{
    [SerializeField] TimeOfDayManager timeOfDayManager;
    [SerializeField] TextMeshProUGUI text;

    void Update()
    {
        text.text = timeOfDayManager.GetTimeString();
    }
}

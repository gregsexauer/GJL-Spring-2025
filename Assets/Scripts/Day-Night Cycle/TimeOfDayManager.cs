using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayManager : MonoBehaviour
{
    [field: SerializeField, Range(0, 24)] public float TimeOfDay { get; private set; }
    [SerializeField] LightingPreset preset;
    [SerializeField] Light directionalLight;
    [SerializeField] float timeMultiplier = .2f;
    public bool IsPaused { get; private set; }
    List<string> _reasonsForPausing = new();

    private void Update()
    {
        if (IsPaused) return;

        TimeOfDay += Time.deltaTime * timeMultiplier;
        TimeOfDay %= 24; // Clamp between 0-24
    }

    private void LateUpdate()
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(GetTimePercent());
        RenderSettings.fogColor = preset.FogColor.Evaluate(GetTimePercent());

        directionalLight.color = preset.DirectionalColor.Evaluate(GetTimePercent());
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((GetTimePercent() * 360) - 90, 170, 0));
    }

    float GetTimePercent()
    {
        return TimeOfDay / 24;
    }

    public string GetTimeString()
    {
        float time = TimeOfDay;
        string ampm = time < 12 ? "AM" : "PM";
        if (time >= 13)
            time -= 12;
        string t = System.TimeSpan.FromHours((double)time).ToString(@"\.hh\:mm").Replace(".", "") + " " + ampm;
        return t;
    }

    public void Pause(string reasonForPausing)
    {
        IsPaused = true;
        _reasonsForPausing.Add(reasonForPausing);
    }

    public void Unpause(string reasonForPausing)
    {
        _reasonsForPausing.Remove(reasonForPausing);
        if (_reasonsForPausing.Count == 0)
            IsPaused = false;
    }
}

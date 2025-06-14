using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Lighting Preset", menuName = "Scriptables/Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    [field: SerializeField] public Gradient AmbientColor { get; private set; }
    [field: SerializeField] public Gradient FogColor { get; private set; }
    [field: SerializeField] public Gradient DirectionalColor { get; private set; }
}
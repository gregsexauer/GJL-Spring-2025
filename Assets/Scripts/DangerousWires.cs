using UnityEngine;

public class DangerousWires : MonoBehaviour
{
    [SerializeField] ParticleSystem _particles1;
    [SerializeField] ParticleSystem _particles2;
    [SerializeField] Collider daveTrigger;

    public void TurnOffElectricity()
    {
        _particles1.Stop();
        _particles2.Stop();
        daveTrigger.enabled = false;
    }
}

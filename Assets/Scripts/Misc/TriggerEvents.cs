using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField] UnityEvent triggerEvent;
    [SerializeField] string triggerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(triggerTag))
        {
            triggerEvent.Invoke();
        }
    }
}

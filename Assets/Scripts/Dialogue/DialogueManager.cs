using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    DialogueRunner _runner;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _runner = GetComponent<DialogueRunner>();
    }

    public void StartDialogue(string nodeName)
    {
        _runner.StartDialogue(nodeName);
    }
}

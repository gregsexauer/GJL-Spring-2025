using UnityEngine;
using System.Collections.Generic;

public class Tallies : MonoBehaviour
{
    [SerializeField] List<TallySet> tallySets;

    private void Awake()
    {
        RunsCounter.runs++;
        int numFullTallies = RunsCounter.runs / 5;
        Debug.Log(numFullTallies);
        int incompleteTally = RunsCounter.runs % 5;
        Debug.Log(incompleteTally);

        foreach(TallySet set in tallySets)
        {
            set.SetTallies(0);
        }

        for (int i = 0; i < numFullTallies; i++)
        {
            tallySets[i].SetTallies(5);
        }

        tallySets[numFullTallies].SetTallies(incompleteTally);
    }
}

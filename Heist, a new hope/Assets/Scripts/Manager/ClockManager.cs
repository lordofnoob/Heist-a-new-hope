using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    ClockManager Instance;

    List<Sc_Action> ActionToPerform;
    public float tickInterval = 0.5f;
    void Awake()
    {
        Instance = this;

        ActionToPerform = new List<Sc_Action>();
        StartCoroutine(Sequencer());
    }

    IEnumerator Sequencer()
    {
        yield return new WaitForSeconds(tickInterval);

        PerformAction();

        StartCoroutine(Sequencer());
    }

    void PerformAction()
    {
        foreach(var action in ActionToPerform)
        {
            action.PerformAction(); ;
        }
        ActionToPerform.Clear();
    }

    public void AddActionToPerform(Sc_Action action)
    {
        ActionToPerform.Add(action);
    }
}

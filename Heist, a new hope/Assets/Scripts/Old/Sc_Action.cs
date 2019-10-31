using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Action : MonoBehaviour
{
    public int timeToPerform;

    public Sc_Action(int timeToPerform)
    {
        this.timeToPerform = timeToPerform;
    }

    public virtual void PerformAction() { }
}

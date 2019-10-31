﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Door : Mb_Trial
{
    public Animation door;

    public override void DoThings()
    {
        for (int i = 0; i < listOfUser.Count; i++)
        {
            listOfUser[i].state = Mb_Player.StateOfAction.Idle;
        }
        listOfUser.Clear();
        door.Play();
    }        
}
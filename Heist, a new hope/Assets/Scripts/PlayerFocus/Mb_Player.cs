﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Mb_Player : Mb_Poolable {

    public Sc_Charaspec characterProperty;
    //[SerializeField] NavMeshAgent agent;
    public Mb_Player player;
    public Color highlightedColor, selectedColor;

    [Header("Hostage")]
    public List<Mb_IAHostage> capturedHostages = new List<Mb_IAHostage>();


    [Header("Items")]
    public List<Sc_Items> itemsHold = new List<Sc_Items>();
        
    [HideInInspector] public bool highlighted = false;
    [HideInInspector] [SerializeField] private bool isSelected = false;

    [HideInInspector] public bool IsSelected { 
        set
        {
            switch (value)
            {
                case true:
                    isSelected = true;
                    ModifyOutlines(Outlines.Mode.OutlineAll, selectedColor, 7.5f);
                    SetOutlinesEnabled(true);
                    break;
                case false:
                    isSelected = false;
                    SetOutlinesEnabled(false);
                    break;
            }
        }
        get
        {
            return isSelected;
        }
    }


    [HideInInspector] public Mb_Trial onGoingInteraction;


    public Tile playerTile;
    public StateOfAction state;
    // nextInteractionToussa
    private Vector3 positionToGo;
    float distanceRemaining;

    void Update () 
    {
        CheckingDistance();
    }

    void OnMouseEnter()
    {
        highlighted = true;
        ModifyOutlines(Outlines.Mode.OutlineVisible, highlightedColor, 7.5f);
        SetOutlinesEnabled(true);
    }

    void OnMouseExit()
    {
        if (IsSelected)
        {
            ModifyOutlines(Outlines.Mode.OutlineVisible, selectedColor, 7.5f);
        }
        else
        {
            SetOutlinesEnabled(false);
        }
        highlighted = false;
    }

    void ModifyOutlines(Outlines.Mode mode, Color color, float width)
    {
        Outlines outline = gameObject.GetComponent<Outlines>();
        outline.OutlineMode = mode;
        outline.OutlineColor = color;
        outline.OutlineWidth = width;
    }

    void SetOutlinesEnabled(bool enabled)
    {
        Outlines outline = gameObject.GetComponent<Outlines>();
        outline.enabled = enabled;
    }

    public void MovePlayer(List<Tile> endPos, float stopDistance)
    {
        state = StateOfAction.Moving;
        //agent.SetDestination(endPos);
        //agent.stoppingDistance = stopDistance;
        //uniquement pour la next interaction n influe pas sur le deplacement whatsoever
        //positionToGo = endPos;
    }

    public void Interact()
    {
        state = StateOfAction.Interacting;
        if (onGoingInteraction.listOfUser.Count==0)
        {
            onGoingInteraction.listOfUser.Add(player);
            onGoingInteraction.StartInteracting();
        }
        else
            for (int i =0; i<onGoingInteraction.listOfUser.Count; i++)
            {
                if (onGoingInteraction.listOfUser[i] != player)
                {
                    onGoingInteraction.listOfUser.Add(player);
                    onGoingInteraction.ReUpduateTiming();
                }
            }
    }

    public void ResetInteractionParameters()
    {
        onGoingInteraction = null;
        distanceRemaining = 0;
        positionToGo = transform.position;
    }

    void CheckingDistance()
    {
        // /!\ TO CHANGE
        /*if (Vector3.Distance(transform.position, positionToGo) <= agent.stoppingDistance && onGoingInteraction != null && state == StateOfAction.Moving)
        {            
                Interact();
        }*/
    }

    public void AddItem(Sc_Items itemToAdd)
    {
        itemsHold.Add(itemToAdd);
    }

    public void DropItem(Sc_Items itemToDrop)
    {
        itemsHold.Remove(itemToDrop);
    }

    public void SetNextInteraction(Mb_Trial trialToUse)
    {
        onGoingInteraction = trialToUse;
    }

    public enum StateOfAction
    {
        Moving, Interacting, Captured, Idle
    }
}

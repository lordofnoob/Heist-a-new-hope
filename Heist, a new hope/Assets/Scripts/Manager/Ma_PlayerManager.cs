﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;

public class Ma_PlayerManager : MonoBehaviour
{
    public static Ma_PlayerManager Instance;

    public Mb_InputController InputController;

    public Mb_Player selectedPlayer = null;
    public int TickPerTileSpeed = 4;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (InputController.LeftClick)
        {
            Ray ray = Ma_CameraManager.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Mb_Player p = hit.transform.GetComponent<Mb_Player>();
                    if (p != selectedPlayer)
                        SelectPlayer(p);
                }
                else
                    DeselectPlayer();
            }
            
        }
        
        if (InputController.RightClick)
        {
            Ray ray = Ma_CameraManager.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Tile") && selectedPlayer != null && selectedPlayer.state != Mb_Player.StateOfAction.Captured)
                {
                    hit.point += new Vector3(Ma_LevelManager.Instance.FreePrefab.transform.localScale.x / 2, 0f, Ma_LevelManager.Instance.FreePrefab.transform.localScale.x / 2);

                    Vector3 gridPos = Vector3.zero;
                    gridPos.x = Mathf.Floor(hit.point.x / Ma_LevelManager.Instance.FreePrefab.transform.localScale.x) * Ma_LevelManager.Instance.FreePrefab.transform.localScale.x;
                    gridPos.z = Mathf.Floor(hit.point.z / Ma_LevelManager.Instance.FreePrefab.transform.localScale.x) * Ma_LevelManager.Instance.FreePrefab.transform.localScale.x;
                    if (selectedPlayer.onGoingInteraction != null)
                    {
                        selectedPlayer.onGoingInteraction.listOfUser.Remove(selectedPlayer);
                        selectedPlayer.onGoingInteraction.QuittingCheck();
                        selectedPlayer.onGoingInteraction = null;
                    }


                    selectedPlayer.MovePlayer(gridPos, 0f);
                    selectedPlayer.state = Mb_Player.StateOfAction.Moving;
                }
                else if (hit.transform.CompareTag("Trial")  && selectedPlayer !=null && selectedPlayer.state != Mb_Player.StateOfAction.Captured && selectedPlayer.state!= Mb_Player.StateOfAction.Interacting)
                {
                    Mb_Trial targetTrial = hit.transform.gameObject.GetComponent<Mb_Trial>();
                    if (selectedPlayer.onGoingInteraction != targetTrial)
                    {
                        Vector3 positionToAccomplishDuty = Vector3.zero;
                        if (targetTrial.listOfUser.Count > 0)
                            for (int i = 0; i < targetTrial.listOfUser.Count; i++)
                            {
                                if (targetTrial.listOfUser[i] != selectedPlayer)
                                {
                                    positionToAccomplishDuty = targetTrial.positionToGo[targetTrial.listOfUser.Count].position;
                                }
                            }
                        else
                            positionToAccomplishDuty = targetTrial.positionToGo[targetTrial.listOfUser.Count].position;

                        selectedPlayer.MovePlayer(positionToAccomplishDuty, 2f);
                        selectedPlayer.SetNextInteraction(targetTrial);
                    }
                    
                }
            }
            
        }

    }

    public void SelectPlayer(Mb_Player p)
    {
        //Debug.Log("SELECT PLAYER");
        if (selectedPlayer != null)
            selectedPlayer.IsSelected = false;
        selectedPlayer = p;
        p.IsSelected = true;
    }

    public void DeselectPlayer()
    {
        if(selectedPlayer != null)
        {
            //Debug.Log("DESELECT PLAYER");
            selectedPlayer.IsSelected = false;
            selectedPlayer = null;
        }
    }


}
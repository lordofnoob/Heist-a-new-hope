using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Deplacement : Sc_Action
{
    public List<Tile> path;
    public Mb_Player toMove;

    public Sc_Deplacement(int timeToPerform, Mb_Player toMove, List<Tile> path) : base(timeToPerform)
    {
        this.toMove = toMove;
        this.path = path;
    }

    public override void PerformAction()
    {
        /*toMove.transform.DOMove(new Vector3(path[0].transform.position.x, 0.5f, path[0].transform.position.z), LevelManager.Instance.clock.tickInterval * timeToPerform).SetEase(Ease.Linear).OnComplete(() =>
        {
            toMove.playerTile = path[0];
            Destroy(path[0].gameObject.GetComponent<Outlines>());
            path.RemoveAt(0);
            PerformAction();
        });*/
    }
}

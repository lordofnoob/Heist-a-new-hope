using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{

    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    public bool outlined = false;

    public int row, column;
    public float StraightLineDistanceToEnd, MinCostToStart;
    public bool Visited = false;
    public Tile previous;

    public object Connections { get; internal set; }

    public Tile(Vector3 position, float row, float column)
    {
        this.row = (int)row;
        this.column = (int)column;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        if (!outlined)
        {
            ModifyOutlines(Outlines.Mode.OutlineVisible, Color.black, 7.5f);
            SetOutlinesEnabled(true);

        }
    }

    void OnMouseExit()
    {
        if (!outlined)
            SetOutlinesEnabled(false);
    }

    public float StraightLineDistanceTo(Tile end)
    {
        //print((end.transform.position - transform.position).magnitude);
        return (end.transform.position - transform.position).magnitude;
    }

    public List<Tile> GetFreeNeighbours()
    {
        List<Tile> res = new List<Tile>();

        if(Ma_LevelManager.Instance.Grid.GetTile(row-1,column) is Free && row !=0)
        {
            res.Add(Ma_LevelManager.Instance.Grid.GetTile(row - 1, column));
        }

        if(Ma_LevelManager.Instance.Grid.GetTile(row+1,column) is Free && row != Ma_LevelManager.Instance.Grid.tilemap.GetLength(0))
        {
            res.Add(Ma_LevelManager.Instance.Grid.GetTile(row + 1, column));
        }

        if(Ma_LevelManager.Instance.Grid.GetTile(row,column-1) is Free && column != 0)
        {
            res.Add(Ma_LevelManager.Instance.Grid.GetTile(row, column-1));
        }

        if(Ma_LevelManager.Instance.Grid.GetTile(row,column+1) is Free && column != Ma_LevelManager.Instance.Grid.tilemap.GetLength(1))
        {
            res.Add(Ma_LevelManager.Instance.Grid.GetTile(row, column+1));
        }

        return res;
    }

    public void ModifyOutlines(Outlines.Mode mode, Color color, float width)
    {
        Outlines outline = gameObject.GetComponent<Outlines>();
        outline.OutlineMode = mode;
        outline.OutlineColor = color;
        outline.OutlineWidth = width;
    }

    public void SetOutlinesEnabled(bool enabled)
    {
        Outlines outline = gameObject.GetComponent<Outlines>();
        outline.enabled = enabled;
    }

}
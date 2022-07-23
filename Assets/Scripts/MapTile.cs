using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    GridClass<MapTile> grid;
    public int x, y;
    private Transform transform;

    public MapTile(GridClass<MapTile> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
    }

    public void ClearTransform()
    {
        transform = null;
    }

    public bool CanBuild()
    {
        return transform == null;
    }
}

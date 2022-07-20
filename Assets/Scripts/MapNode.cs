using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    GridClass<MapNode> grid;
    public int x, y;

    public MapNode(GridClass<MapNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
}

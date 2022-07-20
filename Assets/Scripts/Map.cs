using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    Vector3 origin = new Vector3(-8.9f, -5f);
    GridClass<GameObject> grid;

    public Map(int width, int height)
    {
        grid = new GridClass<GameObject>(width, height, 1f, origin, (GridClass<GameObject> g, int x, int y) => );
    }

    //Place gameobjects

    //Remove gameobjects

    //Return the type of gameobject (ground or turret)
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Vector3 origin;
    int width, height;
    GridClass<MapTile> grid;

    [SerializeField] private List<Transform> objectList;
    private Transform objectToPlace;

    void Awake()
    {
        width = 18;
        height = 10;
        origin = new Vector3(-8.9f, -5f); 

        grid = new GridClass<MapTile>(width, height, 1f, origin, (GridClass<MapTile> g, int x, int y) => new MapTile(g, x, y));

        objectToPlace = objectList[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(GetMousePosition(), out int x, out int y);
            MapTile mapTile = grid.GetGridObject(x, y);
            
            if (mapTile.CanBuild())
            {
                Transform builtTransform = Instantiate(objectToPlace, grid.GetWorldPosition(x, y), Quaternion.identity);
                mapTile.SetTransform(builtTransform);
            } else
            {
                Debug.Log("Cannot build here!");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectToPlace = objectList[0];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectToPlace = objectList[1];
        }
    }

    private Vector3 GetMousePosition()
    {
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
        return mouse = Camera.main.ScreenToWorldPoint(mouse);
    }
}

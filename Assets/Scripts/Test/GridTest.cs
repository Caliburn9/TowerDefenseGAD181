using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    private GridClass<int> grid;
    int width, height;
    float cellSize;

    // Start is called before the first frame update
    void Start()
    {
        width = 18;
        height = 10;
        cellSize = 1f;
        grid = new GridClass<int>(width, height, cellSize, new Vector3(-8.9f, -5f), (GridClass<int> g, int x, int y) => new int());
    }

    // Update is called once per frame
    void Update()
    {
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        if (Input.GetMouseButtonDown(0))
        {
            grid.SetGridObject(mouse, 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(mouse));
        }
    }
}

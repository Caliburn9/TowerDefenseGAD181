using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    Pathfinding pathfind;

    void Awake()
    {
        pathfind = new Pathfinding(18, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pathfind.GetGrid().GetXY(GetMousePosition(), out int x, out int y);
            pathfind.GetNode(x, y).SetIsWalkable(!pathfind.GetNode(x, y).isWalkable);
            Debug.Log("Walkable variable has changed");
        }
    }

    private Vector3 GetMousePosition()
    {
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
        return mouse = Camera.main.ScreenToWorldPoint(mouse);
    }
}

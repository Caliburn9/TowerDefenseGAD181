using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    Pathfinding pathfind;
    GameObject[] ground;

    void Awake()
    {
        pathfind = new Pathfinding(18, 10);
        ground = GameObject.FindGameObjectsWithTag("Ground");
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

    void CreatePath()
    {
        for (int xx = 0; xx < pathfind.GetGrid().GetWidth(); xx++)
        {
            for (int yy = 0; yy < pathfind.GetGrid().GetHeight(); yy++)
            {
                Vector2 checkingPosition = new Vector2(xx, yy);

                for (int i = 0; i < ground.Length - 1; i++)
                {
                    //if the xx and yy position contains a "Ground object"
                    if ((Vector2)ground[i].transform.position == checkingPosition)
                    {
                        pathfind.GetGrid().GetXY(checkingPosition, out int x, out int y);
                        pathfind.GetNode(x, y).SetIsWalkable(!pathfind.GetNode(x, y).isWalkable);
                    }
                }
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
        return mouse = Camera.main.ScreenToWorldPoint(mouse);
    }
}

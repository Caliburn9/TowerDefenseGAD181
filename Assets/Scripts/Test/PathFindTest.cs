using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindTest : MonoBehaviour
{
    Pathfinding pathfind;
    public EnemyAI enemy;

    void Awake()
    {
        pathfind = new Pathfinding(18, 10);
    }

    // Update is called once per frame
    void Update()
    {
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        if (Input.GetMouseButtonDown(0))
        {
            pathfind.GetGrid().GetXY(mouse, out int x, out int y);
            //List<PathNode> path = pathfind.FindPath(0, 0, x, y);
            //if (path != null)
            //{
            //    for (int i = 0; i < path.Count - 1; i++)
            //    {
            //        Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f + Vector3.one * .5f, new Vector3(path[i + 1].x, path[i + 1].y) * 1f + Vector3.one * .5f, Color.green, 1f);
            //        //Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f, new Vector3(path[i+1].x, path[i+1].y) * 1f);
            //    }
            //}

            enemy.SetTargetPosition(mouse);
            //pathfind.GetGrid().GetXY(mouse, out int x, out int y);
            Debug.Log(x + "," + y);
        }

        if (Input.GetMouseButtonDown(1))
        {
            pathfind.GetGrid().GetXY(mouse, out int x, out int y);
            pathfind.GetNode(x, y).SetIsWalkable(!pathfind.GetNode(x, y).isWalkable);
            Debug.Log("Walkable variable has changed");
        }
    }
}

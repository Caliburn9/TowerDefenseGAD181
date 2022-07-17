using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public const int HORIZONTAL_COST = 10;
    public const int DIAGONAL_COST = 14;

    //Vector3.zero* grid.GetCellSize() * .5f
    Vector3 origin = new Vector3(-8.9f, -5f);

    public static Pathfinding Instance { get; private set; }

    GridClass<PathNode> grid;
    List<PathNode> openList, closedList;

    public Pathfinding(int width, int height)
    {
        Instance = this;
        grid = new GridClass<PathNode>(width, height, 1f, origin, (GridClass<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public GridClass<PathNode> GetGrid()
    {
        return grid;
    }

    public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
    {
        grid.GetXY(startPos, out int startX, out int startY);
        grid.GetXY(endPos, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);

        if (path == null)
        {
            return null;
        } 
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + origin + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }
 
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode>() { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currNode = GetLowestFCostNode(openList);
            if (currNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currNode);
            closedList.Add(currNode);

            foreach (PathNode neighborNode in GetNeightborList(currNode))
            {
                if (closedList.Contains(neighborNode))
                {
                    continue;
                }

                if (!neighborNode.isWalkable)
                {
                    closedList.Add(neighborNode);
                    continue;
                }

                int tentativeGCost = currNode.gCost + CalculateDistance(currNode, neighborNode);
                if (tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.previousNode = currNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistance(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        //Out of nodes on open list
        return null;
    }

    private List<PathNode> GetNeightborList(PathNode currentNode)
    {
        List<PathNode> neighborList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            //Left
            neighborList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //Left Down
            if (currentNode.y - 1 >= 0) {
                neighborList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            }
            //Left Up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                neighborList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }
        }

        if (currentNode.x + 1 < grid.GetWidth())
        {
            //Right
            neighborList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //Right Down
            if (currentNode.y - 1 >= 0)
            {
                neighborList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            }
            //Right Up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                neighborList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }
        }
        //Down
        if (currentNode.y - 1 >= 0)
        {
            neighborList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }
        //Up
        if (currentNode.y + 1 < grid.GetHeight())
        {
            neighborList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }

        return neighborList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.previousNode != null)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();
        return path;
    }

    public int CalculateDistance(PathNode a, PathNode b)
    {
        int xDist = Mathf.Abs(a.x - b.x);
        int yDist = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDist - yDist);
        return DIAGONAL_COST * Mathf.Min(xDist, yDist) + HORIZONTAL_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}

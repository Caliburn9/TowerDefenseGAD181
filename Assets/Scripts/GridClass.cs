using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridClass<TGridObject>
{
    public int width, height;
    public float cellSize;
    private Vector3 originPosition;
    public TGridObject[,] gridArray; //Multi-dimensional array with two dimensions

    public GridClass(int width, int height, float cellSize, Vector3 originPosition, Func<GridClass<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        //Initialize grid
        for (int x = 0; x < GetWidth(); x++)
        {
            for (int y = 0; y < GetHeight(); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        //Debug to draw the grid
        bool showDebug = true;

        if (showDebug)
        {
            for (int x = 0; x < GetWidth(); x++)
            {
                for (int y = 0; y < GetHeight(); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }

    public int GetWidth()
    {
        return gridArray.GetLength(0);
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public int GetHeight()
    {
        return gridArray.GetLength(1);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 WorldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((WorldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((WorldPosition - originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public void SetGridObject(Vector3 WorldPosition, TGridObject value)
    {
        int x, y;
        GetXY(WorldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        } else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 WorldPosition)
    {
        int x, y;
        GetXY(WorldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
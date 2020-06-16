using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int tileType;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;

    public Node parent;
    public Vector2 worldPosition;

    public Node(int _tileType, Vector2  _worldPosition, int _gridX, int _gridY){
        tileType = _tileType;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}

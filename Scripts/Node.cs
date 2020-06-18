using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public int tileType;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;

    public Node parent;
    public Vector2 worldPosition;

    int heapIndex;

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

	public int HeapIndex {
		get {
			return heapIndex;
		}
		set {
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare) {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}

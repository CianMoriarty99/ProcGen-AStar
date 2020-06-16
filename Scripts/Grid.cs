using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;

    public LayerMask walkableMask;
    public LayerMask unwalkableMask;
    public Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start(){
        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
        CreateGrid();
    }

    void CreateGrid(){
        grid = new Node[gridSizeX,gridSizeY];
        Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x/2 - Vector2.up * gridWorldSize.y/2;

        for (int x = 0; x < gridSizeX; x++){
			for (int y = 0; y < gridSizeY; y++){
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter) + Vector2.up * (y * nodeDiameter); 
                int tileType = 0;
                
                if (Physics2D.OverlapCircle(worldPoint, 0.1f, walkableMask)) tileType = 1;
                if (Physics2D.OverlapCircle(worldPoint, 0.1f, unwalkableMask)) tileType = 2;
                

                //Debug.Log("Creating Node with tileType: " + tileType + " and worldPoint: " + worldPoint);
                grid[x,y] = new Node(tileType, worldPoint, x, y);


            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x,y];
    }

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}

    public List<Node> path;

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, gridWorldSize);

        if (grid != null){
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in grid){
                if (n.tileType == 2) Gizmos.color = Color.red;
                else if (n.tileType == 1) Gizmos.color = Color.white;
                else Gizmos.color = Color.black;
                if(playerNode == n){
                    Gizmos.color = Color.blue;
                }
                if(path != null){
                    if (path.Contains(n))
                        Gizmos.color = Color.yellow;
                }
                Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter));
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private MazeNode nodePrefab;

    [SerializeField]
    private MazePlayer playerPrefab;

    [SerializeField]
    private GameObject destPrefab;

    public void SpawnMaze(Vector2Int size, List<MazeNodeState> maze)
    {
        int r = size.x;
        int c = size.y;
        for (int i = 0;i < r;i++)
        {
            for (int j = 0;j < c;j++)
            {
                Vector3 pos = new Vector3(-r/2 + i, -c/2 + j, 0);
                MazeNodeState nodeState = maze[i*c+j];
                MazeNode node = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                node.SetState(nodeState);
            }
        }
    }

    public MazePlayer SpawnPlayer(Vector2Int size, int index)
    {
        int x = index / size.y;
        int y = index % size.y;
        Vector3 pos = new Vector3(-size.x/2 + x, -size.y/2 + y, -1);
        MazePlayer player = Instantiate(playerPrefab, pos, Quaternion.identity, transform);
        player.SetPosition(x, y);
        GameObject.Find("PlayerPosition").transform.position = pos;
        return player;
    }

    public void SpawnDestination(Vector2Int size, int index)
    {
        int x = index / size.y;
        int y = index % size.y;
        Vector3 pos = new Vector3(-size.x/2 + x, -size.y/2 + y, -0.5f);
        Instantiate(destPrefab, pos, Quaternion.identity, transform);
    }

}

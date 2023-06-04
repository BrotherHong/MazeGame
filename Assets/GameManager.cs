using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;

    [SerializeField]
    private MazeRenderer mazeRenderer;

    [SerializeField]
    private float timeToMove = 0.2f;

    private List<MazeNodeState> maze;

    private MazePlayer player;
    private Vector2Int startPos;
    private Vector2Int destPos;

    void Start()
    {
        maze = MazeGenerator.Generate(size);

        int rStartIndex = Random.Range(0, maze.Count);
        startPos = new Vector2Int(rStartIndex/size.y, rStartIndex%size.y);
        int rDestIndex;
        do {
            rDestIndex = Random.Range(0, maze.Count);
            destPos = new Vector2Int(rDestIndex/size.y, rDestIndex%size.y);
        } while (rStartIndex == rDestIndex);

        foreach (int index in MazeSolver.Solve(maze, size, startPos, destPos))
        {
            maze[index].SetPath(true);
        }

        // draw
        mazeRenderer.SpawnMaze(size, maze);
        player = mazeRenderer.SpawnPlayer(size, rStartIndex);
        mazeRenderer.SpawnDestination(size, rDestIndex);
    }

    void Update()
    {
        Vector2Int curPos = player.GetPosition();
        if (curPos.Equals(destPos))
        {
            // restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // up 0
        if (Input.GetKey(KeyCode.UpArrow) && !player.Moving())
        {
            
            int curIndex = curPos.x*size.y + curPos.y;
            if (maze[curIndex].HasWall(0))
                return;
            StartCoroutine(player.Move(Vector3.up, timeToMove));
        }
        // right 1
        if (Input.GetKey(KeyCode.RightArrow) && !player.Moving())
        {
            int curIndex = curPos.x*size.y + curPos.y;
            if (maze[curIndex].HasWall(1))
                return;
            StartCoroutine(player.Move(Vector3.right, timeToMove));
        }
        // down 2
        if (Input.GetKey(KeyCode.DownArrow) && !player.Moving())
        {
            int curIndex = curPos.x*size.y + curPos.y;
            if (maze[curIndex].HasWall(2))
                return;
            StartCoroutine(player.Move(Vector3.down, timeToMove));
        }
        
        // left 3
        if (Input.GetKey(KeyCode.LeftArrow) && !player.Moving())
        {
            int curIndex = curPos.x*size.y + curPos.y;
            if (maze[curIndex].HasWall(3))
                return;
            StartCoroutine(player.Move(Vector3.left, timeToMove));
        }
    }

}

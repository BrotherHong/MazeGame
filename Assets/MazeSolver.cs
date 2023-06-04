using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeSolver
{
    public static List<int> Solve(List<MazeNodeState> maze, Vector2Int size, Vector2Int start, Vector2Int dest)
    {
        // Debug.Log("Hi 4");
        List<int> path = new List<int>();
        List<int> record = new List<int>();
        List<bool> vis = new List<bool>();
        // initialize
        for (int i = 0;i < size.x*size.y;i++)
        {   
            record.Add(-1);
            vis.Add(false);
        }
        int[] dir = {0, 1, 0, -1, 0};

        // BFS initialize
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        int startIndex = PosToIndex(start, size);
        int destIndex = PosToIndex(dest, size);
        q.Enqueue(start);
        vis[startIndex] = true;
        path.Add(startIndex);

        // BFS logic
        while (q.Count > 0)
        {
            // Debug.Log("Hi 3");
            int qsize = q.Count;
            while (qsize-- > 0)
            {
                Vector2Int curPos = q.Dequeue();
                int curIndex = PosToIndex(curPos, size);
                MazeNodeState curNode = maze[curIndex];

                for (int d = 0;d < 4;d++)
                {
                    Vector2Int tmpPos = curPos + new Vector2Int(dir[d], dir[d+1]);
                    int tmpIndex = PosToIndex(tmpPos, size);
                    if (InsideRange(tmpPos, size) && !curNode.HasWall(d) && !vis[tmpIndex])
                    {
                        vis[tmpIndex] = true;
                        record[tmpIndex] = d;
                        q.Enqueue(tmpPos);
                    }
                }
            }

            if (vis[destIndex])
            {
                // Debug.Log("Hi");
                Vector2Int trace = dest;
                while (!trace.Equals(start))
                {
                    int tIndex = PosToIndex(trace, size);
                    path.Add(tIndex);
                    trace += new Vector2Int(-dir[record[tIndex]], -dir[record[tIndex]+1]);
                }
                // Debug.Log("Hi 2");
                break;
            }
            
        }
        // foreach (int index in path)
        // {
        //     maze[index].SetPath(true);
        // }
        return path;
    }

    private static int PosToIndex(Vector2Int p, Vector2Int size)
    {
        return p.x * size.y + p.y;
    }

    private static bool InsideRange(Vector2Int p, Vector2Int size) 
    {
        return (0<=p.x&&p.x<size.x) && (0<=p.y&&p.y<size.y);
    }

}

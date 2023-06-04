using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeGenerator
{
    public static List<MazeNodeState> Generate(Vector2Int size)
    {
        int r = size.x, c = size.y;
        List<MazeNodeState> maze = new List<MazeNodeState>();
        List<bool> vis = new List<bool>();

        for (int i = 0;i < r;i++) 
        {
            for (int j = 0;j < c;j++) 
            {
                maze.Add(new MazeNodeState());
                vis.Add(false);
            }
        }

        int[] dir = {0, 1, 0, -1, 0};
        Stack<int> stk = new Stack<int>();
        int start = Random.Range(0, maze.Count);

        vis[start] = true;
        stk.Push(start); // choose start point

        while (stk.Count > 0) 
        {
            int curPos = stk.Peek();
            MazeNodeState curNode = maze[curPos];

            // Find all next possible node
            List<int> psbNextPos = new List<int>();
            List<int> psbCurDir = new List<int>();
            int x = curPos / c;
            int y = curPos % c;

            for (int d = 0;d < 4;d++)
            {
                int xx = x+dir[d], yy = y+dir[d+1];
                int pNextPos = GetPos(size, xx, yy);
                if (InsideRange(xx, yy, r, c) && !vis[pNextPos]) 
                {
                    psbNextPos.Add(pNextPos);
                    psbCurDir.Add(d);
                }
            }

            // random choose next direction
            if (psbNextPos.Count > 0) 
            {
                int nextIndex = Random.Range(0, psbNextPos.Count);
                int nextPos = psbNextPos[nextIndex];
                int curDir = psbCurDir[nextIndex];
                int nextDir = GetOppsite(curDir);

                vis[nextPos] = true;
                stk.Push(nextPos);
                maze[curPos].RemoveWall(curDir);
                maze[nextPos].RemoveWall(nextDir);
            }
            else
            {
                stk.Pop();
            }
        }
        return maze;
    }

    private static int GetPos(Vector2Int size, int r, int c)
    {
        return r*size.y + c;
    }

    private static bool InsideRange(int xx, int yy, int r, int c) 
    {
        return (0<=xx&&xx<r) && (0<=yy&&yy<c);
    }

    private static int GetOppsite(int dir)
    {
        switch (dir)
        {
            case 0: return 2;
            case 1: return 3;
            case 2: return 0;
            case 3: return 1;
            default: return 0;
        }
    }
}

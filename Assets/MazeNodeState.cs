using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNodeState
{
    private bool[] walls = {true, true, true, true};
    private bool path = false;

    public void RemoveWall(int dir) 
    {
        walls[dir] = false;
    }

    public bool HasWall(int dir)
    {
        return walls[dir];
    }

    public void SetPath(bool path)
    {
        this.path = path;
    }

    public bool IsPath()
    {
        return path;
    }
}

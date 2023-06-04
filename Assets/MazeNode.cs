using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : MonoBehaviour
{
    [SerializeField]
    // Tag of wall : 0 top, 1 right, 2 down, 3 left
    private GameObject[] walls;

    [SerializeField]
    private SpriteRenderer floor;

    public void SetState(MazeNodeState state)
    {
        for (int i = 0;i < 4;i++)
        {
            walls[i].SetActive(state.HasWall(i));
        }
        if (state.IsPath())
        {
            floor.color = Color.cyan;
        }
    }
}

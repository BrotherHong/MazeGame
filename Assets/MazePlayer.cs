using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    private bool isMoving;

    private int curX, curY;

    private GameObject playerPosition;

    void Start()
    {
        playerPosition = GameObject.Find("PlayerPosition");
    }

    public IEnumerator Move(Vector3 direction, float timeToMove)
    {
        curX += (int)direction.x;
        curY += (int)direction.y;
        isMoving = true;

        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            playerPosition.transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        playerPosition.transform.position = targetPos;

        isMoving = false;
    }

    public bool Moving()
    {
        return isMoving;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(curX, curY);
    }

    public void SetPosition(int x, int y)
    {
        curX = x;
        curY = y;
    }
}

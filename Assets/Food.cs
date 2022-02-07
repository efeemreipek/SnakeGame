using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int xBound = 15;
    private int yBound = 10;

    private void Start()
    {
        SetRandomPosition();
    }

    private void SetRandomPosition()
    {
        int randomX = Random.Range(-xBound, xBound + 1);
        int randomY = Random.Range(-yBound, yBound + 1);

        transform.position = new Vector3(randomX, randomY, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snake"))
        {
            SetRandomPosition();
        }
    }
}

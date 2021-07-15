using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    SnakeMovement snakeMovement;
    void Start()
    {
        snakeMovement = GetComponent<SnakeMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.CompareTag("SegmentSnake") || collision.rigidbody.CompareTag("Obstacle"))
        {
           Destroy(gameObject);
           GameManager.Instance.GameOver();
        }
        if (collision.rigidbody.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            snakeMovement.AddSegment();
            if (snakeMovement.GetLengthOfSnake() >= 10)
                GameManager.Instance.LevelComplete();
        }
    }
}

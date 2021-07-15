using UnityEngine;

public class DestroyFood : MonoBehaviour
{
    [SerializeField] float _timeToDestruction;
    void Start()
    {
        Destroy(gameObject, _timeToDestruction);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.CompareTag("SegmentSnake") || collision.rigidbody.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

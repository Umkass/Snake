using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] float _speed = 0.66f; //пол клетки в секунду
    float moveRate = 1f;
    Rigidbody2D _rb;
    [SerializeField] GameObject segmentPrefab;
    List<Transform> segmentPositions = new List<Transform>();
    float xOffset = 2f;
    float yOffset = 1.5f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        HUD.Instance.OnLeft += RotateLeft;
        HUD.Instance.OnRight += RotateRight;
        AddSegment();
        InvokeRepeating("MoveForward", moveRate, moveRate);
    }
    void MoveForward()
    {
        Vector3 lastPos = transform.position;
        if (transform.eulerAngles.z == -90.00001) //безуспешные попытки избежать случайного столкновения
        {
            lastPos.x += xOffset;
        }
        if (gameObject.transform.eulerAngles.z == 90.00001)
        {
            lastPos.x -= xOffset;
        }
        if (gameObject.transform.eulerAngles.z == 0)
        {
            lastPos.y -= yOffset;
        }
        if (gameObject.transform.eulerAngles.z == 180)
        {
            lastPos.y += yOffset;
        }
        _rb.velocity = _speed * transform.up;
        _rb.MovePosition(_rb.position + _rb.velocity);
        StartCoroutine(SpawnSnakeCoroutine(lastPos)); //безуспешная попытка №2
    }

    void SegmentMove(Vector3 lastPos)
    {
        segmentPositions.Last().position = lastPos;
        segmentPositions.Insert(0, segmentPositions.Last());
        segmentPositions.RemoveAt(segmentPositions.Count - 1);
    }

    IEnumerator SpawnSnakeCoroutine(Vector3 lastPos)
    {
        yield return new WaitForSeconds(1f);
        SegmentMove(lastPos);
        yield break;
    }
    public void AddSegment()
    {
        Vector2 newSegmentPos;
        if (segmentPositions.Count >= 1)
            newSegmentPos = segmentPositions.Last().position;
        else
        {
            newSegmentPos = transform.position;
        }
        if (transform.eulerAngles.z == -90.00001) //это тоже попытка "при создании"
        {
            newSegmentPos.x += xOffset;
        }
        if (gameObject.transform.eulerAngles.z == 90.00001)
        {
            newSegmentPos.x -= xOffset;
        }
        if (gameObject.transform.eulerAngles.z == 0)
        {
            newSegmentPos.y -= yOffset;
        }
        if (gameObject.transform.eulerAngles.z == 180)
        {
            newSegmentPos.y += yOffset;
        }
        GameObject newSegment = Instantiate(segmentPrefab, newSegmentPos, Quaternion.identity);
        segmentPositions.Add(newSegment.transform);
        _speed *= 1.1f;
    }
    public int GetLengthOfSnake()
    {
        return segmentPositions.Count;
    }
    void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + 90f);
        //points.Add(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
    }
    void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90f);
        //points.Add(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
    }

    void OnDestroy()
    {
        if (HUD.Instance != null)
        {
            HUD.Instance.OnLeft -= RotateLeft;
            HUD.Instance.OnRight -= RotateRight;
        }
    }
}

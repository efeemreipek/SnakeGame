using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private int xBound = 15;
    private int yBound = 10;

    private Vector2 direction = Vector2.right;

    private Transform _transform;

    private List<Transform> _segments = new List<Transform>();

    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private Text scoreText;
    [SerializeField] private int initialSize = 4;

    private void Start()
    {
        _transform = transform;

        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A))
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D))
            direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }


        _transform.position = new Vector3(Mathf.Round(_transform.position.x) + direction.x, Mathf.Round(_transform.position.y) + direction.y, 0f);

        if (_transform.position.x > xBound)
            _transform.position = new Vector3(-xBound, _transform.position.y, 0f);
        if(_transform.position.x < -xBound)
            _transform.position = new Vector3(xBound, _transform.position.y, 0f);
        if (_transform.position.y > yBound)
            _transform.position = new Vector3(_transform.position.x, -yBound, 0f);
        if (_transform.position.y < -yBound)
            _transform.position = new Vector3(_transform.position.x, yBound, 0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
        scoreText.text = $"Score: {_segments.Count - initialSize}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
            Grow();
        else if (collision.CompareTag("Snake"))
            ResetState();
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(_transform);

        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }

        _transform.position = Vector3.zero;

        scoreText.text = $"Score: {_segments.Count - initialSize}";
    }

}

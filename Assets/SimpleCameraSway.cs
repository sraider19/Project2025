using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCameraSway : MonoBehaviour
{
    public float shakeMagnitude = 0.1f;
    public float shakeFrequency = 1f;
    public float movementDecay = 0.5f;

    private Vector3 _initialPosition;
    private float _movementTimer;
    private Vector3 _targetPosition;

    void Start()
    {
        _initialPosition = transform.position;
        _movementTimer = 0f;
    }

    void Update()
    {
        if (_movementTimer <= 0)
        {
            _targetPosition = _initialPosition + Random.insideUnitSphere * shakeMagnitude;
            _movementTimer = shakeFrequency;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, movementDecay * Time.deltaTime);
            _movementTimer -= Time.deltaTime;
        }
    }
}

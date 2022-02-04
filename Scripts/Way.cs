using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private float _minVelocity;
    [SerializeField] private float _maxVelocity;

    private GameOver _gameOver;

    private void Awake()
    {
        _gameOver = GameObject.FindObjectOfType<GameOver>();
    }

    private void OnEnable()
    {
        _gameOver.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        _gameOver.GameEnded -= OnGameEnded;
    }

    private void OnGameEnded()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        //rb.angularDrag = Random.Range(0, 180);
        rb.AddExplosionForce(Random.Range(_minVelocity, _maxVelocity), transform.position, 10f, Random.Range(_minVelocity, _maxVelocity), ForceMode.Impulse);
    }
}

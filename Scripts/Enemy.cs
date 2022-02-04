using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dyingSystem;
    [SerializeField] private GameObject _modelObject;    

    private float _speed;
    private float _rotationTime;
    private int _health;
    private int _award;
    private Transform _currentPoint;
    private Wallet _wallet;
    private Spawner _spawner;
    private bool _isDead = false;

    public bool IsDead => _isDead;

    private void Start()
    {
        _spawner = GameObject.FindObjectOfType<Spawner>();
        _wallet = GameObject.FindObjectOfType<Wallet>();

        _currentPoint = _spawner.GetNextPoint(0);
    }

    private void Update()
    {
        if (_isDead == false)
        {
            Vector3 direction = transform.position - _currentPoint.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationTime * Time.deltaTime);

            Vector3 targetPosition = Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);
            transform.position = targetPosition;
        }    
    }

    public void Init(float speed, float rotationTime, int health, int award)
    {
        _speed = speed;
        _rotationTime = rotationTime;
        _health = health;
        _award = award;
    }

    public void ApplyDamage(int value)
    {
        _health -= value;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void SetMovePoint(Transform point)
    {
        _currentPoint = point;
    }

    public void Die()
    {
        _isDead = true;
        _modelObject.SetActive(false);
        _dyingSystem.Play();
        _wallet.ChangeMoney(_award);
        _spawner.RemoveEnemyFromList(this);

        Destroy(gameObject, 2f);
    }
}

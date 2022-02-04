using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSimple : Bullet
{
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _dyingSystem;

    private Enemy _target = null;
    private float _duyingTime = 5f;
    private float _elapsedTime = 0f;

    private void Update()
    {
        if (_target != null)
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            transform.LookAt(_target.transform);
            transform.position = targetPosition;

        }
        else
        {
            Die();
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _duyingTime)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.IsDead == false)
            {
                enemy.ApplyDamage(Damage);
            }

            Die();
        }
    }

    public Enemy GetTarget()
    {
        return _target;
    }

    public void SetTarget(Enemy enemy)
    {
        _target = enemy;
    }

    private void Die()
    {
        if (_dyingSystem != null)
        {
            ParticleSystem dyingSystem = Instantiate(_dyingSystem);
            dyingSystem.transform.position = transform.position;
            dyingSystem.Play();

            Destroy(dyingSystem, 2f);
        }

        Destroy(gameObject);
    }
}

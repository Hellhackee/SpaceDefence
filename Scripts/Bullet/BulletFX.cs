using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFX : Bullet
{
    private float _duyingTime = 4f;
    private float _elapsedTime = 0f;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _duyingTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Destroy(gameObject,2f);
            
            if (enemy.IsDead == false)
            {
                enemy.ApplyDamage(Damage);
            }
        }
    }
}

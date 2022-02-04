using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _rotationTime;
    [SerializeField] private bool _attachBullets;

    private TowerPlace _towerPlace = null;
    private int _damage;
    private float _cooldown;
    private float _range;
    private int _ammo;
    private int _instantiatedBullets = 0;
    private float _elapsedTime = 0f;
    private Bullet _bullet = null;
    private Enemy _target = null;
    private Enemy _previousTarget = null;

    public void Init(TowerPanelSO so)
    {
        _damage = so.Damage;
        _cooldown = so.Cooldown;
        _range = so.Range;
        _ammo = so.Ammo;
        _bullet = so.BulletPrefab;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _cooldown)
        {
            if (_target != null)
            {
                if (_target.IsDead)
                {
                    _target = null;
                }
            }

            if (_target == null)
            {
                RaycastHit[] rays = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), _range, Vector3.one, _range / 2);

                Dictionary<Enemy, float> enemies = new Dictionary<Enemy, float>();

                foreach (var ray in rays)
                {
                    if (ray.collider.TryGetComponent(out Enemy enemy))
                    {
                        if (enemy.IsDead == false)
                        {
                            enemies.Add(enemy, Vector3.Distance(transform.position, enemy.transform.position));
                        }
                    }
                }

                if (enemies.Count > 0)
                {
                    _target = enemies.OrderBy(x => x.Value).First().Key;
                }
            }
            
            if (_target != null)
            {
                _elapsedTime = 0f;

                Vector3 relativePos = _target.transform.position - transform.position;
                Quaternion rotatePos = Quaternion.LookRotation(relativePos);
                Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, rotatePos.eulerAngles.y - 90, transform.eulerAngles.z);
                transform.rotation = rotation;

                DestoyPreviuosBullets(_target);
                Attack(_target);
            }
        }

        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > _range)
            {
                _target = null;
                DestoyPreviuosBullets(_target);
            }
        }
    }

    private void Attack(Enemy enemy)
    {
        Bullet bullet = null;

        if (_attachBullets == true)
        {
            bullet = Instantiate(_bullet, transform);
        }
        else
        {
            bullet = Instantiate(_bullet);
        }
        
        bullet.transform.position = _attackPoint.position;        
        bullet.SetDamage(_damage);
        _instantiatedBullets++;

        if (bullet.TryGetComponent(out BulletSimple bulletSimple))
        {
            bulletSimple.SetTarget(enemy);
        }

        if (_towerPlace != null)
        {
            float instantiatedBulletsPersent = (float)_instantiatedBullets / (float)_ammo;
            _towerPlace.ChangeSliderValue(instantiatedBulletsPersent);

            if (_ammo == _instantiatedBullets)
            {
                _towerPlace.ChangeSliderValue(0f);
                _towerPlace.SetBusyStatus(false);
                Destroy(gameObject);
                DestoyPreviuosBullets(null);
            }
        }
    }

    private void DestoyPreviuosBullets(Enemy target)
    {
        if (target != _previousTarget)
        {
            var previousBullets = gameObject.GetComponentsInChildren<Bullet>();

            foreach (Bullet bullet in previousBullets)
            {
                if (bullet.DestroyOnEnemyChange)
                {
                    Destroy(bullet.gameObject);
                }
            }

            _previousTarget = target;
        }
    }

    public void SetTowerPlace(TowerPlace towerPlace)
    {
        _towerPlace = towerPlace;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _range);
    }
}

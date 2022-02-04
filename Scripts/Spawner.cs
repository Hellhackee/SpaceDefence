using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _dencity;
    [SerializeField] private float _cooldown;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private GameOver _gameOver;

    public event UnityAction<int, int> EnemySpawned;
    public event UnityAction AllEnemiesDied;
    public int Dencity => _dencity;

    private float _elapsedTime = 0f;
    private int _enemiesSpawned = 0;
    private List<Enemy> _enemies = new List<Enemy>();
    private bool _gameEnded = false;

    [Header("Enemy settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationTime;
    [SerializeField] private int _health;
    [SerializeField] private int _award;

    private void OnEnable()
    {
        _gameOver.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        _gameOver.GameEnded -= OnGameEnded;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_enemiesSpawned < _dencity)
        {
            if (_elapsedTime >= _cooldown)
            {
                Enemy enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], this.transform);
                enemy.transform.position = transform.position;
                enemy.Init(_speed, _rotationTime, _health, _award);

                _enemiesSpawned++;
                _enemies.Add(enemy);
                EnemySpawned?.Invoke(_enemiesSpawned, _dencity);

                _elapsedTime = 0f;
            }
        }    
    }

    public Transform GetNextPoint(int index)
    {
        if (index < _movePoints.Length)
        {
            return _movePoints[index];
        }

        return null;
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count == 0 && _enemiesSpawned == _dencity)
        {
            
            if (_gameEnded == false)
            {
                AllEnemiesDied?.Invoke();
            }
        }
    }

    private void OnGameEnded()
    {
        _gameEnded = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private GameObject _healthImage;
    [SerializeField] private Transform _container;
    [SerializeField] private FinishLaser _finishLaser;
    [SerializeField] private Transform _laserTarget;
    [SerializeField] private GameObject _finishButton;
    [SerializeField] private GameOver _gameOver;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GameObject.FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        for (int i = 0; i < _health; i++)
        {
            Instantiate(_healthImage, _container);
        }
    }

    private void OnEnable()
    {
        _spawner.AllEnemiesDied += OnAllEnemiesDied;
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesDied -= OnAllEnemiesDied;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _health--;

            if (_health > 0)
            {
                UpdateHealthBar(_health);
            }

            enemy.Die();

            if (_health == 0)
            {
                _gameOver.EndGame();
            }
            else
            {
                FinishLaser finishLaser = Instantiate(_finishLaser, transform);
                finishLaser.SetTargetPosition(_laserTarget);
                Destroy(finishLaser.gameObject, 5f);
            }  
        }
    }

    private void UpdateHealthBar(int health)
    {
        int childCount = _container.GetChildCount();

        int healthToSpawn = health - childCount;

        if (healthToSpawn > 0)
        {
            for (int i = 0; i < healthToSpawn; i++)
            {
                Instantiate(_healthImage, _container);
            }
        }
        else
        {
            for (int i = childCount; i > childCount + healthToSpawn; i--)
            {
                Transform healthIcon = _container.GetChild(i - 1);
                Destroy(healthIcon.gameObject);
            }
        }
    }

    private void OnAllEnemiesDied()
    {
        _finishButton.SetActive(true);
        
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("Level", currentIndex + 1);
        }
    }
}

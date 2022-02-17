using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _progressPanel;
    [SerializeField] private GameObject _progressImage;
    [SerializeField] private int _progressImagesMaxCount;

    private float _nextProgressTarget = 0f;

    private void OnEnable()
    {
        _spawner.EnemySpawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawned -= OnEnemySpawned;
    }

    private void Start()
    {
        _nextProgressTarget = (float)_spawner.Dencity / (float)_progressImagesMaxCount;
    }

    private void OnEnemySpawned(int spawned, int dencity)
    {
        if (spawned >= _nextProgressTarget)
        {
            Instantiate(_progressImage, _progressPanel);
            
            _nextProgressTarget += (float)dencity / (float)_progressImagesMaxCount;
        }
    }

    public void OnFinishButtonPressed()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnTryAgainButtonPressed()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

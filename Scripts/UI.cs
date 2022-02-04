using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _moneyLabel;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _progressPanel;
    [SerializeField] private GameObject _progressImage;
    [SerializeField] private int _progressImagesMaxCount;

    private Wallet _wallet;
    private Animator _moneyAnimator;
    private float _nextProgressTarget = 0f;

    private void OnEnable()
    {
        _wallet.MoneyChanged += OnMoneyChanged;
        _spawner.EnemySpawned += OnEnemySpawned;

        _moneyAnimator = _moneyLabel.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged += OnMoneyChanged;
        _spawner.EnemySpawned -= OnEnemySpawned;
    }

    private void Awake()
    {
        _wallet = GameObject.FindObjectOfType<Wallet>();
    }

    private void Start()
    {
        _nextProgressTarget = (float)_spawner.Dencity / (float)_progressImagesMaxCount;
    }

    private void OnMoneyChanged(int value)
    {
        if (value.ToString() == _moneyLabel.text)
        {
            _moneyAnimator.SetTrigger("NotEnought");
        }

        _moneyLabel.text = value.ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Music _musicPrefab;
    [SerializeField] private MusicButton _musicButtonPrefab;
    [SerializeField] private Level _levelPrefab;
    [SerializeField] private Transform _levelsContainer;
    [SerializeField] private GameObject _levelsPanel;
    [SerializeField] private GameObject _startButton;

    private void Awake()
    {
        Music music = GameObject.FindObjectOfType<Music>();
        MusicButton musicButton = GameObject.FindObjectOfType<MusicButton>();

        if (music == null)
        {
            Instantiate(_musicPrefab);
        }

        if (musicButton == null)
        {
            Instantiate(_musicButtonPrefab);
        }
    }

    private void Start()
    {
        int scenesCount = PlayerPrefs.GetInt("Level", 1);

        for (int i = 0; i < scenesCount; i++)
        {
            Level level = Instantiate(_levelPrefab, _levelsContainer);
            level.Init(i + 1);
        }
    }

    public void OnStartButtonPressed()
    {
        _levelsPanel.SetActive(true);
        _startButton.SetActive(false);
    }

    public void OnCloseButtonPressed()
    {
        _levelsPanel.SetActive(false);
        _startButton.SetActive(true);
    }
}

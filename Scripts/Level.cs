using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Text _levelLabel;

    private int _index;

    public void Init(int level)
    {
        _index = level;
        _levelLabel.text = level.ToString();
    }

    public void OnButtonPressed()
    {
        SceneManager.LoadScene(_index);
    }
}

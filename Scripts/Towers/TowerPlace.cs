using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlace : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _container;

    public bool IsBusy { get; private set; }
    public Transform Container => _container;

    public void SetBusyStatus(bool value)
    {
        IsBusy = value;
    }

    public void ChangeSliderValue(float value)
    {
        _slider.value = value;
    }
}

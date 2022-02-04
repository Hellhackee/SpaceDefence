using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlace : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public bool IsBusy { get; private set; }

    public void SetBusyStatus(bool value)
    {
        IsBusy = value;
    }

    public void ChangeSliderValue(float value)
    {
        _slider.value = value;
    }
}

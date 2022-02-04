using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money => _money;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
    }

    public void ChangeMoney(int value)
    {
        int money = _money + value;

        if (money >= 0)
        {
            _money = money;
            MoneyChanged?.Invoke(_money);
        }    
    }
}

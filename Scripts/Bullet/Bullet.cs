using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool DestroyOnEnemyChange;
    
    protected int Damage;

    public void SetDamage(int value)
    {
        Damage = value;
    }
}

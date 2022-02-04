using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Towers", menuName = "CreateTower", order = 51)]
public class TowerPanelSO : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _range;
    [SerializeField] private int _ammo;
    [SerializeField] private Tower _tower;
    [SerializeField] private Bullet _bullet;

    public Sprite Icon => _icon;
    public int Price => _price;
    public int Damage => _damage;
    public float Cooldown => _cooldown;
    public float Range => _range;
    public int Ammo => _ammo;
    public Bullet BulletPrefab => _bullet;

    public Tower TowerPrefab => _tower;
}

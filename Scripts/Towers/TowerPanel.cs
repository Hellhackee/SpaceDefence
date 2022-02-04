using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    [SerializeField] private Text _price;
    [SerializeField] private Text _ammo;
    [SerializeField] private Text _range;
    [SerializeField] private Text _attackSpeed;
    [SerializeField] private Text _damage;
    [SerializeField] private Image _icon;
    [SerializeField] private float _offsetY;

    private TowerCreater _towerCreater;
    private TowerPanelSO _scriptableObject;
    private Wallet _wallet;

    private void Start()
    {
        _towerCreater = GameObject.FindObjectOfType<TowerCreater>();
        _wallet = GameObject.FindObjectOfType<Wallet>();
    }

    public void Init()
    {
        _price.text = _scriptableObject.Price.ToString();
        _ammo.text = _scriptableObject.Ammo.ToString();
        _range.text = _scriptableObject.Range.ToString();
        _damage.text = _scriptableObject.Damage.ToString();
        _icon.sprite = _scriptableObject.Icon;

        float attackDamage = 60 / _scriptableObject.Cooldown;
        int attackDamageInt = (int)attackDamage;

        _attackSpeed.text = attackDamageInt.ToString();
    }

    public void LoadTurret(TowerPanelSO so)
    {
        _scriptableObject = so;
    }

    public void OnButtonDown()
    {
        if (_towerCreater != null)
        {
            int price = _scriptableObject.Price;
            int money = _wallet.Money;

            if (price <= money)
            {
                _wallet.ChangeMoney(-price);

                Tower tower = Instantiate(_scriptableObject.TowerPrefab, _towerCreater.transform);
                tower.Init(_scriptableObject);
                Transform place = _towerCreater.ChosenPlace.transform;
                tower.transform.position = new Vector3(place.position.x, place.position.y + _offsetY, place.position.z);
                tower.transform.rotation = Quaternion.Euler(place.eulerAngles.x, place.eulerAngles.y, place.eulerAngles.z);
                tower.SetTowerPlace(_towerCreater.ChosenPlace);

                _towerCreater.ChosenPlace.SetBusyStatus(true);
                _towerCreater.OnCloseButtonClicked();

                tower = null;
            }
            else
            {
                _wallet.ChangeMoney(0);
            }
        }        
    }
}

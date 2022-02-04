using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreater : MonoBehaviour
{
    [SerializeField] private TowerPanelSO[] _towers;
    [SerializeField] private TowerPanel _towerPanelPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _towerPanel;

    private Camera _mainCamera;
    private TowerPlace _chosenPlace = null;

    public TowerPlace ChosenPlace => _chosenPlace;

    private void Start()
    {
        _mainCamera = Camera.main;

        foreach (var tower in _towers)
        {
            TowerPanel towerPanel = Instantiate(_towerPanelPrefab, _container);
            towerPanel.LoadTurret(tower);
            towerPanel.Init();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, 100f, _layerMask);

            if (hits.Length > 0)
            {
                if (hits[0].collider.TryGetComponent(out TowerPlace towerPlace))
                {
                    if (towerPlace.IsBusy == false)
                    {
                        _chosenPlace = towerPlace;
                        _towerPanel.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnCloseButtonClicked()
    {
        _chosenPlace = null;
        _towerPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class GeneratedElement : MonoBehaviour
{
    [SerializeField] private float _positionY;
    [SerializeField] private GameObject mainObject;
    [SerializeField] private TowerPanelSO _towerSO;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _offsetY;
    [SerializeField] private ParticleSystem _dieParticle;
    [SerializeField] private ParticleLines _particlePrefab;

    private ParticleLines _particleLines;
    private ParticleSystem _particleSystem;
    private bool _isChosen = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        if (_isChosen == true)
        {
            Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 80f);
            Vector3 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(mouse);
            Vector3 target = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);

            transform.position = target;

            if (_particleSystem != null)
            {
                _particleSystem.transform.position = transform.position;
            }
        }    
    }

    private void OnMouseDown()
    {
        rb.isKinematic = true;

        Vector3 targetPosition = new Vector3(transform.localPosition.x, _positionY, transform.localPosition.z);
        Sequence transformSequence = DOTween.Sequence();
        transformSequence.Append(transform.DOLocalMove(targetPosition, 0.5f)).Append(transform.DORotate(Vector3.zero, 0.5f));
        transformSequence.Play();

        SetChosenStatus(true);

        _particleLines = Instantiate(_particlePrefab);
        _particleSystem = _particleLines.GetComponent<ParticleSystem>();

        if (_particleSystem != null)
        {
            _particleSystem.transform.position = transform.position;
            _particleSystem.Play();
        }
    }

    private void OnMouseUp()
    {
        if (_isChosen == true)
        {
            rb.isKinematic = false;

            SetChosenStatus(false);

            if (_particleSystem != null)
            {
                Destroy(_particleSystem);
                Destroy(_particleLines);
            }

            RaycastHit[] rays = Physics.BoxCastAll(transform.position, new Vector3(3f, 15f, 3f), Vector3.down, Quaternion.Euler(0,0,0), 30f, _layerMask);
            
            foreach (RaycastHit ray in rays)
            {
                if (ray.collider.TryGetComponent<TowerPlace>(out TowerPlace towerPlace))
                {
                    if (towerPlace.IsBusy == false)
                    {
                        if (_towerSO != null)
                        {
                            Tower tower = Instantiate(_towerSO.TowerPrefab, towerPlace.Container);
                            tower.Init(_towerSO);
                            Transform place = towerPlace.transform;
                            tower.transform.position = new Vector3(place.position.x, place.position.y + _offsetY, place.position.z);
                            tower.transform.rotation = Quaternion.Euler(place.eulerAngles.x, place.eulerAngles.y, place.eulerAngles.z);
                            tower.SetTowerPlace(towerPlace);

                            towerPlace.SetBusyStatus(true);

                            Destroy(gameObject);
                        }
                        else
                        {
                            DestroyThis();
                        }
                    }
                    else
                    {
                        DestroyThis();
                    }
                }
                else
                {
                    DestroyThis();
                }
            }

            if (rays.Length == 0)
            {
                DestroyThis();
            }

        }
    }

    private void SetChosenStatus(bool value)
    {
        _isChosen = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(6f, 30f, 6f));
    }

    private void DestroyThis()
    {
        Destroy(gameObject, 2f);

        mainObject.SetActive(false);
        _dieParticle.Play();
    }
}

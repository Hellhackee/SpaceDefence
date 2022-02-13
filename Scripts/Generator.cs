using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _leftSide;
    [SerializeField] private GameObject _rightSide;

    [Header("Spawner")]
    [SerializeField] private int _objectsCount;
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private BoxCollider _spawnPlace;
    [SerializeField] private Transform _container;

    private List<GameObject> _leftSideElements = new List<GameObject>();
    private List<GameObject> _rightSideElements = new List<GameObject>();
    private bool _activateStatus = false;

    private void Start()
    {
        foreach (Transform element in _leftSide.GetComponentInChildren<Transform>())
        {
            _leftSideElements.Add(element.gameObject);
        }

        foreach (Transform element in _rightSide.GetComponentInChildren<Transform>())
        {
            _rightSideElements.Add(element.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SetActivateElement(_activateStatus, _leftSideElements));
            StartCoroutine(SetActivateElement(_activateStatus, _rightSideElements));
            
            _activateStatus = !_activateStatus;

            Generate();
        }
    }

    private IEnumerator SetActivateElement(bool value, List<GameObject> elements)
    {
        if (value == false)
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                GameObject element = elements[i];

                yield return new WaitForSeconds(0.03f);

                element.SetActive(value);
            }
        }
        else
        {
            for (int i = 0; i < elements.Count; i++)
            {
                GameObject element = elements[i];

                yield return new WaitForSeconds(0.03f);

                element.SetActive(value);
            }
        }
    }

    public void Generate()
    {
        for (int i = 0; i < _objectsCount; i++)
        {
            Vector3 extents = _spawnPlace.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
                ) + _spawnPlace.center;

            Vector3 spawnPoint = _spawnPlace.transform.TransformPoint(point);

            GameObject prefab = _templates[Random.Range(0, _templates.Length)];
            Instantiate(prefab, spawnPoint, Random.rotation, _container);
        }
    }
}

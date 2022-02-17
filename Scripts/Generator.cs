using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private int _objectsCount;
    [SerializeField] private GameObject[] _templates;
    [SerializeField] private BoxCollider _spawnPlace;
    [SerializeField] private Transform _container;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
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

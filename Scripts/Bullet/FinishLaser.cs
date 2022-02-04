using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLaser : MonoBehaviour
{
    private Transform _targetPosition;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (_targetPosition != null)
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, _targetPosition.position, _speed * Time.deltaTime);
            transform.position = targetPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
        }
    }

    public void SetTargetPosition(Transform targetPosition)
    {
        _targetPosition = targetPosition;
    }
}

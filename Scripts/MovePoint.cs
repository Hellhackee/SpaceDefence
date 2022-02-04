using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private Spawner _spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Transform movePoint = _spawner.GetNextPoint(_index + 1);
            enemy.SetMovePoint(movePoint);
        }
    }
}

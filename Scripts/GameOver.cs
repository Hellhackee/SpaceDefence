using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] _rigidbodyObjects;
    [SerializeField] private GameObject _label;
    [SerializeField] private float _minVelocity;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private GameObject _tryAgainButton;

    public event UnityAction GameEnded;

    public void EndGame()
    {
        _label.gameObject.SetActive(true);

        foreach (GameObject item in _rigidbodyObjects)
        {
            Rigidbody rb = item.AddComponent<Rigidbody>();
           // rb.mass = Random.Range(_minMass, _maxMass);
            rb.isKinematic = false;
            rb.AddExplosionForce(Random.Range(_minVelocity, _maxVelocity), transform.position, 10f, Random.Range(_minVelocity, _maxVelocity), ForceMode.Impulse);
        }

        GameEnded?.Invoke();

        StartCoroutine(SetTryAgainActive());
    }

    private IEnumerator SetTryAgainActive()
    {
        yield return new WaitForSeconds(1f);

        _tryAgainButton.SetActive(true);
    }
}

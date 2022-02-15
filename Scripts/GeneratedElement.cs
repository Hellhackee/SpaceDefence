using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class GeneratedElement : MonoBehaviour
{
    [SerializeField] private float _positionY;
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
    }

    private void OnMouseUp()
    {
        if (_isChosen == true)
        {
            rb.isKinematic = false;

            SetChosenStatus(false);
        }
    }

    private void SetChosenStatus(bool value)
    {
        _isChosen = value;
    }
}

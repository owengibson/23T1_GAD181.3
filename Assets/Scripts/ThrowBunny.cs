using MeadowMateys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBunny : MonoBehaviour
{
    [SerializeField] private KeyCode _interactKey;
    private bool _isHolding = false;
    private bool canPickUp = false;
    [SerializeField] private Transform _pickUpPoint;
    [SerializeField] private GameObject pickUpTrigger;
    [SerializeField] private GameObject _grabObject;
    public SimplePlayerMovement simplePlayerMovement;



    private void Update()
    {
        if (canPickUp == true && Input.GetKeyDown(_interactKey))
        {
            PickUpObject();
        }

        if (_isHolding == true && Input.GetKeyDown(_interactKey) && canPickUp == false)
        {
            ThrowGrabObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player 2 Entered Trigger");

        if (collision.CompareTag("Player"))
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }

    private void PickUpObject()
    {
        _isHolding = true;
        _grabObject.GetComponent<Rigidbody2D>().isKinematic = true;
        _grabObject.transform.position = _pickUpPoint.position;
        _grabObject.transform.SetParent(transform);
        simplePlayerMovement.enabled = false;
        // set animation trigger to be on
    }

    private void ThrowGrabObject()
    {
        _grabObject.GetComponent<Rigidbody2D>().isKinematic = false;
        _grabObject.transform.SetParent(null);
        _isHolding = false;
        simplePlayerMovement.enabled = true;
        // set animation trigger to be off

        Rigidbody2D rb = _grabObject.GetComponent<Rigidbody2D>();
        float throwDistance = 10f;
        float throwHeight = 15f;
        float throwAngle = Mathf.Atan((4f * throwHeight) / throwDistance) * Mathf.Rad2Deg;
        float throwRadians = throwAngle * Mathf.Deg2Rad;
        Vector2 throwDirection = new Vector2(1f, 1f).normalized;
        Vector2 throwVelocity = throwDirection * throwDistance * Mathf.Cos(throwRadians);
        throwVelocity.y = Mathf.Sin(throwRadians) * throwHeight;

        rb.velocity = throwVelocity;

    }
}

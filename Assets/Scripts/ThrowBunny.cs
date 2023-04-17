using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBunny : MonoBehaviour
{
    [SerializeField] private KeyCode pickUpKey;
    [SerializeField] private KeyCode throwKey;
    [SerializeField] private Transform pickUpPoint;
    [SerializeField] private Transform raycastPoint;

    [SerializeField] private float rayDistance;

    private GameObject grabObject;

    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Player");
    }


    private void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(raycastPoint.position, transform.right, rayDistance);

        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.layer == layerIndex)
        {
            if (Input.GetKeyDown(pickUpKey) && grabObject == null)
            {
                grabObject = raycastHit2D.collider.gameObject;
                grabObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabObject.transform.position = pickUpPoint.position;
                grabObject.transform.SetParent(transform);
            }

        }

        if (Input.GetKeyDown(throwKey))
        {
            ThrowGrabObject();
        }

    }

    private void ThrowGrabObject()
    {
        if (grabObject == null) return;

        grabObject.GetComponent<Rigidbody2D>().isKinematic = false;
        grabObject.transform.SetParent(null);

        Rigidbody2D rb = grabObject.GetComponent<Rigidbody2D>();
        float throwDistance = 10f;
        float throwHeight = 15f;
        float throwAngle = Mathf.Atan((4f * throwHeight) / throwDistance) * Mathf.Rad2Deg;
        float throwRadians = throwAngle * Mathf.Deg2Rad;
        Vector2 throwDirection = new Vector2(1f, 1f).normalized;
        Vector2 throwVelocity = throwDirection * throwDistance * Mathf.Cos(throwRadians);
        throwVelocity.y = Mathf.Sin(throwRadians) * throwHeight;

        rb.velocity = throwVelocity;
        grabObject = null;
    }
}

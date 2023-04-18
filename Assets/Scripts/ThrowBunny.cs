using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBunny : MonoBehaviour
{
    [SerializeField] private KeyCode pickUpKeyP1;
    [SerializeField] private KeyCode throwKeyP1;
    [SerializeField] private KeyCode pickUpKeyP2;
    [SerializeField] private KeyCode throwKeyP2;
    [SerializeField] private Transform pickUpPoint;
    public Vector2 offset;
    [SerializeField] private float rayDistance;

    private GameObject grabObject;

    private int layerIndexP2;
    private int layerIndexP1;

    private void Start()
    {
        layerIndexP1 = LayerMask.NameToLayer("Player");
        layerIndexP2 = LayerMask.NameToLayer("Player");

    }


    private void Update()
    {

        Vector2 rayDirection = transform.right;
        if (transform.localScale.x < 0)
        {
            // if the player is facing left, flip the direction of the RayCast
            rayDirection *= -1;
        }

        Vector2 rayStartPoint = transform.position + transform.forward + transform.right * offset.x + transform.up * offset.y;


        RaycastHit2D raycastHit2D = Physics2D.Raycast(rayStartPoint, transform.right, rayDistance);

        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.layer == layerIndexP2)
        {
            if (Input.GetKeyDown(pickUpKeyP1) && grabObject == null)
            {
                grabObject = raycastHit2D.collider.gameObject;
                grabObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabObject.transform.position = pickUpPoint.position;
                grabObject.transform.SetParent(transform);
            }

        }

        if (Input.GetKeyDown(throwKeyP1))
        {
            ThrowGrabObject();
        }

 
        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.layer == layerIndexP1)
        {
            if (Input.GetKeyDown(pickUpKeyP2) && grabObject == null)
            {
                grabObject = raycastHit2D.collider.gameObject;
                grabObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabObject.transform.position = pickUpPoint.position;
                grabObject.transform.SetParent(transform);
            }

        }

        if (Input.GetKeyDown(throwKeyP2))
        {
            ThrowGrabObject();
        }

        Debug.DrawRay(transform.position, raycastHit2D.point, Color.red);

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

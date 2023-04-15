using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject prefabRopeSeg;
    [SerializeField] private GameObject prefabLastSeg;
    [SerializeField] private int numLinks = 3;

    private Rigidbody2D _prevBod;

    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    private void Start()
    {
        GenerateRope();
        //transform.localScale = new Vector3(0.37f, 0.37f, 0.37f);
    }

    private void GenerateRope()
    {
        _prevBod = hook;
        for (int i = 0; i < numLinks; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSeg);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = _prevBod;

            _prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
        GameObject lastSeg = Instantiate(prefabLastSeg);
        lastSeg.transform.parent = transform;
        lastSeg.transform.position = transform.position;
        HingeJoint2D lastHj = lastSeg.GetComponent<HingeJoint2D>();
        //lastHj.connectedBody = prevBod;
        _prevBod = lastHj.connectedBody;
    }
    private void FixedUpdate()
    {
        //hook.transform.position = P1.transform.position;
        //_prevBod.transform.position = P2.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hook.transform.position, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_prevBod.transform.position, 0.8f);
    }
}

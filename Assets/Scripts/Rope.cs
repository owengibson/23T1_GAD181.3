using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject prefabRopeSeg;
    [SerializeField] private GameObject prefabLastSeg;
    [SerializeField] private int numLinks = 3;
    public float distanceBetweenEnds;

    private Rigidbody2D _prevBod;
    private List<GameObject> _ropeSegments = new List<GameObject>();
    private GameObject _lastSeg;
    private Vector2 _ropeEndPos;
    private HingeJoint2D _hookHingeJoint2D;
    private HingeJoint2D _lastSegHingeJoint2D;

    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    private void Start()
    {
        _ropeSegments.Add(hook.gameObject);
        GenerateRope();
        //transform.localScale = new Vector3(0.37f, 0.37f, 0.37f);
    }

    private void GenerateRope()
    {
        _prevBod = hook;
        _hookHingeJoint2D = hook.GetComponent<HingeJoint2D>();

        for (int i = 0; i < numLinks; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSeg);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = _prevBod;
            _ropeSegments.Add(newSeg);

            _prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
        _ropeEndPos = new Vector2(_prevBod.GetComponent<SpriteRenderer>().bounds.center.x, _prevBod.GetComponent<SpriteRenderer>().bounds.min.y);

        //_lastSeg = Instantiate(prefabLastSeg);
        //_lastSeg.transform.parent = transform;
        //_lastSeg.transform.position = transform.position;
        //HingeJoint2D lastHj = _lastSeg.GetComponent<HingeJoint2D>();
        //lastHj.connectedBody = _prevBod;
        ////_prevBod = lastHj.connectedBody;
        ////lastHj.connectedAnchor = _ropeEndPos;
        //_ropeSegments.Add(_lastSeg);
        //_lastSegHingeJoint2D = _lastSeg.GetComponent<HingeJoint2D>();

        _lastSeg = _prevBod.gameObject;
        _lastSegHingeJoint2D = _lastSeg.GetComponent<HingeJoint2D>();
    }
    private void FixedUpdate()
    {
        distanceBetweenEnds = Vector2.Distance(hook.transform.position, _lastSeg.transform.position);

        //_hookHingeJoint2D.connectedAnchor = P1.transform.position;
        //_lastSegHingeJoint2D.connectedAnchor = P2.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hook.transform.position, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_prevBod.transform.position, 0.8f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_ropeEndPos, 0.5f);
    }
}

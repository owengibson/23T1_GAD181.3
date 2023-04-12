using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeadowMateys
{
    public class RopeSegment : MonoBehaviour
    {
        public GameObject connectedAbove, connectedBelow;

        private void Start()
        {
            connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
            RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
            if (aboveSegment != null)
            {
                aboveSegment.connectedAbove = gameObject;
                float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
                GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
            }
            else
            {
                GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeadowMateys
{
    public class Parallax : MonoBehaviour
    {
        private float _length, _startPos;
        [SerializeField] private Transform cam;
        [SerializeField] private float parallaxEffect;

        private void Start()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float temp = cam.position.x * (1 - parallaxEffect);
            float dist = cam.position.x * parallaxEffect;

            transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

            if (temp > _startPos + _length) _startPos += _length;
            else if (temp < _startPos - _length) _startPos -= _length;
        }
    }
}
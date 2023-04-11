using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD181_3
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private GameObject pickupPlayer;
        private bool isCollected = false;
        private SpriteRenderer sr;

        private void Start()
        {
            sr = pickupPlayer.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == pickupPlayer)
            {
                isCollected = true;
                transform.localScale *= 0.75f;
                GetComponent<BoxCollider2D>().enabled = false;
                EventManager.OnPickup?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (isCollected)
            {
                transform.position = new Vector2(pickupPlayer.transform.position.x, pickupPlayer.transform.position.y + 2.25f);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GAD181_3
{
    public class EndTrigger : MonoBehaviour
    {
        private List<GameObject> objectsInTrigger = new List<GameObject>();
        private int pickupsCollected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                objectsInTrigger.Add(collision.gameObject);
                Debug.Log(collision.gameObject.name + " is ready to end");

                if (objectsInTrigger.Count >= 2 && pickupsCollected >= 2)
                {
                    SceneManager.LoadScene("End");
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                objectsInTrigger.Remove(collision.gameObject);
            }
        }
        private void IncrementPickupsCollected()
        {
            pickupsCollected++;
            Debug.Log("Pickups Collected: " + pickupsCollected);
        }
        private void OnEnable()
        {
            EventManager.OnPickup += IncrementPickupsCollected;
        }
        private void OnDisable()
        {
            EventManager.OnPickup -= IncrementPickupsCollected;
        }
    }
}
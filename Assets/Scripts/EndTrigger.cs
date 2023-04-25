using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeadowMateys
{
    public class EndTrigger : MonoBehaviour
    {
        private List<GameObject> _objectsInTrigger = new List<GameObject>();
        private int _pickupsCollected;

        [SerializeField] private string sceneToLoad;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _objectsInTrigger.Add(collision.gameObject);

                if (_objectsInTrigger.Count >= 2 && _pickupsCollected >= 2)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _objectsInTrigger.Remove(collision.gameObject);
            }
        }
        private void IncrementPickupsCollected()
        {
            _pickupsCollected++;
            Debug.Log("Pickups Collected: " + _pickupsCollected);
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
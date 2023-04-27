using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeadowMateys
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private AudioManager audioManager;

        public void PlayGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        private void Start()
        {
            audioManager.Play("TitleMusic");
            audioManager.Play("Ambience");
        }
    }
}
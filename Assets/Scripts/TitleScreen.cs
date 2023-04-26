using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeadowMateys
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        public void PlayGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeadowMateys
{
    public class TitleScreen : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Tutorial Level");
        }
    }
}
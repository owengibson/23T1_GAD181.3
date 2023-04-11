using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GAD181_3
{
    public class TitleScreen : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Level01");
        }
    }
}
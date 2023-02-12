using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SAMI.TIKTAKTEO
{
    public class SeneManeger : MonoBehaviour
    {

        public void LoadScene(int SceneIndex)
        {
            SceneManager.LoadScene(SceneIndex);
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace SAMI.TIKTAKTEO
{
    public class AudioManeger : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer mixer;
        // Start is called before the first frame update
        public void ChangeVolume(float value)
        {

            mixer.SetFloat("MusicVolume", value);


        }
    }
}

using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Modding.Logger;

namespace HK_AudioMod
{
    public class AudioRegion
    {

        public string name = null;
        public bool isEnabled = false;

        public GameObject gameObject = null;
        public AudioSource audioSource = null;

        public AudioRegion(string name)
        {
            this.isEnabled = false;
            this.name = name;
            
            this.gameObject = new GameObject();
            this.audioSource = this.gameObject.AddComponent<AudioSource>();

            AudioClip ac = null;

            HK_AudioMod.nameClipMap.TryGetValue(name, out ac);

            this.audioSource.clip = ac;
        }

        public AudioRegion(string name, bool isEnabled) { 
            this.name = name;
            this.isEnabled = isEnabled;
            this.gameObject = new GameObject();
            this.audioSource = this.gameObject.AddComponent<AudioSource>();

            AudioClip ac = null;

            HK_AudioMod.nameClipMap.TryGetValue(name, out ac);

            this.audioSource.clip = ac;
            this.isEnabled = isEnabled;
        }

        

        public void Play()
        {
            if (!audioSource.isPlaying && isEnabled)
            {
                audioSource.Play();
            }
        }


        


    }
}

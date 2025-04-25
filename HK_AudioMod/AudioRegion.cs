using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Modding.Logger;

namespace HK_AudioMod
{
    public class AudioRegion
    {
        public static AudioRegion Instance { get; private set; }

        public string name = null;
        public bool isEnabled = false;

        public GameObject gameObject = null;
        public AudioSource audioSource = null;

        public bool isPlaying = false;

        private float playTime = 0.0f;

        [Obsolete]
        public AudioRegion(string name) { 
            this.name = name;
            this.isEnabled = false;
            this.gameObject = new GameObject();
            this.audioSource = this.gameObject.AddComponent<AudioSource>();
            this.audioSource.spatialBlend = 0;

            try
            {
                
            }
            catch(Exception ex) {
                Logger.Log(ex);
            }
            
            
            Instance = this;
        }

        public void Play()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            isPlaying = true;
            playTime = 0.0f;
        }

        private class PlayTimer : MonoBehaviour{

            

            void Update()
            {
                if (Instance.isPlaying)
                {
                    Instance.playTime += Time.deltaTime;

                    if(Instance.playTime >= 5)
                    {
                        Instance.audioSource.Stop();
                    }
                }
            }
        }


    }
}

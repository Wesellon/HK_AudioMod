using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HK_AudioMod
{
    public class CondAudioRegion : AudioRegion
    {
        public bool condition;

        public CondAudioRegion(string name, string currentRoom, bool condition,params string[] roomList ) : base(name)
        {
            this.condition = roomList.Contains(currentRoom) && condition;

            gameObject.SetActive(this.condition);
        }

        public new void Play()
        {
            if (!audioSource.isPlaying && isEnabled && condition)
            {
                audioSource.Play();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HK_AudioMod;
using Logger = Modding.Logger;

namespace HK_AudioMod
{
    public class CondRoomAudioRegion : AudioRegion
    {
        private bool condition;
        // Constructor that takes a name, current room, condition, and a list of rooms
        // If the current room is in the list and the condition is true, the region is active
        // Otherwise, it is inactive

        public CondRoomAudioRegion(string name, string currentRoom, bool condition, params string[] roomList) : base(name, condition)
        {
            this.condition = roomList.Contains(currentRoom) && condition;

            gameObject.SetActive(this.condition);
        }

        public override void Play()
        {
            if (!audioSource.isPlaying && isEnabled && condition)
            {
                Logger.Log(name + " collision");
                audioSource.Play();
            }
        }
    }
}

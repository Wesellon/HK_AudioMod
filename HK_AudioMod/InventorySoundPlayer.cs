using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mono.Security.X509.X520;
using UnityEngine;
using System.Collections;
using Modding;
using HutongGames.PlayMaker.Actions;

namespace HK_AudioMod
{
    public class InventorySoundPlayer
    {
        public GameObject go;
        public string sound_clip;

        private bool isPlaying = false;

        public InventorySoundPlayer()
        {
            go = new GameObject();
            GameManager.DontDestroyOnLoad(go);
            go.AddComponent<AudioSource>();
            Modding.Logger.Log("add audiosource: " + go.GetComponent<AudioSource>());
            go.GetComponent<AudioSource>().loop = true;

            ModHooks.HeroUpdateHook += ModHooks_HeroUpdateHook;

            ModHooks.AfterTakeDamageHook += ModHooks_AfterTakeDamageHook;

            ModHooks.BeforeAddHealthHook += ModHooks_BeforeAddHealthHook;
        }

 

        private int ModHooks_AfterTakeDamageHook(int hazardType, int damageAmount)
        {
            Modding.Logger.Log("Take Damage: " + damageAmount);
            Modding.Logger.Log("Health:" + PlayerData.instance.health);
            if (PlayerData.instance.health - damageAmount == 1)
            {
                setSound("heart_beat_sound");
                playSound();
            }

            return damageAmount;
        }


        private int ModHooks_BeforeAddHealthHook(int arg)
        {

            if (PlayerData.instance.health + arg > 1)
            {
                Modding.Logger.Log("Add Health: " + arg);
                if (sound_clip != null && sound_clip.Equals("heart_beat_sound"))
                {
                    stopSound();
                }
            }

            return arg;
        }



        public void setSound(string sound_name)
        {
            Modding.Logger.Log("setSound: " + sound_name);

            AudioClip ac = null;

            HK_AudioMod.nameClipMap.TryGetValue(sound_name, out ac);
            Modding.Logger.Log("ac: " + ac);
            Modding.Logger.Log("as: " + go.GetComponent<AudioSource>());
            go.GetComponent<AudioSource>().clip = ac;

            sound_clip = sound_name;




        }

        private void ModHooks_HeroUpdateHook()
        {
            if (sound_clip != null)
            {
                if (HK_AudioMod.isInvOpen)
                {
                    go.GetComponent<AudioSource>().volume = 0.8f;
                }
                else
                {
                    go.GetComponent<AudioSource>().volume = 0;
                }
            }

            if (sound_clip == null && PlayerData.instance.healthBlue > 0 && PlayerData.instance.health == PlayerData.instance.maxHealth)
            {
                setSound("glow_sound");
                playSound();
            }

            if(sound_clip != null && sound_clip.Equals("glow_sound") && PlayerData.instance.healthBlue == 0)
            {
                stopSound();
            }

        }

        public void playSound()
        {
            isPlaying = true;
            go.GetComponent<AudioSource>().Play();


            Modding.Logger.Log("playSound: " + sound_clip);
        }

        public void stopSound()
        {

            sound_clip = null;
            Modding.Logger.Log("stopSound: " + sound_clip);
            isPlaying = false;
        }
    }


}

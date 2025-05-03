using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HK_AudioMod
{
    public static class AudioRegionsHandler
    {
        public static List<AudioRegion> audioRegions = new List<AudioRegion>();

        public static void addAudioRegion(AudioRegion audioRegion, Vector2 position, Vector2 size)
        {
            SpriteRenderer spriteRenderer = audioRegion.gameObject.AddComponent<SpriteRenderer>();
            
            spriteRenderer.sortingLayerName = "HUD";
            spriteRenderer.sortingOrder = 1000;
            spriteRenderer.transform.localScale = new Vector3(size.x,size.y,1);
            spriteRenderer.forceRenderingOff = true;
            
            audioRegion.gameObject.transform.localPosition = new Vector3(position.x,position.y,1);

            audioRegion.gameObject.AddComponent<AudioSource>();



            audioRegions.Add(audioRegion);
            
        }

        public static void addAudioRegion(AudioRegion audioRegion, Vector2 position, Vector2 size, float volume)
        {
            addAudioRegion(audioRegion, position, size);

            audioRegions[audioRegions.Count-1].gameObject.GetComponent<AudioSource>().volume = volume;
        }

        public static string listToString()
        {
            String str = string.Empty;

            foreach(AudioRegion ar in audioRegions)
            {
                str += ar.name + "\n";
            }

            return str;
        }

        public static void clearList()
        {
            foreach (AudioRegion ar in audioRegions)
            {
                UnityEngine.Object.Destroy(ar.gameObject);
                UnityEngine.Object.Destroy(ar.audioSource);
            }

            audioRegions.Clear();
        }

        public static void set_visibility(bool isVisible)
        {
            foreach(AudioRegion ar in audioRegions)
            {
                ar.gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = !isVisible;
            }
        }

        
    }
}

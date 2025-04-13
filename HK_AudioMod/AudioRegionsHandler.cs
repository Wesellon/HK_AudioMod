using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HK_AudioMod
{
    public static class AudioRegionsHandler
    {
        public static List<GameObject> gameObjects = new List<GameObject>();

        private static int index = 0;

        public static void addAudioRegion(GameObject audioRegion, Vector3 position, Vector3 size)
        {
            SpriteRenderer spriteRenderer = audioRegion.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "HUD";
            spriteRenderer.sortingOrder = 1000;
            spriteRenderer.transform.localScale = size;
            
            audioRegion.transform.localPosition = position;
            

            gameObjects.Add(audioRegion);
        }

        public static string listToString()
        {
            String str = string.Empty;

            foreach(GameObject go in gameObjects)
            {
                str += go.name + "\n";
            }

            return str;
        }

        public static void clearList()
        {
            foreach (GameObject go in gameObjects)
            {
                UnityEngine.Object.Destroy(go);
            }

            gameObjects.Clear();
        }

        
    }
}

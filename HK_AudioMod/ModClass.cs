using HutongGames.PlayMaker.Actions;
using HutongGames.Utility;
using IL;
using IL.TMPro;
using Modding;
using On;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace HK_AudioMod
{
    public class HK_AudioMod : Mod, ITogglableMod
    {
        internal static HK_AudioMod Instance;

        new public string GetName() => "HK_AudioMod";
        public override string GetVersion() => "0.0.0.1";

        //public override List<ValueTuple<string, string>> GetPreloadNames()
        //{
        //    return new List<ValueTuple<string, string>>
        //    {
        //        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
        //    };
        //}

        //public HK_AudioMod() : base("HK_AudioMod")
        //{
        //    Instance = this;
        //}

        public static GameObject placementCursor = null;
        public static GameObject customCursor = null;

        Vector2 posA, posB = new Vector2(-1000, -1000);

        public static bool firstUpdate = true;
        public static bool mapMarkerMenuOpen = false;

        
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initialize");

            Sprite s = LoadCustomSprite();



            On.MapMarkerMenu.Open += (orig, self) =>
            {
                orig(self);
                placementCursor = self.placementCursor;
                mapMarkerMenuOpen = true;



                Log("MapMarkerMenu Open");

                AudioRegionsHandler.addAudioRegion(new AudioRegion("Mantis Lords"), new Vector2(-0.4f, -14.1f), new Vector2(0.5f, 1));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("City Of Tears"), new Vector2(12.3f, -12.8f), new Vector2(0.5f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Watchers Knights"), new Vector2(14.4f, -8.9f), new Vector2(0.7f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("False Knight"), new Vector2(3.2f, -2.1f), new Vector2(0.5f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("The Collector"), new Vector2(22.4f, -11), new Vector2(0.5f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Broken Vessel"), new Vector2(2.5f, -19.9f), new Vector2(0.2f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Brooding Mawlek"), new Vector2(0, -2.4f), new Vector2(0.7f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Crystal Guardian"), new Vector2(12.6f, 2.1f), new Vector2(0.2f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Dung Defender"), new Vector2(13.7f, -14.4f), new Vector2(0.3f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Flukemarm"), new Vector2(7.1f, -16.3f), new Vector2(0.3f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Gruz Mother"), new Vector2(9.2f, -3.9f), new Vector2(0.15f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Hive Knight"), new Vector2(28.3f, -16.4f), new Vector2(0.15f, 0.15f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Hornet Protector"), new Vector2(-14.1f, -0.4f), new Vector2(0.4f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Massive Moss Charger"), new Vector2(-7.2f, -4.7f), new Vector2(0.5f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Nosk"), new Vector2(-6.2f, -18.6f), new Vector2(0.4f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Soul Master"), new Vector2(9.7f, -6.9f), new Vector2(0.3f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Traito Lord"), new Vector2(-15.2f, -5.9f), new Vector2(0.5f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Ummuu"), new Vector2(-3.9f, -6.8f), new Vector2(0.75f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Vengefly"), new Vector2(-11.8f, -0.0f), new Vector2(0.5f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Honet Sentinel"), new Vector2(31.6f, -13.1f), new Vector2(0.3f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Delicate Flower - Goal"), new Vector2(-13.9f, -8f), new Vector2(0.2f, 0.15f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Delicate Flower - Origin"), new Vector2(22.9f, -3.3f), new Vector2(0.15f, 0.15f));


                foreach (AudioRegion audioRegion in AudioRegionsHandler.audioRegions)
                {
                    if (audioRegion != null)
                    {
                        Log(audioRegion.name);
                        audioRegion.gameObject.transform.SetParent(GameManager.instance.gameMap.transform);

                        audioRegion.gameObject.transform.position -= getMapCursorPosition();
                        Log(audioRegion.gameObject.transform.localPosition);

                        audioRegion.gameObject.GetComponent<SpriteRenderer>().sprite = s;
                        Log("instantiated: " + audioRegion.name);


                        audioRegion.gameObject.layer = 5;

                    }
                }

            };


            On.MapMarkerMenu.Close += (orig, self) =>
            {
                Log("MapMarkerMenu Close");
                orig(self);
                UObject.Destroy(customCursor);
                placementCursor = null;
                mapMarkerMenuOpen = false;
                AudioRegionsHandler.clearList();
            };







            ModHooks.HeroUpdateHook += () =>
            {
                if (mapMarkerMenuOpen)
                {

                    if (customCursor == null)
                    {
                        customCursor = new GameObject();
                        customCursor.AddComponent<SpriteRenderer>();
                    }

                    var sr = customCursor.GetComponent<SpriteRenderer>();
                    customCursor.GetComponent<SpriteRenderer>().sprite = s;
                    customCursor.transform.localScale = new Vector3(0.1f, 0.1f, 0.5f);
                    customCursor.layer = 5;
                    sr.sortingLayerName = "HUD";
                    sr.sortingOrder = 1000;


                    customCursor.transform.position = placementCursor.transform.position;


                    foreach (AudioRegion audioRegion in AudioRegionsHandler.audioRegions)
                    {
                        audioRegion.gameObject.transform.SetPositionZ(customCursor.transform.position.z);
                        if (audioRegion.gameObject.GetComponent<SpriteRenderer>().bounds.Intersects(customCursor.GetComponent<SpriteRenderer>().bounds))
                        {
                            Log(audioRegion.name + " collision");
                            audioRegion.Play();
                            Log("RegionClip: " + audioRegion.audioSource.clip.length);
                        }
                    }




                    if (Input.GetKeyDown(KeyCode.L))
                    {
                        Log(getMapCursorPosition());
                    }

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        if (posA == new Vector2(-1000, -1000))
                        {
                            posA = GameManager.instance.gameMap.transform.position - customCursor.transform.position;
                        }
                        else if (posB == new Vector2(-1000, -1000))
                        {
                            posB = GameManager.instance.gameMap.transform.position - customCursor.transform.position;

                            Log("DIFFERENCE");
                            Log(posA - posB);
                        }
                        else
                        {
                            Log("Reset");
                            posA = new Vector2(-1000, -1000);
                            posB = new Vector2(-1000, -1000);
                        }
                    }
                }


                

            };
        }



        public Vector3 getMapCursorPosition()
        {

            return placementCursor.transform.position - GameManager.instance.gameMap.transform.position + new Vector3(4f, -8.6f, 0);


        }
        private Sprite LoadCustomSprite()
        {
            byte[] fileData = File.ReadAllBytes($"Hollow Knight_Data\\Managed\\Mods\\HK_AudioMod\\red_square.png");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); // Automatically resizes the texture
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }
    }
}

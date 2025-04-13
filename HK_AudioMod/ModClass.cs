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






            On.MapMarkerMenu.Open += (orig, self) =>
            {
                orig(self);
                placementCursor = self.placementCursor;
                mapMarkerMenuOpen = true;

                Log("MapMarkerMenu Open");

                AudioRegionsHandler.addAudioRegion(new GameObject("cokc"), new Vector3(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new GameObject("Mantis Lords"), new Vector3(-0.4f, -14.1f, 0), new Vector3(0.5f, 1, 1));


                foreach (GameObject audioRegion in AudioRegionsHandler.gameObjects)
                {
                    if (audioRegion != null)
                    {
                        Log(audioRegion.name);
                        audioRegion.transform.SetParent(GameManager.instance.gameMap.transform);

                        audioRegion.transform.position -= getMapCursorPosition();
                        Log(audioRegion.transform.localPosition);

                        audioRegion.GetComponent<SpriteRenderer>().sprite = LoadCustomSprite();
                        Log("instantiated: " + audioRegion.name);


                        audioRegion.layer = 5;

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
                    customCursor.GetComponent<SpriteRenderer>().sprite = LoadCustomSprite();
                    customCursor.transform.localScale = new Vector3(0.1f, 0.1f, 0.5f);
                    customCursor.layer = 5;
                    sr.sortingLayerName = "HUD";
                    sr.sortingOrder = 1000;


                    customCursor.transform.position = placementCursor.transform.position;

                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        foreach (GameObject gameObject in AudioRegionsHandler.gameObjects)
                        {
                            gameObject.transform.SetPositionZ(customCursor.transform.position.z);
                            if (gameObject.GetComponent<SpriteRenderer>().bounds.Intersects(customCursor.GetComponent<SpriteRenderer>().bounds))
                            {
                                Log(gameObject.name + " collision");
                            }
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

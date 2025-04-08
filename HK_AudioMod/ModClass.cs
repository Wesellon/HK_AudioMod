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

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initialize");



            On.MapMarkerMenu.Open += (orig, self) =>
            {
                orig(self);
                placementCursor = self.placementCursor;

            };

            On.MapMarkerMenu.Close += (orig, self) =>
            {
                orig(self);
                placementCursor = null;
            };

            On.MapMarkerMenu.PlaceMarker += (orig, self) =>
            {
                orig(self);
                
                Log(self.panSpeed);
            };

            


           

            ModHooks.HeroUpdateHook += () =>
            {
                if (placementCursor != null)
                {
                   
                    if(customCursor == null)
                    {
                        customCursor = new GameObject();
                        customCursor.AddComponent<SpriteRenderer>();
                    }

                    var sr = customCursor.GetComponent<SpriteRenderer>();
                    customCursor.GetComponent<SpriteRenderer>().sprite = LoadCustomSprite();
                    customCursor.transform.localScale = new Vector3(1, 1, 1);
                    customCursor.layer = 5;
                    sr.sortingLayerName = "HUD";
                    sr.sortingOrder = 1000;
                    customCursor.transform.SetParent(placementCursor.transform.parent);
                    customCursor.transform.position = placementCursor.transform.position;



                    //Log(customCursor.transform.parent);
                    //Log(map.transform.position);

                    //Map ScrollPosition
                    //Log(GameManager.instance.gameMap.transform.position);
                    



                }

                var gameObject = new GameObject();
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = LoadCustomSprite();
                gameObject.transform.parent = GameObject.Find("Knight").transform;

                gameObject.transform.localPosition = new Vector2(0, 0);
                
            };
        }

        

        private Sprite LoadCustomSprite()
        {
            byte[] fileData = File.ReadAllBytes($"D:\\Games\\Hollow-Knight-Steamrip.com\\Hollow Knight v1.5.78.11833\\Hollow Knight_Data\\Managed\\Mods\\HK_AudioMod\\FrameIcon.png");
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
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
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;
using UnityEngine.UI;
using static Mono.Security.X509.X520;
using UObject = UnityEngine.Object;

namespace HK_AudioMod
{
    public class HK_AudioMod : Mod, ITogglableMod
    {
        internal static HK_AudioMod Instance;

        new public string GetName() => "HK_AudioMod";
        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

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

        public static Dictionary<string, AudioClip> nameClipMap = new Dictionary<string, AudioClip>();

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initialize");

            Sprite s = LoadCustomSprite("red_square.png");
            Sprite green_square = LoadCustomSprite("green_square.png");

            DirectoryInfo d = new DirectoryInfo(Application.dataPath + "\\Managed\\Mods\\HK_AudioMod\\Audio");

            Log("Creating Map");

            FileInfo[] Files = d.GetFiles("*.mp3");
            foreach (FileInfo file in Files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FullName);
                AudioClip ac = null;
                GameManager.instance.StartCoroutine(GetAndSetAudioClip(fileName, ac));
                Log(fileName);
                

                nameClipMap.TryGetValue(fileName, out ac);
                
            }
            Log("Created Map");
            EscortHandler flowerEscortHandler = new EscortHandler();

            On.MapMarkerMenu.Open += (orig, self) =>
            {
                orig(self);
                placementCursor = self.placementCursor;
                mapMarkerMenuOpen = true;

                PlayerData pd = PlayerData.instance;

                AudioRegionsHandler.addAudioRegion(new AudioRegion("Mantis Lords",pd.statueStateMantisLords.hasBeenSeen), new Vector2(-0.4f, -14.1f), new Vector2(0.5f, 1));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("City Of Tears", pd.scenesVisited.Contains("city")), new Vector2(12.3f, -12.8f), new Vector2(0.5f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Watchers Knights",pd.statueStateWatcherKnights.hasBeenSeen), new Vector2(14.4f, -8.9f), new Vector2(0.7f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("False Knight", pd.statueStateFalseKnight.hasBeenSeen), new Vector2(3.2f, -2.1f), new Vector2(0.5f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("The Collector", pd.statueStateCollector.hasBeenSeen), new Vector2(22.4f, -11), new Vector2(0.5f, 0.5f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Broken Vessel", pd.statueStateBrokenVessel.hasBeenSeen), new Vector2(2.5f, -19.9f), new Vector2(0.2f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Brooding Mawlek", pd.statueStateBroodingMawlek.hasBeenSeen), new Vector2(0, -2.4f), new Vector2(0.7f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Crystal Guardian", pd.statueStateCrystalGuardian1.hasBeenSeen), new Vector2(12.6f, 2.1f), new Vector2(0.2f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Dung Defender", pd.statueStateDungDefender.hasBeenSeen), new Vector2(13.7f, -14.4f), new Vector2(0.3f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Flukemarm", pd.statueStateFlukemarm.hasBeenSeen), new Vector2(7.1f, -16.3f), new Vector2(0.3f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Gruz Mother", pd.statueStateGruzMother.hasBeenSeen), new Vector2(9.2f, -3.9f), new Vector2(0.15f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Hive Knight", pd.statueStateHiveKnight.hasBeenSeen), new Vector2(28.3f, -16.4f), new Vector2(0.15f, 0.15f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Hornet Protector", pd.statueStateHornet1.hasBeenSeen), new Vector2(-14.1f, -0.4f), new Vector2(0.4f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Massive Moss Charger", pd.statueStateMegaMossCharger.hasBeenSeen), new Vector2(-7.2f, -4.7f), new Vector2(0.5f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Nosk", pd.statueStateNosk.hasBeenSeen), new Vector2(-6.2f, -18.6f), new Vector2(0.4f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Soul Master", pd.statueStateSoulMaster.hasBeenSeen), new Vector2(9.7f, -6.9f), new Vector2(0.3f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Traitor Lord", pd.killedTraitorLord), new Vector2(-15.2f, -5.9f), new Vector2(0.5f, 0.1f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Ummuu", pd.statueStateUumuu.hasBeenSeen), new Vector2(-3.9f, -6.8f), new Vector2(0.75f, 0.3f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Vengefly", pd.statueStateVengefly.hasBeenSeen), new Vector2(-11.8f, -0.0f), new Vector2(0.5f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Hornet Sentinel", pd.statueStateHornet2.hasBeenSeen), new Vector2(31.6f, -13.1f), new Vector2(0.3f, 0.2f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Delicate Flower - Goal"), new Vector2(-13.9f, -8f), new Vector2(0.2f, 0.15f));
                AudioRegionsHandler.addAudioRegion(new AudioRegion("Delicate Flower - Origin"), new Vector2(22.9f, -3.3f), new Vector2(0.15f, 0.15f));


                bool t = true;
                AudioRegionsHandler.addAudioRegion(new CondAudioRegion("Delicate Flower - Escort", ref t), new Vector2(-13.9f, -8f), new Vector2(0.2f, 0.15f));

                AudioRegionsHandler.set_visibility(true);

                foreach (AudioRegion audioRegion in AudioRegionsHandler.audioRegions)
                {
                    if (audioRegion != null)
                    {
                        Log(audioRegion.name);
                        audioRegion.gameObject.transform.SetParent(GameManager.instance.gameMap.transform);

                        audioRegion.gameObject.transform.position -= getMapCursorPosition();
                        Log(audioRegion.gameObject.transform.localPosition);

                        if (audioRegion.GetType() == typeof(AudioRegion))
                        {
                            audioRegion.gameObject.GetComponent<SpriteRenderer>().sprite = s;
                        }
                        else if (audioRegion.GetType() == typeof(CondAudioRegion))
                        {
                            audioRegion.gameObject.GetComponent<SpriteRenderer>().sprite = green_square;
                        }
                        
                        Log("instantiated: " + audioRegion.name);


                        audioRegion.gameObject.layer = 5;

                    }
                }

                

                Log("MapMarkerMenu Open");
                
                
                
                

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


                    customCursor.transform.SetParent(placementCursor.transform);

                    foreach (AudioRegion audioRegion in AudioRegionsHandler.audioRegions)
                    {
                        audioRegion.gameObject.transform.SetPositionZ(customCursor.transform.position.z);
                        if (audioRegion.gameObject.GetComponent<SpriteRenderer>().bounds.Intersects(customCursor.GetComponent<SpriteRenderer>().bounds))
                        {
                            if (!audioRegion.gameObject.GetComponent<AudioSource>().isPlaying)
                            {
                                Log(audioRegion.name + " collision");
                            }
                            audioRegion.Play();
                            
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
        private Sprite LoadCustomSprite(string fileName)
        {
            byte[] fileData = File.ReadAllBytes(Application.dataPath+"\\Managed\\Mods\\HK_AudioMod\\" + fileName);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); // Automatically resizes the texture
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }


        public IEnumerator GetAndSetAudioClip(string fileName, AudioClip ac)
        {
            UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(Application.dataPath + "\\Managed\\Mods\\HK_AudioMod\\Audio\\" + fileName + ".mp3", AudioType.MPEG);

            yield return webRequest.SendWebRequest();

            AudioClip clip = DownloadHandlerAudioClip.GetContent(webRequest);
            clip.name = fileName;
            ac = clip;
            nameClipMap.Add(fileName, ac);
        }
    }
    

}

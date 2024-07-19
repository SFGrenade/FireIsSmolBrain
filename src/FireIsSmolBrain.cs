//#define VERSION_1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if !VERSION_1
using GlobalEnums;
#endif
using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FireIsSmolBrain
{
    class FireIsSmolBrain : Mod
    {
        private GameObject _totemPrefab;

#if !VERSION_1
        public override string GetVersion() => "Electric Boogaloo";

        public FireIsSmolBrain() : base("Fire is smol brain 2")
#else
        public override string GetVersion() => "Fire is smol brain";

        public FireIsSmolBrain() : base("Fire is smol brain")
#endif
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneManagerActiveSceneChanged;
        }

        public override List<ValueTuple<string, string>> GetPreloadNames()
        {
            return new List<ValueTuple<string, string>>
            {
                new ValueTuple<string, string>("White_Palace_18", "Soul Totem white_Infinte")
            };
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            _totemPrefab = preloadedObjects["White_Palace_18"]["Soul Totem white_Infinte"];
        }

        private void OnSceneManagerActiveSceneChanged(Scene from, Scene to)
        {
            switch (to.name)
            {
                case "Town":
                    GameObject inst = Object.Instantiate(_totemPrefab);
                    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(inst, to);
                    inst.transform.position = new Vector3(137f, 12.75f, inst.transform.position.z);
                    {
                        var fsm = inst.LocateMyFSM("soul_totem");
                        typeof(PlayMakerFSM).GetField("fsmTemplate", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(fsm, null);
                        var fofgpAction = (FlingObjectsFromGlobalPool) fsm.FsmStates.First(x => x.Name.Equals("Hit")).Actions[8];
                        fofgpAction.spawnMin = 50;
                        fofgpAction.spawnMax = 50;
                    }
                    inst.SetActive(true);
                    break;
#if !VERSION_1
                case "Crossroads_04":
                    MakeBlocker(to, new Vector3(94.5f, 19.5f), new Vector2(17, 11));
                    break;
                case "Crossroads_09":
                    MakeBlocker(to, new Vector3(61.5f, 13.5f), new Vector2(25, 21));
                    break;
                case "Crossroads_10":
                    MakeBlocker(to, new Vector3(28.5f, 35), new Vector2(35, 18));
                    break;
                case "Ruins1_24":
                    MakeBlocker(to, new Vector3(21, 32.5f), new Vector2(32, 9));
                    MakeBlocker(to, new Vector3(26.5f, 42.5f), new Vector2(43, 11));
                    MakeBlocker(to, new Vector3(34.5f, 53), new Vector2(49, 10));
                    MakeBlocker(to, new Vector3(25, 17), new Vector2(34, 16));
                    break;
                case "Ruins1_31b":
                    MakeBlocker(to, new Vector3(30.5f, 45), new Vector2(27, 14));
                    break;
                case "Ruins2_03":
                    MakeBlocker(to, new Vector3(43.5f, 80.5f), new Vector2(39, 23));
                    break;
                case "Ruins2_11":
                    MakeBlocker(to, new Vector3(54.5f, 100), new Vector2(31, 12));
                    break;
                case "Fungus1_04":
                    MakeBlocker(to, new Vector3(26.5f, 36.5f), new Vector2(23, 19));
                    break;
                case "Fungus1_20_v02":
                    MakeBlocker(to, new Vector3(47.5f, 17.5f), new Vector2(15, 11));
                    break;
                case "Fungus1_29":
                    MakeBlocker(to, new Vector3(49.5f, 9.5f), new Vector2(43, 7));
                    break;
                case "Fungus2_15":
                    MakeBlocker(to, new Vector3(30, 10), new Vector2(26, 8));
                    break;
                case "Fungus3_23":
                    MakeBlocker(to, new Vector3(40, 32), new Vector2(40, 8));
                    break;
                case "Fungus3_archive_02":
                    MakeBlocker(to, new Vector3(53, 120), new Vector2(38, 36));
                    break;
                case "Mines_18":
                    MakeBlocker(to, new Vector3(30, 13), new Vector2(20, 6));
                    break;
                case "Mines_32":
                    MakeBlocker(to, new Vector3(30, 17.5f), new Vector2(22, 15));
                    break;
                case "Deepnest_32":
                    MakeBlocker(to, new Vector3(94, 11), new Vector2(50, 18));
                    break;
                case "Deepnest_East_Hornet":
                    MakeBlocker(to, new Vector3(27, 33), new Vector2(24, 14));
                    break;
                case "Abyss_19":
                    MakeBlocker(to, new Vector3(27, 34), new Vector2(24, 15));
                    break;
                case "Waterways_05":
                    MakeBlocker(to, new Vector3(76, 13), new Vector2(34, 16));
                    break;
                case "Waterways_12":
                    MakeBlocker(to, new Vector3(20, 17), new Vector2(36, 30));
                    break;
                case "Hive_05":
                    MakeBlocker(to, new Vector3(69, 33), new Vector2(24, 14));
                    break;
                case "Grimm_Main_Tent":
                    MakeBlocker(to, new Vector3(87, 15), new Vector2(44, 20));
                    break;
#endif
                default:
                    break;
            }
        }

#if !VERSION_1
        private void MakeBlocker(Scene sc, Vector3 p, Vector2 si)
        {
            var blocker = new GameObject("ArenaBlocker", typeof(BoxCollider2D));
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(blocker, sc);
            blocker.layer = (int) PhysLayers.TERRAIN;
            blocker.transform.position = p;
            blocker.GetComponent<BoxCollider2D>().size = si;
        }
#endif
    }
}

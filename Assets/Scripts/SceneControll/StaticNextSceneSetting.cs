using Assets.Scripts.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneControll
{
    public static class StaticNextSceneSetting
    {
        public static string SceneName;
        public static string battleFile;
        public static TextAsset inkFile;
        public static TextAsset inkFile0;
        public static TextAsset inkFile1;
        public static MenuController menu;
        private static Queue<Tuple<string, string>> sceneOrder;

        public static void SetInkData(string filename) 
        {
            inkFile = Resources.Load("Dialogues/" + filename) as TextAsset;
        }

        public static void SetNextSceneType(string sceneName)
        {
            SceneName = sceneName;
        }

        public static void Init()
        {
            inkFile0 = menu.asset0;
            inkFile1 = menu.asset1;
            Debug.Log(inkFile0.name);
            Debug.Log(inkFile1.name);
            sceneOrder = new Queue<Tuple<string, string>>();
            sceneOrder.Enqueue(new Tuple<string, string>("Novelle", "Start"));
            sceneOrder.Enqueue(new Tuple<string, string>("Battle", "Prologue"));
            sceneOrder.Enqueue(new Tuple<string, string>("Novelle", "Prologue_part_2"));
            sceneOrder.Enqueue(new Tuple<string, string>("Battle", "Graveyard"));
        }

        public static void MoveToNextScene()
        {
            if (sceneOrder.Count <= 0)
                return;
            var t = sceneOrder.Dequeue();
            SceneName = t.Item1;
            if (SceneName == "Battle")
                BattleSettingsStatic.SetData(Resources.Load("ScriptableObjects/Battle/" + t.Item2) as BattleSettingsData);
            else if (SceneName == "Novelle")
            //  BattleSettingsStatic.SetData(Resources.Load("Dialogues/" + t.Item2) as BattleSettingsData);
                inkFile = t.Item2 == "Start" ? inkFile0 : inkFile1;
            SceneManager.LoadScene(SceneName);
        }
    }
}

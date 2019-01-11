using IllusionPlugin;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using BeatSaberConsole.Util;

namespace BeatSaberConsole
{
    public class Plugin : IPlugin
    {
        public string Name => "BeatSaberConsole";
        public string Version => "1.0.0";

        internal static char[] prefixCutoffs = new char[] { ']', '|', ':' };

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            new GameObject("Beat Saber Console").AddComponent<ConsoleWindow>();
            new GameObject("Beat Saber Output Log").AddComponent<OutputLog>();
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1) { }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
            try
            {
                if (arg0.name == "Menu") UI.InitUI();
            }catch(Exception e)
            {
                Log("Ah sheet, menu failed.", LogInfo.Fatal);
                Log(e.ToString());
            }
            if (arg0.name == "HealthWarning")
            {
                GameObject.Find("Beat Saber Console").AddComponent<Moveable>();
                GameObject.Find("Beat Saber Output Log").AddComponent<Moveable>();
            }
            foreach (Moveable moveable in GameObject.FindObjectsOfType<Moveable>())
            {
                moveable.found = false;
            }
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public void OnLevelWasLoaded(int level) { }

        public void OnLevelWasInitialized(int level) { }

        public void OnUpdate() { }

        public void OnFixedUpdate() { }

        public enum LogInfo {
            Info, Warning, Error, Fatal
        }
        
        public static void Log(string m)
        {
            Log(m, LogInfo.Info);
        }

        public static void Log(string m, LogInfo l)
        {
            Console.WriteLine($"[BS Console | {l.ToString()}] {m}");
        }
    }
}

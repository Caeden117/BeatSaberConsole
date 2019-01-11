using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using VRUIControls;
using System.Threading;
using UnityEngine.SceneManagement;

namespace BeatSaberConsole
{
    class Moveable : MonoBehaviour
    {
        private GameObject cube;
        private Material moveableMat;
        internal bool found = false;

        void Awake()
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = $"{(transform.name.IndexOf("Output") == -1 ? "Console" : "Output" )} Moveable";
            cube.transform.parent = transform;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(cube);
            DontDestroyOnLoad(this);
            new Thread(() => { grabPointer(); }).Start();
            SceneManager.sceneLoaded += Loaded;
        }

        void Loaded(Scene arg, LoadSceneMode lolPants)
        {
            if ((arg.name == "HealthWarning" || arg.name == "Menu") && !found) grabPointer();
        }

        Task grabPointer()
        {
            return Task.Run(() => {
                while (true)
                {
                    try
                    {
                        VRPointer pointer = Resources.FindObjectsOfTypeAll<VRPointer>().FirstOrDefault();
                        if (pointer != null)
                        {
                            Material material = Resources.FindObjectsOfTypeAll<Material>().Where(m => m.name == "GlassHandle").FirstOrDefault();
                            if (material)
                            {
                                moveableMat = new Material(material);
                            }
                            if (pointer.gameObject.GetComponent<Movement>() == null)
                            {
                                var movePointer = pointer.gameObject.AddComponent<Movement>();
                                movePointer.Init(cube.transform);
                            }
                            found = true;
                            foreach(Moveable hi in FindObjectsOfType<Moveable>())
                            {
                                hi.found = true;
                            }
                            break;
                        }
                        Thread.Sleep(10);
                    }
                    catch(Exception e) { Plugin.Log(e.ToString(), Plugin.LogInfo.Error); } //Issues where during scene transitions this *may or may not* crash Beat Saber. Le feex.
                }
            });
        }

        void Update()
        {
            cube.transform.rotation = transform.rotation;
            cube.transform.localPosition = new Vector3(-0.5f, -0.55f, 0);
            cube.transform.localScale = new Vector3(1f, 0.1f, 0);
            cube.GetComponent<Renderer>().material = moveableMat;
            if (Config.showMoveCubes)
                cube.GetComponent<Renderer>().enabled = true;
            else
                cube.GetComponent<Renderer>().enabled = false;
        }
    }
}

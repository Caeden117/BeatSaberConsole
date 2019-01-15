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
                        if (pointer != null && found == false)
                        {
                            Material material = Resources.FindObjectsOfTypeAll<Material>().Where(m => m.name == "GlassHandle").FirstOrDefault();
                            if (material) moveableMat = new Material(material);
                            /*
                             * Big error boi happens right here. It's preventing me from pushing out another test build.
                             * 
                             * Not even the ol' reliable try/catch statement can contain this beast.
                             * IDK sometimes on scene transition/game start it crashes because of an invalid address.
                             * Everything in output_log.txt is telling me its when adding the component, which I dont think is possible?
                             * 
                             * For one, a component has to have a gameobject. Two, I have an if statement up above which checks to see if
                             * the pointer its found is not null. Movement *should* have a perfectly valid address.
                             * 
                             * I cant find out why.
                             * 
                             * If anyone can feex this and send me a PR I will forever be in debt.
                             */
                            if (pointer.gameObject.GetComponent<Movement>() == null)
                            {
                                var movePointer = pointer.gameObject.AddComponent<Movement>(); //output_log.txt leads me to believe this line of code is the source.
                                movePointer.Init(cube.transform);
                            }
                            //Like literally this if statement right up here can crash the entire game.
                            found = true;
                            foreach (Moveable hi in FindObjectsOfType<Moveable>())
                            {
                                hi.found = true;
                            }
                            break;
                        }
                        else if (found) break;
                        Thread.Sleep(10);
                    }
                    catch(Exception e) { Plugin.Log(e.ToString(), Plugin.LogInfo.Error); }
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

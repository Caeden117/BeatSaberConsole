﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;
using VRUIControls;
using System.Threading;
using System.Windows.Forms;

namespace BeatSaberConsole
{
    class ConsoleWindow : MonoBehaviour
    {
        private TextMeshPro consoleTMP;
        private StringWriter console = new StringWriter();
        enum ColorCode
        {
            LOG = 0,
            WARNING = 1,
            ERROR = 2,
            FATAL = 3,
        }

        private string[] seperatedConsole = new string[] { };
        private List<string> colorisedConsole = new List<string>(){};

        private bool isException = false;
        private List<string> lastException = new List<string>() { };

        void Awake()
        {
            Plugin.Log("Uh oh! This console has been moved over to inside Beat Saber!", Plugin.LogInfo.Warning);
            DontDestroyOnLoad(gameObject);
            consoleTMP = gameObject.AddComponent<TextMeshPro>();
            consoleTMP.fontSize = 0.25f;
            consoleTMP.alignment = TextAlignmentOptions.BottomLeft;
            consoleTMP.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
            consoleTMP.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            StartCoroutine(WaitToUpdate());

            Console.SetOut(console);
        }

        void OnDestroy()
        {
            console.Close();
            Plugin.Log("Console has returned to its default console window.");
        }

        IEnumerator WaitToUpdate()
        {
            while (true)
            {
                UpdateConsole();
                yield return new WaitForSeconds(Config.updateTime);
            }
        }

        void Update()
        {
            consoleTMP.gameObject.transform.position = Config.consolePos;
            consoleTMP.gameObject.transform.rotation = Quaternion.Euler(Config.consoleRot);
        }

        void UpdateConsole()
        {
            try
            {
                seperatedConsole = console.ToString().Split('\n');
                colorisedConsole.Clear();
                int offset = seperatedConsole.Count() - Config.lines - 1;
                if (offset < 0) offset = 0;
                for (var i = offset; i < seperatedConsole.Count(); i++)
                {
                    try
                    {
                        string line = seperatedConsole[i];
                        string prefix = "";
                        char[] prefixCutoffs = Plugin.prefixCutoffs;
                        List<int> prefixIndex = new List<int>();
                        foreach (char end in prefixCutoffs)
                        {
                            if (line.IndexOf(end) != -1) prefixIndex.Add(line.IndexOf(end));
                        }
                        prefixIndex.Sort();
                        prefix = line.Substring(0, prefixIndex.Last());

                        if (line.ToUpper().Contains("EXCEPTION"))
                        {
                            if (!isException) isException = true;
                            lastException.Clear();
                            lastException.Add("Generated by Beat Saber Console. To turn this off, disable the \"Copy Last Exception\" option in settings.");
                            lastException.Add(line);
                            colorisedConsole.Add($"<color=#550000>{line}</color>");
                        }
                        else if (line.ToUpper().Contains(" AT") && isException)
                        {
                            if (isException) lastException.Add(line);
                            if (colorisedConsole.Last().StartsWith("<color=#550000>"))
                                colorisedConsole.Add($"<color=#550000>{line}</color>");
                        }
                        else
                        {
                            if (isException)
                            {
                                isException = false;
                                if (Config.copyToClipboard)
                                {
                                    CopyToClipboard();
                                    Plugin.Log("Automatically copied this last exception to the Clipboard!", Plugin.LogInfo.Warning);
                                }
                            }
                            if (prefix.ToUpper().Contains("WARN") || prefix.ToUpper().Contains("WARNING"))
                                colorisedConsole.Add($"<color=#FFA500>{line}</color>");
                            else if (prefix.ToUpper().Contains("ERR") || prefix.ToUpper().Contains("ERROR"))
                                colorisedConsole.Add($"<color=#FF0000>{line}</color>");
                            else if (prefix.ToUpper().Contains("FATAL"))
                                colorisedConsole.Add($"<color=#550000>{line}</color>");
                            else
                                colorisedConsole.Add($"<color=#555555>{line}</color>");
                        }
                    }
                    catch { }
                }
                consoleTMP.text = $"<size=400%>Beat Saber Console</size><size=200%> By Caeden117</size> Showing {Config.lines} lines\n";
                consoleTMP.text += String.Join("\n", colorisedConsole);
            }
            catch (Exception e)
            {
                consoleTMP.text = $"<size=400%>Failed to Load Console</size>\n";
                consoleTMP.text += e.ToString();
            }
        }

        [STAThread]
        void CopyToClipboard()
        {
            Clipboard.SetText(String.Join(Environment.NewLine, lastException));
        }
    }
}

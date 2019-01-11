using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using System.IO;

namespace BeatSaberConsole
{
    class OutputLog : MonoBehaviour
    {
        private TextMeshPro consoleTMP;
        private List<string> console = new List<string>();
        enum ColorCode
        {
            LOG = 0,
            WARNING = 1,
            ERROR = 2,
            FATAL = 3,
        }
        
        private List<string> colorisedConsole = new List<string>() { };

        void Awake()
        {
            Plugin.Log("Initializing output log...", Plugin.LogInfo.Warning);
            DontDestroyOnLoad(gameObject);
            consoleTMP = gameObject.AddComponent<TextMeshPro>();
            consoleTMP.fontSize = 0.25f;
            consoleTMP.alignment = TextAlignmentOptions.BottomLeft;
            consoleTMP.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
            consoleTMP.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            Application.logMessageReceivedThreaded += receive;
        }

        private void receive(string logString, string stackTrace, LogType type)
        {
            if (stackTrace.IndexOf("ScoreSaber") != -1 && (type == LogType.Error || type == LogType.Exception))
            {
                if (Config.hideCommonErrors) return;
                console.Add("God damnit Umby fix your ScoreSaber exceptions >=( >=( >=(");
            }
            else if (stackTrace.IndexOf("CameraPlus") != -1 && (type == LogType.Error || type == LogType.Exception))
            {
                if (Config.hideCommonErrors) return;
                console.Add("CameraPlus exceptions plz :pepeHands:");
            }
            else
            {
                if (Config.showStacktrace)
                    console.Add($"{logString}\n{stackTrace}");
                else
                    console.Add($"{logString}");
            }
        }

        void Update()
        {
            consoleTMP.gameObject.transform.position = Config.outputPos;
            consoleTMP.gameObject.transform.rotation = Quaternion.Euler(Config.outputRot);
            colorisedConsole.Clear();
            try
            {
                int offset = console.Count() - Config.lines - 1;
                if (offset < 0) offset = 0;
                for (var i = 0; i < console.Count(); i++)
                {
                    string line = console[i];
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
                        colorisedConsole.Add($"<color=#550000>{line}</color>");
                    else if (line.ToUpper().Contains(" AT"))
                    {
                        if (colorisedConsole.Last().StartsWith("<color=#550000>"))
                            colorisedConsole.Add($"<color=#550000>{line}</color>");
                    }
                    else
                    {
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
                consoleTMP.text = $"<size=400%>Output Log</size>Updating in real time!\n";
                consoleTMP.text += String.Join("\n", colorisedConsole);
            }
            catch { } //TeamViewer is a legend
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using IllusionPlugin;

namespace BeatSaberConsole
{
    public class Config
    {
        public static int lines
        {
            get
            {
                return ModPrefs.GetInt("Beat Saber Console", "Lines Displayed", 15, true);
            }
            set
            {
                ModPrefs.SetInt("Beat Saber Console", "Lines Displayed", value);
            }
        }

        public static float updateTime
        {
            get
            {
                return ModPrefs.GetFloat("Beat Saber Console", "Console Update Time", 1, true);
            }
            set
            {
                ModPrefs.SetFloat("Beat Saber Console", "Console Update Time", value);
            }
        }

        public static bool copyToClipboard
        {
            get
            {
                return ModPrefs.GetBool("Beat Saber Console", "Copy Last Exception To Clipboard", false, true);
            }
            set
            {
                ModPrefs.SetBool("Beat Saber Console", "Copy Last Exception To Clipboard", value);
            }
        }

        public static bool showStacktrace
        {
            get
            {
                return ModPrefs.GetBool("Beat Saber Console", "Output Show Exception Stacktrace", false, true);
            }
            set
            {
                ModPrefs.SetBool("Beat Saber Console", "Output Show Exception Stacktrace", value);
            }
        }

        public static bool hideCommonErrors
        {
            get
            {
                return ModPrefs.GetBool("Beat Saber Console", "Hide Common Output Errors", true, true);
            }
            set
            {
                ModPrefs.SetBool("Beat Saber Console", "Hide Common Output Errors", value);
            }
        }

        public static bool showMoveCubes
        {
            get
            {
                return ModPrefs.GetBool("Beat Saber Console", "Show Movement Bar", true, true);
            }
            set
            {
                ModPrefs.SetBool("Beat Saber Console", "Show Movement Bar", value);
            }
        }

        public static Vector3 consolePos
        {
            get
            {
                string stringPos = ModPrefs.GetString("Beat Saber Console", "Console Position", "1|2.5|0", true);
                string[] splitPos = stringPos.Split('|');
                return new Vector3(float.Parse(splitPos[0]), float.Parse(splitPos[1]), float.Parse(splitPos[2]));
            }
            set
            {
                ModPrefs.SetString("Beat Saber Console", "Console Position", $"{value.x}|{value.y}|{value.z}");
            }
        }

        public static Vector3 consoleRot
        {
            get
            {
                string stringPos = ModPrefs.GetString("Beat Saber Console", "Console Rotation", "-90|0|0", true);
                string[] splitPos = stringPos.Split('|');
                return new Vector3(float.Parse(splitPos[0]), float.Parse(splitPos[1]), float.Parse(splitPos[2]));
            }
            set
            {
                ModPrefs.SetString("Beat Saber Console", "Console Rotation", $"{value.x}|{value.y}|{value.z}");
            }
        }

        public static Vector3 outputPos
        {
            get
            {
                string stringPos = ModPrefs.GetString("Beat Saber Console", "Output Position", "0|2.5|0", true);
                string[] splitPos = stringPos.Split('|');
                return new Vector3(float.Parse(splitPos[0]), float.Parse(splitPos[1]), float.Parse(splitPos[2]));
            }
            set
            {
                ModPrefs.SetString("Beat Saber Console", "Output Position", $"{value.x}|{value.y}|{value.z}");
            }
        }

        public static Vector3 outputRot
        {
            get
            {
                string stringPos = ModPrefs.GetString("Beat Saber Console", "Output Rotation", "-90|0|0", true);
                string[] splitPos = stringPos.Split('|');
                return new Vector3(float.Parse(splitPos[0]), float.Parse(splitPos[1]), float.Parse(splitPos[2]));
            }
            set
            {
                ModPrefs.SetString("Beat Saber Console", "Output Rotation", $"{value.x}|{value.y}|{value.z}");
            }
        }
    }
}

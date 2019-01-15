﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomUI.Settings;
using UnityEngine;

namespace BeatSaberConsole.Util
{
    public class UI
    {
        public static void InitUI()
        {
            SubMenu menu = SettingsUI.CreateSubMenu("Beat Saber Console");
            var lines = menu.AddInt("Line Count", "How many lines the Console will display.", 15, 50, 5);
            lines.GetValue += delegate { return Config.lines; };
            lines.SetValue += v => Config.lines = v;

            float[] timeOptions = new float[] {
                0.1f, 0.25f, 0.5f, 0.75f, 1, 1.5f, 2, 3, 4, 5
            };
            var time = menu.AddList("Console Update Time", timeOptions, "The amount of time between Console updates in seconds.");
            time.GetValue += delegate { return Config.updateTime; };
            time.SetValue += v => Config.updateTime = v;
            time.FormatValue += delegate (float c) { return $"{c} Sec."; };

            var cubes = menu.AddBool("Show Move Bars", "Show the bars that appear under the console.\n<color=#FF0000>Moving the console and output log by clicking under them will still work.</color>");
            cubes.GetValue += delegate { return Config.showMoveCubes; };
            cubes.SetValue += v => Config.showMoveCubes = v;

            var clipboard = menu.AddBool("Copy Last Exception", "Automatically copies the Console's last logged Exception to your clipboard for easy pasting.\n<color=#FF0000>This will overwrite any previous clipboard entries.</color>");
            clipboard.GetValue += delegate { return Config.copyToClipboard; };
            clipboard.SetValue += v => Config.copyToClipboard = v;

            var common = menu.AddBool("Hide Common Errors", "Hide common errors from appearing in the Output Log.");
            common.GetValue += delegate { return Config.hideCommonErrors; };
            common.SetValue += v => Config.hideCommonErrors = v;

            var showStacktrace = menu.AddBool("Show Stacktrace", "Shows stacktrace in Output Log.");
            showStacktrace.GetValue += delegate { return Config.showStacktrace; };
            showStacktrace.SetValue += v => Config.showStacktrace = v;

            var reset = menu.AddBool("Reset Positions?", "When set to true, the Console and Output log will reset to above the player.");
            reset.GetValue += delegate { return false; };
            reset.SetValue += v =>
            {
                if (v)
                {
                    Config.consolePos = new Vector3(1, 2.5f, 0);
                    Config.consoleRot = new Vector3(-90, 0, 0);
                    Config.outputPos = new Vector3(0, 2.5f, 0);
                    Config.outputRot = new Vector3(-90, 0, 0);
                }
            };
        }
    }
}

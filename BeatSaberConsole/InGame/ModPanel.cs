using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using IllusionInjector;
using IllusionPlugin;

namespace BeatSaberConsole
{
    [Obsolete("Not in use anymore. If you want to take a look feel free.")]
    public class ModPanel : MonoBehaviour
    {
        private GameObject healthPanel;
        private TextMeshProUGUI header;
        private TextMeshProUGUI body;

        void Awake()
        {
            StartCoroutine(GetMainScreen());
        }

        IEnumerator GetMainScreen()
        {
            while (true)
            {
                healthPanel = GameObject.Find("MainScreen");
                if (healthPanel != null) break;
                yield return new WaitForSeconds(0.1f);
            }
            Init();
        }

        void Init()
        {
            header = healthPanel.transform.Find("HeaderPanel").Find("Text").GetComponent<TextMeshProUGUI>();
            header.text = "Beat Saber Console - Mod List";
            body = healthPanel.transform.Find("HealthWarning").Find("Text").GetComponent<TextMeshProUGUI>();
            body.text = "";
            foreach (IPlugin plugin in PluginManager.Plugins)
            {
                body.text += $"{plugin.Name} - {plugin.Version}\n";
            }
        }

    }
}

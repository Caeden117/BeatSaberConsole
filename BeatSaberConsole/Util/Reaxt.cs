using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SorrylolPantsReaxtRequestedIt = UnityEngine.MonoBehaviour;
using TMPro;

namespace BeatSaberConsole.Uhh
{
    class Reaxt : SorrylolPantsReaxtRequestedIt
    {
        private string[] train = new string[] {
            "      ====        ________                 ___________ ",
            "     _|  |_______/        \\__I_I_____===__|_________| ",
            "    |(_)---  |   H\\________/ |   |       =|___ ___|   ",
            "   /     |  |   H  |  |     |   |          ||_| |_||   ",
            "  |      |  |   H  |__---------------------| [___] |   ",
            "  | ________|___H__/__|_____/[][]~\\_______|       |   ",
            "  |/ |   |-----------I_____I [][] []  D    |=======|__ ",
            "__/ =| o |=-~~\\  /~~\\  /~~\\  /~~\\ ____Y___________|__ ",
            "  |-=|___|=    ||    ||    ||    |_____/~\\___/        ",
            "    \\_/     \\O=====O=====O=====O_/      \\_/            "
        };

        [Obsolete("It doesnt work in Beat Saber because of the font REEEE")]
        void Awake()
        {
            foreach (string piece in train)
            {
                Console.WriteLine($"     {piece}");
            }
        }
    }
}

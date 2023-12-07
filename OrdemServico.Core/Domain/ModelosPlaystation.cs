using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Core.Domain
{
    public static class ModelosPlaystation
    {
        public static List<string> ModelosConsole { get; }
        public static List<string> ModelosAcessorio { get; }
        public static List<string> ModelosControle { get; }
        public static List<string> ModelosComponente { get; }

        static ModelosPlaystation()
        {
            ModelosConsole = new()
            {
                "Playstation",
                "Playstation 2",
                "Playstation 3",
                "Playstation 4",
                "Playstation 5",
                "PSP",
                "PSVita"
            };

            ModelosAcessorio = new()
            {
                "Hd Camera",
                "Pulse3d Wireless HeadSet",
                "DualSense Charging Station",
                "PSVR",
                "PSVR Aim Controller",
                "Memory Card"
            };

            ModelosControle = new()
            {
                "Dualshock",
                "Dualshock 2",
                "Dualshock 3",
                "Dualshock 4",
                "Dualsense",
            };

            ModelosComponente = new()
            {
                "Cabo USB",
                "Baterias",
                "Cabo AV",
                "Cabo VGA",
                "Cabo HDMI",
                "Fonte"
            };
        }
    }
}

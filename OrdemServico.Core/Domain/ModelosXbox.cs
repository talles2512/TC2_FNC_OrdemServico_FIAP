using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Core.Domain
{
    public static class ModelosXbox
    {
        public static List<string> ModelosConsole { get; }
        public static List<string> ModelosAcessorio { get; }
        public static List<string> ModelosControle { get; }
        public static List<string> ModelosComponente { get; }

        static ModelosXbox()
        {
            ModelosConsole = new()
            {
                "Xbox",
                "Xbox 360",
                "Xbox One",
                "Xbox Series"
            };

            ModelosAcessorio = new()
            {
                "Kinect",
                "Xbox Wireless HeadSet",
                "Play & Charge",
                "Xbox Seagate",
                "Bluetooth Adapter"
            };

            ModelosControle = new()
            {
                "Xbox Classic Controller",
                "Xbox 360 Controller",
                "Xbox One Controller",
                "Xbox One Elite Controller",
                "Xbox Series Controller",
                "Xbox Adaptive Controller"
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

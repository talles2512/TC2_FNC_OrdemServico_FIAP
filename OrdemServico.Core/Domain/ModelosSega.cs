using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Core.Domain
{
    public static class ModelosSega
    {
        public static List<string> ModelosConsole { get; }
        public static List<string> ModelosAcessorio { get; }
        public static List<string> ModelosControle { get; }
        public static List<string> ModelosComponente { get; }

        static ModelosSega()
        {
            ModelosConsole = new()
            {
                "SG-1000",
                "Master System",
                "Mega Drive",
                "Sega CD",
                "Sega Game Gear",
                "Sega Saturn",
                "Sega 32x",
                "Nomad",
                "DreamCast",
                "MegaDrive Mini"
            };

            ModelosAcessorio = new()
            {
                "Sega Light Phaser",
                "Sega 3D Glasses",
                "Sega Power Base Converter",
                "Sega Team Player",
                "Sega Netlink",
                "Sega DreamCast VMU"
            };

            ModelosControle = new()
            {
                "Mega Drive Controller",
                "DreamCast Controller",
                "Sega 6-button Controller",
                "Sega DreamCast Fishing"
            };

            ModelosComponente = new()
            {
                "Cabo Coaxial",
                "Cabo AV",
                "Cabo VGA",
                "Fonte"
            };
        }
    }
}

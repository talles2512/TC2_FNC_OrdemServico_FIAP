using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Core.Domain
{
    public static class ModelosNintendo
    {
        public static List<string> ModelosConsole { get; }
        public static List<string> ModelosAcessorio { get; }
        public static List<string> ModelosControle { get; }
        public static List<string> ModelosComponente { get; }

        static ModelosNintendo()
        {
            ModelosConsole = new()
            {
                "Game & Watch",
                "Nintendo Entertainment System",
                "Super Nintendo Entertainment System",
                "Nintendo 64",
                "Gameboy Advanced",
                " DS",
                "GameCube",
                "Wii",
                " 3DS",
                "Switch"
            };

            ModelosAcessorio = new()
            {
                "Amiibo",
                "JoyCon Grip",
                "Mario Kart Live Home Circuit",
                "Super NES Mouse"
            };

            ModelosControle = new()
            {
                "JoyCon",
                "Controller Pro",
                "Wii Remote",
                "Nunchuk",
                "NES Controller",
                "SNES Controller",
                "64 Controller"
            };

            ModelosComponente = new()
            {
                "Dock",
                "Cabo USB",
                "Baterias",
                "Cabo Coaxial",
                "Cabo AV",
                "Cabo VGA",
                "Cabo HDMI",
                "Fonte"
            };
        }
    }
}

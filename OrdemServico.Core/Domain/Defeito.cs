using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Core.Domain
{
    public static class Defeito
    {
        public static List<string> Defeitos { get; }

        static Defeito()
        {
            Defeitos = new()
            {
                "Superaquecimento",
                "Problemas de Conexão e Portas",
                "Problemas de Leitura de Mídia",
                "Atualizações de Software Problemáticas",
                "Problemas Conectividade de Rede",
                "Problemas de Energia",
                "Falhas de Hardware Gerais",
                "Desgaste de Controles",
                "Problemas de Armazenamento",
                "Problemas de Áudio e Vídeo",
                "Problemas de Compatibilidade",
                "Falhas de Sistema e Erros de Software"
            };
        }
    }
}

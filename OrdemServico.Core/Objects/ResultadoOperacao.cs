using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Core.Objects
{
    public class ResultadoOperacao<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Retorno { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BuscaBidirecional
{
    public class ProblemaMapa : IProblema<Local>
    {
        public Mapa Mapa { get; set; }

        public Local Origem { get; set; }

        public Local Destino { get; set; }

        public IEnumerable<Local> Caminhos(Local local) =>
            local.Ligacoes;
    }
    
    public class No<T>
    {
        public No<T> Pai { get; set; }

        public T Estado { get; set; }

        public int Profundidade { get; set; }
    }
}

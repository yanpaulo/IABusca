using System.Collections.Generic;

namespace BuscaBidirecional
{
    public class ProblemaMapa : IProblema<Local>
    {
        public Mapa Mapa { get; set; }

        public Local Inicial { get; set; }

        public Local Objetivo { get; set; }

        public IEnumerable<Local> Acoes(Local local) =>
            local.Ligacoes;
    }
}

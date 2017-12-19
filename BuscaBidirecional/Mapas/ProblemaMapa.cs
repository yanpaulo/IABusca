using BuscaBidirecional.Mapas;
using System.Collections.Generic;

namespace BuscaBidirecional.Mapas
{
    public class ProblemaMapa : IProblema<Local>
    {
        public Mapa Mapa { get; set; }

        public Local Inicial { get; set; }

        public Local Destino { get; set; }

        public IEnumerable<Local> Acoes(Local local) =>
            local.Ligacoes;

        public bool TestaObjetivo(Local estado) =>
            estado == Destino;
    }
}

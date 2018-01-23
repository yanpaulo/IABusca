using IABusca.Mapas;
using System.Collections.Generic;
using System.Linq;

namespace IABusca.Mapas
{
    public class ProblemaMapa : IProblemaHeuristica<Local>
    {
        public Mapa Mapa { get; set; }

        public Local Inicial { get; set; }

        public Local Destino { get; set; }

        public IEnumerable<Acao<Local>> Acoes(Local local) =>
            local.Ligacoes.Select(l => new Acao<Local> { Resultado = l.Local, Custo = l.Distancia });

        public bool TestaObjetivo(Local estado) =>
            estado == Destino;

        public int ValorHeuristica(Local estado) =>
            estado.DLR[Destino];
    }
}

using IABusca.Mapas;
using System.Collections.Generic;
using System.Linq;

namespace IABusca.Mapas
{
    public class ProblemaMapa : IProblema<Local>
    {
        public Mapa Mapa { get; set; }

        public Local Inicial { get; set; }

        public Local Destino { get; set; }

        public IEnumerable<Acao<Local>> Acoes(Local local) =>
            local.Ligacoes.Select(l => new Acao<Local> { Estado = local, Resultado = l.Local });

        public bool TestaObjetivo(Local estado) =>
            estado == Destino;
    }
}

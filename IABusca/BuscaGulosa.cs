using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca
{
    public class BuscaGulosa : IAlgoritmo<Local>
    {
        private List<No<Local>> borda = new List<No<Local>>();
        private ProblemaMapa problema;

        public BuscaGulosa(ProblemaMapa problema)
        {
            this.problema = problema;
            borda.Add(new No<Local> { Estado = problema.Inicial });
        }

        public IProblema<Local> Problema => problema;

        public No<Local> Objetivo { get; private set; }

        public IEnumerable<No<Local>> Borda => borda;

        public bool Falha => !Borda.Any();

        public bool AtingiuObjetivo => Objetivo != null;

        public void Expande()
        {
            var pai = borda.OrderBy(n => n.Estado.DLR[problema.Destino]).First();
            borda.Remove(pai);

            var acoes = Problema.Acoes(pai.Estado).Where(a => !Borda.Any(n => a.Resultado.Equals(n.Estado)));

            foreach (var acao in acoes)
            {
                var no = new No<Local>
                {
                    Estado = acao.Resultado,
                    Pai = pai,
                };

                borda.Add(no);

                if (Problema.TestaObjetivo(no.Estado))
                {
                    Objetivo = no;
                    return;
                }
            }
        }

        public string ImprimeListas()
        {
            var str = $@"
Borda:      [{ string.Join(", ", Borda.Select(b => b.Estado.ToString())) }]";
            return str;
        }
    }
}

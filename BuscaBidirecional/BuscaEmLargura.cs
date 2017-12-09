using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmLargura : IAlgoritmo
    {
        private Queue<No> borda = new Queue<No>();

        public BuscaEmLargura(Problema problema)
        {
            Problema = problema;

            Arvore = new Arvore
            {
                Raiz = new No
                {
                    Local = Problema.Origem
                }
            };

            borda.Enqueue(Arvore.Raiz);
        }

        public Problema Problema { get; private set; }

        public Arvore Arvore { get; private set; }

        public No Objetivo { get; private set; }

        public IList<Local> Explorado { get; private set; } = new List<Local>();

        public IEnumerable<No> Borda => borda;
        
        public bool AtingiuObjetivo => Objetivo != null;

        public bool Falha => !Borda.Any();

        public void Expande()
        {
            var no = borda.Dequeue();
            Explorado.Add(no.Local);

            var ligacoes = no.Local.Ligacoes.Where(l => !Explorado.Contains(l) && !borda.Any(b => b.Local == l));

            foreach (var local in ligacoes)
            {
                var filho = new No
                {
                    Pai = no,
                    Local = local
                };
                borda.Enqueue(filho);

                if (local == Problema.Destino)
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}

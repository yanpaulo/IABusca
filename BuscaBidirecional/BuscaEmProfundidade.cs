using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmProfundidade : BuscaBase
    {
        private Stack<No> borda = new Stack<No>();

        public BuscaEmProfundidade(Problema problema) 
            : base(problema)
        {
            borda.Push(Arvore.Raiz);
        }

        public override IEnumerable<No> Borda => borda;

        public override void Expande()
        {
            var no = borda.Pop();
            Explorado.Add(no.Local);

            var ligacoes = no.Local.Ligacoes.Where(l => !Explorado.Contains(l) && !borda.Any(b => b.Local == l));

            foreach (var local in ligacoes)
            {
                var filho = new No
                {
                    Pai = no,
                    Local = local
                };
                borda.Push(filho);

                if (local == Problema.Destino)
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmLargura : BuscaBase
    {
        private Queue<No> borda = new Queue<No>();

        public BuscaEmLargura(Problema problema) : 
            base(problema)
        {
            borda.Enqueue(Arvore.Raiz);
        }

        public override IEnumerable<No> Borda => borda;
        
        public override void Expande()
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

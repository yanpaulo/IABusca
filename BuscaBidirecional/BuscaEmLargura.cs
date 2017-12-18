using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmLargura<T> : BuscaEmArvoreBase<T>
    {
        private Queue<No<T>> borda = new Queue<No<T>>();

        public BuscaEmLargura(IProblema<T> problema) : 
            base(problema)
        {
            borda.Enqueue(Raiz);
        }

        public override IEnumerable<No<T>> Borda => borda;
        
        public override void Expande()
        {
            var no = borda.Dequeue();
            Explorado.Add(no.Estado);
            
            var caminhos = Problema.Caminhos(no.Estado).Where(e => !Explorado.Contains(e) && !borda.Any(b => b.Estado.Equals(e)));

            foreach (var local in caminhos)
            {
                var filho = new No<T>
                {
                    Pai = no,
                    Estado = local,
                    Profundidade = no.Profundidade + 1
                };
                borda.Enqueue(filho);

                if (local.Equals(Problema.Destino))
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}

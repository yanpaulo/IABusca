using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmProfundidade<T> : BuscaBase<T>
    {
        private Stack<No<T>> borda = new Stack<No<T>>();

        public BuscaEmProfundidade(IProblema<T> problema) 
            : base(problema)
        {
            borda.Push(Raiz);
        }

        public override IEnumerable<No<T>> Borda => borda;

        public override void Expande()
        {
            var no = borda.Pop();
            Explorado.Add(no.Estado);

            
            var ligacoes = Problema.Caminhos(no.Estado).Where(l => !Explorado.Contains(l) && !borda.Any(b => b.Estado.Equals(l)));

            foreach (var ligacao in ligacoes)
            {
                var filho = new No<T>
                {
                    Pai = no,
                    Estado = ligacao
                };
                borda.Push(filho);

                if (ligacao.Equals(Problema.Destino))
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}

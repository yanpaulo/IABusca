using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmProfundidadeArvore<T> : BuscaEmArvoreBase<T>
    {
        private Stack<No<T>> borda = new Stack<No<T>>();
        private int? limite;

        public BuscaEmProfundidadeArvore(IProblema<T> problema, int? limite = null) 
            : base(problema)
        {
            this.limite = limite;
            borda.Push(Raiz);
        }

        public override IEnumerable<No<T>> Borda => borda;

        public override void Expande()
        {
            var no = borda.Pop();
            Explorado.Add(no.Estado);

            if (no.Estado.Equals(Problema.Destino))
            {
                Objetivo = no;
                return;
            }

            if (no.Profundidade >= limite)
            {
                return;
            }
            
            var caminhos = Problema.Caminhos(no.Estado).Where(c => !Explorado.Contains(c) && !borda.Any(b => b.Estado.Equals(c)));

            foreach (var caminho in caminhos)
            {
                var filho = new No<T>
                {
                    Pai = no,
                    Estado = caminho,
                    Profundidade = no.Profundidade + 1
                };
                borda.Push(filho);

                if (caminho.Equals(Problema.Destino))
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}

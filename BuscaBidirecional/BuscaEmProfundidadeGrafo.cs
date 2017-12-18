using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmProfundidadeGrafo<T> : IAlgoritmo<T>
    {
        private Stack<No<T>> borda = new Stack<No<T>>();

        public BuscaEmProfundidadeGrafo(IProblema<T> problema)
        {
            Raiz = new No<T>
            {
                Estado = problema.Origem
            };
            borda.Push(Raiz);
        }

        public No<T> Raiz { get; private set; }

        public IEnumerable<No<T>> Borda => borda;

        public IProblema<T> Problema { get; private set; }

        public No<T> Objetivo { get; private set; }

        public bool Falha => !borda.Any();

        public bool AtingiuObjetivo => Objetivo != null;

        public void Expande()
        {
            var no = borda.Pop();
            var ligacoes = Problema.Caminhos(no.Estado).Where(l => !borda.Any(b => b.Estado.Equals(l)));

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

        public string ImprimeListas()
        {
            var str = $@"
Borda:      [{ string.Join(", ", Borda.Select(b => b.Estado.ToString())) }]";

            return str;
        }
    }
}

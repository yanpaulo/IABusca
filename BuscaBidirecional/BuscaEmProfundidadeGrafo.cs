using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public class BuscaEmProfundidadeGrafo<T> : IAlgoritmo<T>
    {
        private Stack<No<T>> borda = new Stack<No<T>>();

        private int? limite;

        public BuscaEmProfundidadeGrafo(IProblema<T> problema, int? limite = null)
        {
            Problema = problema;
            this.limite = limite;
            Raiz = new No<T>
            {
                Estado = problema.Inicial
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
            if (no.Estado.Equals(Objetivo))
            {
                Objetivo = no;
                return;
            }
            if (no.Profundidade >= limite)
            {
                return;
            }

            var ligacoes = Problema.Acoes(no.Estado).Where(l => !borda.Any(b => b.Estado.Equals(l)));

            foreach (var ligacao in ligacoes)
            {
                var filho = new No<T>
                {
                    Pai = no,
                    Estado = ligacao,
                    Profundidade = no.Profundidade + 1
                };
                borda.Push(filho);

                if (Problema.TestaObjetivo(ligacao))
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

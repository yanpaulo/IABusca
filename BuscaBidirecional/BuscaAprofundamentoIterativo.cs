using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaBidirecional
{
    public class BuscaAprofundamentoIterativo<T> : IAlgoritmo<T>
    {
        private BuscaEmProfundidadeArvore<T> algoritmoProfundidade;
        
        public BuscaAprofundamentoIterativo(IProblema<T> problema)
        {
            Problema = problema;
            algoritmoProfundidade = new BuscaEmProfundidadeArvore<T>(problema, Limite);
        }

        public int Limite { get; private set; } = 0;

        public IProblema<T> Problema { get; private set; }

        public No<T> Objetivo =>
            algoritmoProfundidade.Objetivo;

        public IEnumerable<No<T>> Borda =>
            algoritmoProfundidade.Borda;

        public bool Falha =>
            false;

        public bool AtingiuObjetivo =>
            Objetivo != null;

        public void Expande()
        {
            if (algoritmoProfundidade.Falha)
            {
                algoritmoProfundidade = new BuscaEmProfundidadeArvore<T>(Problema, ++Limite);
            }
            else
            {
                algoritmoProfundidade.Expande();
            }
        }

        public string ImprimeListas() =>
            algoritmoProfundidade.ImprimeListas();
    }
}

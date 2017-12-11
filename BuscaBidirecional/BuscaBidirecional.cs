using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public enum TipoAlgoritmo
    {
        BuscaEmProfundidade,
        BuscaEmLargura
    }
    public class BuscaBidirecional : IAlgoritmo
    {
        private IAlgoritmo a1, a2;
        private No objetivo;

        public BuscaBidirecional(Problema problema, TipoAlgoritmo a1 = TipoAlgoritmo.BuscaEmLargura, TipoAlgoritmo a2 = TipoAlgoritmo.BuscaEmLargura)
        {
            Problema = problema;
            var inverso = new Problema
            {
                Mapa = problema.Mapa,
                Origem = problema.Destino,
                Destino = problema.Origem
            };

            this.a1 = a1 == TipoAlgoritmo.BuscaEmLargura ? 
                new BuscaEmLargura(problema) as IAlgoritmo : 
                new BuscaEmProfundidade(problema);

            this.a2 = a2 == TipoAlgoritmo.BuscaEmProfundidade ?
                new BuscaEmLargura(inverso) as IAlgoritmo :
                new BuscaEmProfundidade(inverso);


        }
        
        public IList<Local> Explorado => 
            new[] { a1.Explorado, a2.Explorado }
            .SelectMany(a => a)
            .ToList();

        public IEnumerable<No> Borda => 
            new[] { a1.Borda, a2.Borda }
            .SelectMany(a => a)
            .ToList();

        public No Objetivo => objetivo ?? a1.Objetivo ?? a2.Objetivo;

        public bool Falha => a1.Falha && a2.Falha;

        public bool AtingiuObjetivo => Objetivo != null;

        public Problema Problema { get; private set; }

        public void Expande()
        {
            if (AtingiuObjetivo || Falha)
            {
                throw new InvalidOperationException("Não é possível expandir no estado atual.");
            }

            if (!a1.Falha)
            {
                a1.Expande();
            }
            if (AtingiuObjetivo)
            {
                return;
            }
            BuscaObjetivo();
            if (AtingiuObjetivo)
            {
                return;
            }

            if (!a2.Falha)
            {
                a2.Expande();
            }
            if (AtingiuObjetivo)
            {
                return;
            }
            BuscaObjetivo();
        }

        public string ImprimeListas()
        {
            return $@"
a1:
{a1.ImprimeListas()}

a2:
{a2.ImprimeListas()}
";
        }

        private void BuscaObjetivo()
        {
            No no2 = null;
            var no1 = a1.Borda.FirstOrDefault(n1 => a2.Borda.Any(n2 => (no2 = n2).Local == n1.Local));

            //Se no1 != null, significa que ele "tocou" no2.
            if (no1 != null)
            {
                //Inverte a ordem do caminho, trocando os pais a partir de no2.
                var anterior = no1;
                var atual = no2.Pai;
                var proximo = atual.Pai;
                
                while (atual != null)
                {
                    proximo = atual.Pai;
                    atual.Pai = anterior;
                    anterior = atual;
                    atual = proximo;
                }

                objetivo = anterior;
            }
        }
    }
}

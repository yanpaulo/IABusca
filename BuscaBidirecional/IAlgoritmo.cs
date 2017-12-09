using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscaBidirecional
{
    public interface IAlgoritmo
    {
        Arvore Arvore { get; }
        
        IList<Local> Explorado { get; }

        IEnumerable<No> Borda { get; }
        
        No Objetivo { get; }

        bool Falha { get; }

        bool AtingiuObjetivo { get; }

        Problema Problema { get; }

        void Expande();
    }

    public static class AlgoritmoExtensoes
    {
        public static string ImprimeListas(this IAlgoritmo algoritmo)
        {
            var str = $@"
Explorado:  [{ string.Join(", ", algoritmo.Explorado.Select(l => l.Nome)) }]
Borda:      [{ string.Join(", ", algoritmo.Borda.Select(b => b.Local.Nome)) }]";

            return str;
        }

        public static string ImprimeCaminho(this IAlgoritmo algoritmo)
        {
            if (algoritmo.AtingiuObjetivo)
            {
                var lista = new List<Local>();
                var no = algoritmo.Objetivo;

                while (no.Pai != null)
                {
                    lista.Add(no.Local);
                    no = no.Pai;
                }
                lista.Add(no.Local);
                lista.Reverse();

                return string.Join(", ", lista.Select(l => l.Nome));
            }
            throw new InvalidOperationException("Só é possível imprimir o caminho se o objetivo for atingido.");
        }
    }
}
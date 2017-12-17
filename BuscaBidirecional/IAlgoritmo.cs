using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscaBidirecional
{
    public interface IAlgoritmo <T>
    {
        IList<T> Explorado { get; }

        IEnumerable<No<T>> Borda { get; }
        
        No<T> Objetivo { get; }

        bool Falha { get; }

        bool AtingiuObjetivo { get; }

        IProblema<T> Problema { get; }

        void Expande();
    }

    public static class AlgoritmoExtensoes
    {
        public static string ImprimeListas<T>(this IAlgoritmo<T> algoritmo)
        {
            var str = $@"
Explorado:  [{ string.Join(", ", algoritmo.Explorado.Select(l => l.ToString())) }]
Borda:      [{ string.Join(", ", algoritmo.Borda.Select(b => b.Estado.ToString())) }]";

            return str;
        }

        public static string ImprimeCaminho<T>(this IAlgoritmo<T> algoritmo)
        {
            if (algoritmo.AtingiuObjetivo)
            {
                var lista = new List<T>();
                var no = algoritmo.Objetivo;

                while (no.Pai != null)
                {
                    lista.Add(no.Estado);
                    no = no.Pai;
                }
                lista.Add(no.Estado);
                lista.Reverse();

                return string.Join(", ", lista.Select(l => l));
            }
            throw new InvalidOperationException("Só é possível imprimir o caminho se o objetivo for atingido.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscaBidirecional
{
    public interface IAlgoritmo <T>
    {
        IProblema<T> Problema { get; }

        No<T> Objetivo { get; }
        
        IEnumerable<No<T>> Borda { get; }
        
        bool Falha { get; }

        bool AtingiuObjetivo { get; }
        
        void Expande();

        string ImprimeListas();
    }

    public static class AlgoritmoExtensoes
    {

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
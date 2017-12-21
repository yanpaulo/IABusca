using System.Collections.Generic;

namespace IABusca
{
    public interface IProblema<T>
    {
        T Inicial { get; }
        
        IEnumerable<T> Acoes(T estado);

        bool TestaObjetivo(T estado);
    }
}
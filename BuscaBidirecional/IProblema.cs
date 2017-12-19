using System.Collections.Generic;

namespace BuscaBidirecional
{
    public interface IProblema<T>
    {
        T Inicial { get; set; }
        T Objetivo { get; set; }
        

        IEnumerable<T> Acoes(T estado);
    }
}
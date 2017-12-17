using System.Collections.Generic;

namespace BuscaBidirecional
{
    public interface IProblema<T>
    {
        T Origem { get; set; }
        T Destino { get; set; }
        

        IEnumerable<T> Caminhos(T estado);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaBidirecional.Aspirador
{
    public class ProblemaAspirador : IProblema<Posicao>
    {
        public ProblemaAspirador(Matriz matriz)
        {
            Matriz = matriz;
        }
        public Matriz Matriz { get; private set; }
        public Posicao Origem { get; set; }
        public Posicao Destino { get; set; }

        public IEnumerable<Posicao> Caminhos(Posicao estado)
        {
            //Esquerda
            if (estado.X-1 >= 0)
            {
                yield return Matriz[estado.X - 1, estado.Y];
            }
        }
    }
}

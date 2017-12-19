using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaBidirecional.Aspirador
{
    public class ProblemaAspirador : IProblema<Posicao>
    {
        public Matriz Matriz { get; set; }
        public Posicao Inicial { get; set; }
        public Posicao Objetivo { get; set; }

        public IEnumerable<Posicao> Acoes(Posicao estado)
        {
            //Esquerda
            if (estado.X > 0)
            {
                yield return Matriz[estado.X - 1, estado.Y];
            }
            if (estado.X < Matriz.Largura - 1)
            {
                yield return Matriz[estado.X + 1, estado.Y];
            }
            if (estado.Y > 0)
            {
                yield return Matriz[estado.X, estado.Y - 1];
            }
            if (estado.Y < Matriz.Largura - 1)
            {
                yield return Matriz[estado.X, estado.Y + 1];
            }
        }
    }
}

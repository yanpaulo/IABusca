using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaBidirecional.Aspirador
{
    public class ProblemaAspirador : IProblema<Ambiente>
    {
        public ProblemaAspirador(Ambiente ambiente)
        {
            Inicial = ambiente;
        }
        public Ambiente Inicial { get; private set; }
        
        public bool TestaObjetivo(Ambiente estado) =>
            estado.Posicoes.All(p => p == EstadoPosicao.Limpo);

        public IEnumerable<Ambiente> Acoes(Ambiente estado)
        {
            if (estado.Posicoes[estado.PosicaoAspirador] == EstadoPosicao.Sujo)
            {
                yield return new Ambiente(estado, true);
            }

            if (estado.PosicaoAspirador > 0)
            {
                yield return new Ambiente(estado) { PosicaoAspirador = estado.PosicaoAspirador - 1 };
            }

            if (estado.PosicaoAspirador < estado.Posicoes.Count() - 1)
            {
                yield return new Ambiente(estado) { PosicaoAspirador = estado.PosicaoAspirador + 1 };
            }
        }

    }
}

using BuscaBidirecional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscasCustomizadas
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapa = Mapa.FromFile();
            var problema = new ProblemaMapa
            {
                Mapa = mapa,
                Origem = mapa.Locais.Single(l => l.Nome == "Oradea"),
                Destino = mapa.Locais.Single(l => l.Nome == "Timisoara")
            };

            var algoritmo = new BuscaAprofundamentoIterativo<Local>(problema);
            while (!algoritmo.AtingiuObjetivo && !algoritmo.Falha)
            {
                Console.WriteLine(algoritmo.ImprimeListas());
                algoritmo.Expande();
            }

            if (algoritmo.Falha)
            {
                Console.WriteLine("Falha!");
            }
            else
            {
                Console.WriteLine(algoritmo.ImprimeCaminho());
            }

            Console.ReadKey();
        }
    }
}

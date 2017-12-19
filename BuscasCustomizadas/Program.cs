using BuscaBidirecional;
using BuscaBidirecional.Aspirador;
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
            var matriz = new Matriz(5, 5);

            var problemaMapa = new ProblemaMapa
            {
                Mapa = mapa,
                Inicial = mapa.Locais.Single(l => l.Nome == "Oradea"),
                Objetivo = mapa.Locais.Single(l => l.Nome == "Iasi")
            };

            var problemaAspirador = new ProblemaAspirador
            {
                Matriz = matriz,
                Inicial = matriz[0, 0],
                Objetivo = matriz[4, 4]
            };

            var algoritmo = new BuscaAprofundamentoIterativo<Local>(problemaMapa);

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

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.ReadKey(); 
            }
        }
    }
}

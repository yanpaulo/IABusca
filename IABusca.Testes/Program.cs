using IABusca;
using IABusca.Aspirador;
using IABusca.Mapas;
using System;

namespace BuscasCustomizadas
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapa = Mapa.FromFile();
            var ambiente = new Ambiente();

            //TEstado: Local
            var problemaMapa = new ProblemaMapa
            {
                Mapa = mapa,
                Inicial = mapa.Cidade(Cidade.Arad),
                Destino = mapa.Cidade(Cidade.Bucareste)
            };

            //TEstado: Ambiente
            var problemaAspirador = new ProblemaAspirador(ambiente);

            var algoritmo = new BuscaCustoUniforme<Local>(problemaMapa);

            while (!algoritmo.AtingiuObjetivo && !algoritmo.Falha)
            {
                Console.WriteLine(algoritmo.ImprimeListas());
                algoritmo.Expande();
            }

            Console.WriteLine(algoritmo.ImprimeListas());

            if (algoritmo.Falha)
            {
                Console.WriteLine("Falha!");
            }
            else
            {
                Console.WriteLine("Resultado: ");
                Console.WriteLine(algoritmo.ImprimeCaminho());
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.ReadKey(); 
            }
        }
    }

}

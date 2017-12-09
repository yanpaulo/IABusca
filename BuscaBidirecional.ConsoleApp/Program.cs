using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaBidirecional.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapa = Mapa.FromFile();
            var problema = new Problema
            {
                Mapa = mapa,
                Origem = mapa.Locais.First(l => l.Nome.ToLower().Contains(args[0].ToLower())),
                Destino = mapa.Locais.Last(l => l.Nome.ToLower().Contains(args[1].ToLower()))
            };
            var bp = new BuscaEmLargura(problema);

            while (!bp.AtingiuObjetivo && !bp.Falha)
            {
                bp.Expande();
                Console.WriteLine(bp.ImprimeListas());
            }

            Console.WriteLine(bp.ImprimeCaminho());

            Console.ReadKey();
        }
    }




}

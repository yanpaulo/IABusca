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

            //Teste de busca bidirecional.
            var mapa = Mapa.FromFile();
            var problema = new Problema
            {
                Mapa = mapa,
                Origem = mapa.Locais.First(l => l.Nome.ToLower().Contains("zerind")),
                Destino = mapa.Locais.Last(l => l.Nome.ToLower().Contains("fagaras"))
            };
            var bp = new BuscaBidirecional(problema);

            while (!bp.AtingiuObjetivo && !bp.Falha)
            {
                bp.Expande();
                Console.WriteLine(bp.ImprimeListas());
            }

            Console.WriteLine(bp.ImprimeCaminho());

            Console.ReadKey();
        }

        static void RodaAlgoritmo(string[] args)
        {
            string mapFile = null;
            string a1 = null, a2 = null;
            string orig = "oradea", dest = "fagaras";
            bool verbose = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    switch (args[i].Remove(0, 1))
                    {
                        case "a1":
                            a1 = args[++i];
                            break;
                        case "a2":
                            a2 = args[++i];
                            break;
                        case "m":
                            mapFile = args[++i];
                            break;
                        case "v":
                            verbose = true;
                            break;
                        default:
                            throw new ArgumentException("Parâmetro não reconhecido.");
                    }
                }
                else
                {
                    if (i < args.Length - 1 && !args[i + 1].StartsWith("-"))
                    {
                        orig = args[i];
                        dest = args[++i];
                    }
                    else
                    {
                        dest = args[i];
                    }
                }

            }

            var mapa = mapFile != null ? Mapa.FromFile(mapFile) : Mapa.FromFile();
            var origem = mapa.Locais.SingleOrDefault(l => l.Nome.ToLower().Contains(orig.ToLower()));
            var destino = mapa.Locais.SingleOrDefault(l => l.Nome.ToLower().Contains(orig.ToLower()));
            if (origem == null || destino == null)
            {
                throw new ArgumentException("Local especificado não existe.");
            }
            var problema = new Problema
            {
                Mapa = mapa,
                Origem = origem,
                Destino = destino
            };
            var algoritmo1 = a1 == "dfs" ? new BuscaEmLargura(problema) : new BuscaEmLargura(problema);
            var algoritmo2 = a2 == "dfs" ? new BuscaEmLargura(problema) : new BuscaEmLargura(problema);

            //Usa algoritmo1 e algoritmo2 para criar BuscaBidirecional.
        }
    }




}

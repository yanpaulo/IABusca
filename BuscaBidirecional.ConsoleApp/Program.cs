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
            RodaAlgoritmo(args);

            ////Teste de busca bidirecional.
            //var mapa = Mapa.FromFile();
            //var problema = new Problema
            //{
            //    Mapa = mapa,
            //    Origem = mapa.Locais.First(l => l.Nome.ToLower().Contains("zerind")),
            //    Destino = mapa.Locais.Last(l => l.Nome.ToLower().Contains("fagaras"))
            //};
            //var bp = new BuscaBidirecional(problema);

            //while (!bp.AtingiuObjetivo && !bp.Falha)
            //{
            //    bp.Expande();
            //    Console.WriteLine(bp.ImprimeListas());
            //}

            //Console.WriteLine(bp.ImprimeCaminho());

            //Console.ReadKey();
        }

        static void RodaAlgoritmo(string[] args)
        {
            string mapFile = null;
            TipoAlgoritmo? a = null;
            TipoAlgoritmo a1 = TipoAlgoritmo.BuscaEmLargura, a2 = TipoAlgoritmo.BuscaEmLargura;
            string orig = "oradea", dest = "fagaras";
            bool verbose = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    switch (args[i].Remove(0, 1))
                    {
                        case "a":
                            a = args[++i] == "dfs" ? TipoAlgoritmo.BuscaEmProfundidade : TipoAlgoritmo.BuscaEmLargura;
                            break;
                        case "a1":
                            a1 = args[++i] == "dfs" ? TipoAlgoritmo.BuscaEmProfundidade : TipoAlgoritmo.BuscaEmLargura;
                            break;
                        case "a2":
                            a2 = args[++i] == "dfs" ? TipoAlgoritmo.BuscaEmProfundidade : TipoAlgoritmo.BuscaEmLargura;
                            break;
                        case "m":
                            mapFile = args[++i];
                            break;
                        case "v":
                            verbose = true;
                            break;
                        default:
                            ImprimeAjuda();
                            return;
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
            var destino = mapa.Locais.SingleOrDefault(l => l.Nome.ToLower().Contains(dest.ToLower()));
            if (origem == null || destino == null)
            {
                throw new ArgumentException("Local especificado não existe.");
            }
            var problema = new ProblemaMapa
            {
                Mapa = mapa,
                Origem = origem,
                Destino = destino
            };
            
            //cria a busca apropriada e a executa
            if (a.HasValue)
            {
                var busca = a == TipoAlgoritmo.BuscaEmLargura ? 
                    new BuscaEmLargura<Local>(problema) as IAlgoritmo<Local> : 
                    new BuscaEmProfundidade<Local>(problema);

                while (!busca.AtingiuObjetivo)
                {
                    busca.Expande();
                    if (verbose)
                    {
                        Console.WriteLine(busca.ImprimeListas());
                    }
                }

                Console.WriteLine($"Rota: {busca.ImprimeCaminho()}");
            }
            else
            {
                var busca = new BuscaBidirecional(problema, a1, a2);

                while (!busca.AtingiuObjetivo)
                {
                    busca.Expande();
                    if (verbose)
                    {
                        Console.WriteLine(busca.ImprimeListas());
                    }
                }

                Console.WriteLine(busca.ImprimeCaminho());
            }

        }

        private static void ImprimeAjuda()
        {
            const string str = "Uso: \r\n" +
                "\tbusca.exe [-a1 algoritmo1] [-a2 algoritmo2] [-m mapfile] [-v] [origem] destino\n" +
                "\t\tExibe o caminho de origem a destino utilizando o algoritmo de Busca Bidirecional, opcionalmente especificando os sub-algoritmos a1 e a2.\n\n" +
                "\tbusca.exe [-a algoritmo] [-m mapfile] [-v] [origem] destino.\n" +
                "\t\tExibe o caminho de origem a destino utilizando o algoritmo opcionalmente especificado.\n\n" +
                "\tbusca.exe\n" +
                "\t\tExibe o caminho de Oradea a Fagaras utilizando Busca Bidirecional, com a1 e a2 sendo bfs.\n\n" +
                "\nOpções:\n" +
                "-a, -a1, -a2:      Especificadores de algoritmo. O valor pode ser bfs ou dfs.\n" +
                "-m:                Especificador de arquivo de mapa.\n" +
                "-v:                Habilita saída verbosa\n" +
                "origem, destino:   Nomes de locais\n" +
                "-?:                Exibe esta mensagem.\n";
            Console.WriteLine(str);
        }
    }




}

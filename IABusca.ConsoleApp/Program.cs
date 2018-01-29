using IABusca.Aspirador;
using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run(args);
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Console.ReadKey();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                ImprimeAjuda();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Run(string[] args)
        {
            var dict = new Dictionary<string, string>();
            var flat = new List<string>();
            bool verbose = false;

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("-"))
                {
                    switch (arg[1])
                    {
                        case 'v':
                            verbose = true;
                            break;
                        default:
                            dict[arg[1].ToString()] = args[++i];
                            break;
                    }
                }
                else
                {
                    flat.Add(arg);
                }
            }

            var p = dict.GetValueOrDefault("p", null);
            switch (p)
            {
                case "m":
                    {
                        var mapa = Mapa.FromFile();

                        string
                            origem = flat.Count > 0 ? flat[0].ToLower() : "zerind",
                            destino = flat.Count > 1 ? flat[1].ToLower() : "sibiu";

                        var problema = new ProblemaMapa
                        {
                            Mapa = mapa,
                            Inicial = mapa.Locais.First(l => l.Nome.ToLower().Contains(origem)),
                            Destino = mapa.Locais.First(l => l.Nome.ToLower().Contains(destino)),
                        };

                        InstanciaAlgoritmo(problema, dict)
                            .Executa(verbose);
                    }
                    break;
                case "a":
                    {
                        var problema = new ProblemaAspirador(new Ambiente(
                            (int)dict.GetIntOrDefault("t", 2),
                            (int)dict.GetIntOrDefault("i", 0))
                            );

                        InstanciaAlgoritmo(problema, dict)
                            .Executa(verbose);
                    }
                    break;
                case null:
                    throw new ArgumentException("Problema não especificado.");
                default:
                    throw new ArgumentException("Problema não reconhecido.");
            }
        }

        static IAlgoritmo<T> InstanciaAlgoritmo<T>(IProblema<T> problema, Dictionary<string, string> dict)
        {

            var a = dict.GetValueOrDefault("a", "bfs");

            switch (a)
            {
                case "bfs":
                    return new BuscaEmLargura<T>(problema);
                case "dfs":
                    return new BuscaEmProfundidade<T>(problema, dict.GetIntOrDefault("l", null));
                case "dfsv":
                    return new BuscaEmProfundidadeComVisitados<T>(problema, dict.GetIntOrDefault("l", null));
                case "idfs":
                    return new BuscaAprofundamentoIterativo<T>(problema);
                case "bb":
                    #region Busca Bidirecional
                    if (!(problema is ProblemaMapa))
                    {
                        throw new ArgumentException("Problema não suportado por Busca Bidirecional.");
                    }
                    TipoAlgoritmo
                        a1 = TipoAlgoritmo.BuscaEmLargura,
                        a2 = TipoAlgoritmo.BuscaEmLargura;

                    switch (dict.GetValueOrDefault("a1", "bfs"))
                    {
                        case "bfs":
                            a1 = TipoAlgoritmo.BuscaEmLargura;
                            break;
                        case "dfs":
                            a1 = TipoAlgoritmo.BuscaEmProfundidade;
                            break;
                        default:
                            throw new ArgumentException("Algoritmo inválido para Busca Bidirecional.");
                    }

                    switch (dict.GetValueOrDefault("a2", "bfs"))
                    {
                        case "bfs":
                            a2 = TipoAlgoritmo.BuscaEmLargura;
                            break;
                        case "dfs":
                            a2 = TipoAlgoritmo.BuscaEmProfundidade;
                            break;
                        default:
                            throw new ArgumentException("Algoritmo inválido para Busca Bidirecional.");
                    }

                    return new BuscaBidirecional(problema as ProblemaMapa, a1, a2) as IAlgoritmo<T>;
                #endregion
                case "bcu":
                    return new BuscaCustoUniforme<T>(problema);
                case "bgl":
                    {
                        if (problema is IProblemaHeuristica<T> p)
                        {
                            return new BuscaGulosa<T>(p);
                        }
                        throw new ArgumentException("Tipo de problema não suportado por Busca Gulosa.");
                    }
                case "ba*":
                    {
                        if (problema is IProblemaHeuristica<T> p)
                        {
                            return new BuscaAEsrela<T>(p);
                        }

                        throw new ArgumentException("Tipo de problema não suportado Busca A*.");
                    }
                default:
                    throw new ArgumentException("Algoritmo inválido.");
            }
        }

        private static void ImprimeAjuda()
        {
            const string str = "Uso: \r\n" +
                "busca.exe [-p problema opcoes] [-a algoritmo [opcoes]] [-v]\n" +
                "\tproblema:\r\n" +
                "\t\tm [origem] [destino] - Mapa da Romenia\r\n" +
                "\t\ta [-t tamanho_do_mundo(padrão=2)] [-i posicao_inicial(padrão=0)] - Aspirador\r\n" +
                "\r\n" +
                "\talgoritmo:\r\n" +
                "\t\tbfs - Busca em Largura (padrão)\r\n" +
                "\t\tdfs [-l limite(padrão=null)] - Busca em profundidade\r\n" +
                "\t\tdfsv [-l limite(padrão=null)] - Busca e profundidade com visitados \r\n" +
                "\t\tidfs - Busca com aprofundamento iterativo\r\n" +
                "\t\tbb [-a1 bfs|dfs (padrão bfs)] [-a2 bfs|dfs (padrão bfs)] - Busca Bidirecional\r\n" +
                "\t\tbcu - Busca de Custo Uniforme\r\n" +
                "\t\tbgl - Busca Gulosa\r\n" +
                "\t\tba* - Busca A*\r\n" +
                "\t -v:\r\n" +
                "\t\tMostra a saída detalhada da execução do algoritmo.\n\n" +
                "\tExemplos:\r\n" +
                "\t\tbusca.exe -p m zerind sibiu -a dfs -l 10 (Busca o caminho de zerind para sibiu utilizando dfs com limite de profundidade 10).\r\n" +
                "\t\tbusca.exe -p a -t 5 -i 2 -a idfs (Executa o aspirador em um universo de tamanho 5 e posição inicial 2, com algoritmo idfs.\r\n" +
                "\t\tbusca.exe -p a -a dfsv (Executa o aspirador em um universo de tamanho 5 e posição inicial 2 com o algoritmo dfsv sem limite de profundidade).\r\n" +
                "\t\tbusca.exe -p a -a dfsv -v (Mesmo exemplo anterior, mas com saída detalhada).\r\n"
                ;
            Console.WriteLine(str);
        }
    }

    public static class LocalExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        public static int? GetIntOrDefault<TKey>(this Dictionary<TKey, string> dictionary, TKey key, int? defaultValue)
        {
            return dictionary.TryGetValue(key, out string s) ?
                (int.TryParse(s, out int v) ? v : defaultValue) :
                defaultValue;
        }

        public static void Executa<T>(this IAlgoritmo<T> algoritmo, bool verbose)
        {
            while (!algoritmo.AtingiuObjetivo && !algoritmo.Falha)
            {
                if (verbose)
                {
                    Console.WriteLine(algoritmo.ImprimeListas());
                }
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
        }
    }


}

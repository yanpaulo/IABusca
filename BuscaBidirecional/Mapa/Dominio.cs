using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BuscaBidirecional
{
    public class Mapa
    {
        public List<Local> Locais { get; private set; } =
            new List<Local>();

        public static Mapa FromFile(string filename = "mapa.txt")
        {
            var mapa = new Mapa();
            var reader = new StreamReader(filename);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var nome = line.Substring(0, line.IndexOf(":"));
                var valores = line
                    .Remove(0, line.IndexOf(":") + 1)
                    .Split(new[] { ',' })
                    .Select(p => p.Trim());

                var local = GerOrCreateLocal(mapa, nome);
                
                local.Ligacoes.AddRange(valores.Select(v => GerOrCreateLocal(mapa, v)));

            }

            return mapa;
        }

        private static Local GerOrCreateLocal(Mapa mapa, string nome)
        {
            var local = mapa.Locais.SingleOrDefault(l => l.Nome == nome);
            if (local == null)
            {
                local = new Local { Nome = nome };
                mapa.Locais.Add(local);
            }

            return local;
        }
    }

    public class Local
    {
        public string Nome { get; set; }

        public List<Local> Ligacoes { get; private set; } =
            new List<Local>();

        public override string ToString() =>
            Nome;
    }
}

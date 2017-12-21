using IABusca;
using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuscasCustomizadas
{

    public enum Cidade
    {
        Oradea, Zerind, Arad, Timisoara, Lugoj, Mehadia,
        Drobeta, Sibiu, RimnicuVileea, Craiova, Fagaras,
        Pitesti, Bucareste, Giurgiu, Urziceni, Neamt, Iasi,
        Vaslui, Hirsova, Eforie
    }

    public static class MapaExtensions
    {
        static Dictionary<Cidade, string> cidadesDictionary = CidadesDictionary();

        public static Local Cidade(this Mapa mapa, Cidade cidade) =>
            mapa.Locais.Single(l => l.Nome == cidadesDictionary[cidade]);

        static Dictionary<Cidade, string> CidadesDictionary()
        {
            var regex = new Regex("([a-z])([A-Z])");
            var dictionary = new Dictionary<Cidade, string>();
            var cidades = Enum.GetValues(typeof(Cidade));
            foreach (var item in cidades)
            {
                var nome = Enum.GetName(typeof(Cidade), item);
                dictionary.Add((Cidade)item, regex.Replace(nome, "$1 $2"));
            }

            return dictionary;
        }
    }
}

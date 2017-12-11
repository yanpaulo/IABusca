using System;
using System.Collections.Generic;

namespace BuscaBidirecional
{
    public class Problema
    {
        public Mapa Mapa { get; set; }

        public Local Origem { get; set; }

        public Local Destino { get; set; }
    }

    public class Arvore
    {
        public No Raiz { get; set; }
    }

    public class No
    {
        public No Pai { get; set; }

        public Local Local { get; set; }
    }
}

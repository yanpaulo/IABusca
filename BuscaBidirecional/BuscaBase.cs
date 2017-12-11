using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public abstract class BuscaBase : IAlgoritmo
    {

        public BuscaBase(Problema problema)
        {
            Problema = problema;

            Arvore = new Arvore
            {
                Raiz = new No
                {
                    Local = Problema.Origem
                }
            };
        }

        public Problema Problema { get; private set; }

        public Arvore Arvore { get; private set; }

        public No Objetivo { get; protected set; }

        public IList<Local> Explorado { get; private set; } = new List<Local>();

        public abstract IEnumerable<No> Borda { get; }
        
        public bool AtingiuObjetivo => Objetivo != null;

        public bool Falha => !Borda.Any();

        public abstract void Expande();
    }
}

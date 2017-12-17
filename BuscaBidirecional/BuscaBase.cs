﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BuscaBidirecional
{
    public abstract class BuscaBase<T> : IAlgoritmo<T>
    {

        public BuscaBase(IProblema<T> problema)
        {
            Problema = problema;
            Raiz = new No<T>
            {
                Estado = Problema.Origem
            };
        }

        public IProblema<T> Problema { get; private set; }

        public No<T> Raiz { get; protected set; }

        public No<T> Objetivo { get; protected set; }

        public IList<T> Explorado { get; private set; } = new List<T>();

        public abstract IEnumerable<No<T>> Borda { get; }

        public bool AtingiuObjetivo => Objetivo != null;

        public bool Falha => !Borda.Any();

        public abstract void Expande();
    }
}

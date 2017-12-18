using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaBidirecional.Aspirador
{
    public class Matriz
    {
        private Posicao[,] matriz;
        public Matriz(int largura, int altura)
        {
            matriz = new Posicao[largura, altura];
            for (int i = 0; i < largura; i++)
            {
                for (int j = 0; j < altura; j++)
                {
                    matriz[i, j] = new Posicao(largura, altura);
                }
            }
        }

        public int Largura => matriz.GetLength(0);

        public int Altura => matriz.GetLength(1);

        public Posicao this[int x, int y] => matriz[x, y];
        
    }
}

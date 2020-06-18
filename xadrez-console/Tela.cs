﻿using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        //Método para imprimir meu tabuleiro no console(matriz)
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++) 
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++) 
                {
                    if(tab.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else 
                    {
                        Tela.ImprimirPeca(tab.Peca(i, j));
                        Console.Write(" ");
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        //Método para ler a posição em que o usuário vai colocar a peça
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        //Método para imprimir peças da cor amarela e cor normal
        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branca) 
            {
                Console.Write(peca);
            }
            else 
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}

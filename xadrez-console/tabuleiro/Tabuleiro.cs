
namespace tabuleiro
{
    class Tabuleiro
    {

        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro()
        {

        }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        //Método para definir o tamanho da minha matriz de peças
        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        //Método para colocar a peça no tabuleiro definindo sua posição
        public void ColocarPeca(Peca p, Posicao pos)
        {
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;

        }
    }
}

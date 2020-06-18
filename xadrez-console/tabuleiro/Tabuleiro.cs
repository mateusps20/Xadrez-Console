
using tabuleiro;

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

        //Obter uma peça por posição
        public Peca Peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        //Testar se existe alguma peça em uma determinada posição
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        //Método para colocar a peça no tabuleiro definindo sua posição
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos)) 
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;

        }

        //Método para retirar uma peça e retornando uma peça caso precise reutilizar ela
        public Peca RetirarPeca(Posicao pos)
        {
            if(Peca(pos) == null) 
            {
                return null;
            }
            Peca aux = Peca(pos);
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        //Método para validar se minha posição é válida ou não
        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas) 
            {
                return false;
            }
            return true;
        }

        //Método para lançar exceções caso a posição informada não seja válida
        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos)) 
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}

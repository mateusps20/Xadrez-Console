
namespace tabuleiro
{
    abstract class Peca
    {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca()
        {

        }

        //Posicao = null pois quando cria uma peça ela ainda não tem posição, e quem define sua posição é a class Tabuleiro
        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Tab = tab;
            Cor = cor;
            QteMovimentos = 0;
        }

        //Método para incrementar a quantidade de movimentos ao mexer uma peça.
        public void IncrementarQteMovimentos()
        {
            QteMovimentos++;
        }

        public void DecrementarQteMovimentos()
        {
            QteMovimentos--;
        }

        //Método para verificar se existe movimentos possiveis para a peça a ser jogada
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i = 0; i < Tab.Linhas; i++)
            {
                for(int j = 0; j < Tab.Colunas; j++) 
                {
                    if(mat[i, j]) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Método para validar a movimentação da peça
        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}

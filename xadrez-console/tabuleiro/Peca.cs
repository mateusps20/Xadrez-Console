
namespace tabuleiro
{
    class Peca
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
    }
}

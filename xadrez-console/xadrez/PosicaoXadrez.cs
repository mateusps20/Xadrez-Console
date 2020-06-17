
using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {

        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez()
        {

        }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        //Converte a posição do xadrez na posição da matriz de peças
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}

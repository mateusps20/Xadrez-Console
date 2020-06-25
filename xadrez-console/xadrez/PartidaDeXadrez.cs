using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        /*Método para executar um movimento de peça, que vai retirar minha peça p da origem,incrementar qte movimentos,
         retirar a peça da posição destino(caso tenha) e guardar em uma variavel Peca, e por final colocar a minha peça p
        na posição destino*/
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if(pecaCapturada != null) 
            {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        //Método para desfazer o movimento da peça para utilizar no método RealizaJogada caso o usuário se coloque em xeque
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada != null) 
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }

        /*Método para realizar a jogada no tabuleiro incrementando o turno e mudando a vez do jogador,
        e também verificando se o usuário se colocou em xeque*/
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual)) 
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            
            if(EstaEmXeque(Adversaria(JogadorAtual))) 
            {
                Xeque = true;
            }

            else 
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }

            else 
            {
                Turno++;
                MudaJogador();
            }

            
        }

        //Método para validar a posição de origem e lançar erros caso ocorra algumas das verificações abaixo
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null ) 
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor) 
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis()) 
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        //Método para validar a posição de destino e lançar erros caso ocorra algumas das verificações abaixo
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino)) 
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca) 
            {
                JogadorAtual = Cor.Preta;
            }
            else 
            {
                JogadorAtual = Cor.Branca;
            }
        }

        //Método para retornar as peças capturadas da cor que eu informar
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas) 
            {
                if(x.Cor == cor) 
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        //Método para retornar as peças ainda em jogo retirando as peças que foram capturadas da cor informada
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas) 
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        //Definir cor adversária
        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else 
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor)) 
            {
                if(x is Rei) 
                {
                    return x;
                }
            }
            return null;
        }

        //Método para verificaçaõ do Rei estar em xeque
        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if(R == null) 
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach(Peca x in PecasEmJogo(Adversaria(cor))) 
            {
                bool[,] mat = x.MovimentosPossiveis();
                if(mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        /*Método para testar xequemate
         Faço o movimento, testo se ainda está em xeque e desfaço o movimento*/
        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor)) 
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor)) 
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < Tab.Linhas; i++)
                {
                    for(int j = 0; j < Tab.Colunas; j++) 
                    {
                        if(mat[i, j]) 
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque) 
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        //Método para adicionar nova peça no meu conjunto hashset
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(Tab, Cor.Branca));


            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Preta));


        }
    }
}

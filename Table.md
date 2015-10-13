> # Classe Table #

Namespace: Xadrez

> ## Inner class ##
> > [TableSquare](TableSquare.md) - Representa as casas do tabuleiro.


> ## Membros ##

> `TABLE_SIZE` - tamanho do lado do tabuleiro = 8.

> `m_selfImage` - Imagem do tabuleiro a ser exibida na interface.

> `m_table` - Matriz quadrada de tamanho `TABLE_SIZE`, de casas (`TableSquare`).

> `m_vecDeadBlackPieces` - vetor com as peças pretas mortas.

> `m_vecDeadWhitePieces` - vetor com as peças brancas mortas.



> ## Métodos ##

> `Table()` - Construtor da classe.

> `initTable(bool bBlack)` - Criação e distribuição das peças no tabuleiro. Cada peça fica em uma casa específica (`TableSquare`). O parâmetro bBlack informa se as peças pretas ficarão embaixo.

> `getPossibleMovement(Point pt)` - Retorna os possíveis movimentos da peça que está na posição `pt`. O parâmetro `pt` informa uma posição do tabuleiro (0 até `TABLE_SIZE-1`). Método público, pode ser usado pela interface para iluminar as casas disponíveis para o usuário.

> `getPiece(Point pt)` - Retorna a peça que está na posição `pt`. O parâmetro `pt` informa uma posição do tabuleiro (0 até `TABLE_SIZE-1`). Método privado, não há necessidade de outras classes terem acesso a uma peça.

> `move(Play play)` - Move uma peça do tabuleiro de acordo com a jogada `play`. O parâmetro play representa a jogada a ser feita no tabuleiro. Caso a jogada seja ilegal, retorna `false`. Método público, pode ser usado pela interface para realizar a jogada do usuário.

> `isPtInTable(Point pt)` - Retorna `true` se o ponto `pt` é uma posição válida do tabuleiro, e `false` se for uma posição inválida. Método privado, só a classe table faz esse tipo de verificação no tabuleiro.

> `isInCheckMate(bool bBlack)` - Retorna `true` se o jogador `bBlack` está em check mate. O parâmetro `bBlack` representa o jogador a ser verificado, se for `true` é o jogador das peças pretas, senão é o das peças brancas. Método público, a classe Minimax pode usar para terminar a busca de jogadas, e a interface pode usar para exibir aviso de fim de jogo.


> `setSelfImage()` - Associa a imagem do tabuleiro.

> `getSelfImage()` - Retorna a imagem do tabuleiro.



## Operadores ##

> `static Table operator +(Table table, Play play)` - Realiza uma jogada no tabuleiro `table`. Retorna uma novo tabuleiro. Semelhante ao método `move`, porém cria uma cópia do tabuleiro para retorná-lo. Método público, será usado pelo algoritmo minimax.

> `static Play operator -(Table newTable, Table oldTable)` - Compara dois tabuleiros e retorna a jogada que os diferencia. Método público, será usado pelo algoritmo minimax.
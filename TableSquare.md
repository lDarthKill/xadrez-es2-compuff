> # Classe `TableSquare` #

> Classe que representa uma casa do tabuleiro. É uma inner class da classe [Table](Table.md).


> ## Membros ##

> `Piece m_piece` - Aponta para a peça que está neste `TableSquare`. Pode ser `null`.

> `Rectangle m_rBoundingBox` - Retangulo com as coordenadas do `TableSquare` na interface.


> ## Métodos ##

> `TableSquare( )` - Construtor padrão.

> `TableSquare( Piece _piece, Rectangle _boundingBox )` - Construtor que recebe a peça que está neste `TableSquare` e o retangulo do `TableSquare` na interface.

> `Piece` - Accessor. Seta/Retorna a peça que está no `TableSquare` atualmente.

> `BoundingBox` - Accessor. Seta/Retorna o retângulo do `TableSquare` na interface.

> `hasPiece()` - Retorna `true` se existe uma peça neste `TableSquare`.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xadrez;

namespace Xadrez
{
    public class Table : ICloneable
    {
        // Definitions
		const bool BLACK = true;
		const bool WHITE = false;
		const int TABLE_SIZE = 8;


		public class TableSquare : ICloneable // Squares of a table. Can hold a piece (or not).
        {
            // Private members
			private Piece m_piece;
			private Rectangle m_rBoundingBox;

			
			// Constructors
			public
			TableSquare( )
			{

			}

			public
			TableSquare( Piece _piece, Rectangle _boundingBox )
			{
				m_piece = _piece;
				m_rBoundingBox = _boundingBox;
			}


            // Access methods
			public Piece Piece
			{
				get
				{
					return this.m_piece;
				}

				set
				{
					this.m_piece = value;
				}
			}

			public Rectangle BoundingBox
			{
				get
				{
					return this.m_rBoundingBox;
				}

				set
				{
					this.m_rBoundingBox = value;
				}
			}

			public bool hasPiece( )
			{
			    return (m_piece != null);
            }

            #region ICloneable Members
            public object Clone()
            {
				TableSquare tableSquare = ( TableSquare )this.MemberwiseClone( );
				tableSquare.m_piece = null;

				return ( object )tableSquare;
            }
            #endregion
        }

        static private Texture2D  m_selfImage;
        private TableSquare[ , ]  m_table;
		private List< Piece >     m_vecBlackPieces;
		private List< Piece >     m_vecDeadBlackPieces;
		private List< Piece >     m_vecWhitePieces;
		private List< Piece >     m_vecDeadWhitePieces;

		private bool			  m_bCheck;
		private bool			  m_bCheckMate;

		private bool			  m_bVerifyCheck;

		public Table( )
		{
			m_table = new TableSquare[ TABLE_SIZE, TABLE_SIZE ];
			m_vecBlackPieces = new List<Piece>( );
			m_vecDeadBlackPieces = new List<Piece>( );
			m_vecWhitePieces = new List<Piece>( );
            m_vecDeadWhitePieces = new List<Piece>( );

			m_bCheck = false;
			m_bCheckMate = false;
			m_bVerifyCheck = true;

			initTable( );
        }

        private void
		initTable( )
        {
			int nRow = 0;
			int nCol = 0;
            Piece piece;
			Rectangle boundingBox;

            // Team pieces.
            // First row - Black - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
            m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
            ++nCol;
			piece = new Knight( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Queen( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new King( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Rook( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecBlackPieces.Add( piece );

            // Second row - Black - Pawns
            ++nRow;
            for (nCol = 0; nCol < TABLE_SIZE; ++nCol)
            {
				piece = new Pawn( nRow, nCol, BLACK, this );
				boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
				m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
				m_vecBlackPieces.Add( piece );
			}

			// Third to sixth rows - Empty
			piece = null;
			for( nRow = 2; nRow < 6; nRow++ )
			{
				for( nCol = 0; nCol < TABLE_SIZE; ++nCol )
				{
					boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
					m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
				}
			}

			// Seventh row - White - Pawns
			for( nCol = 0; nCol < TABLE_SIZE; ++nCol )
			{
				piece = new Pawn( nRow, nCol, WHITE, this );
				boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
				m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
				m_vecWhitePieces.Add( piece );
			}

			nRow++;
			nCol = 0;
			//bBlack = !bBlack;

			// Last row - White - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Queen( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new King( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Rook( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			m_vecWhitePieces.Add( piece );
        }

		public TableSquare getTableSquare( int nRow, int nCol )
		{
			return m_table[ nRow, nCol ];
		}

		private Piece getPiece( Point pt )
        {
            if (!isPtInTable(pt))
                return null;

            return m_table[pt.X, pt.Y].Piece;
        }

        public bool move( Play _play )
        {
            if (!isPtInTable(_play.oldPosition))
                return false;

            if (!isPtInTable(_play.newPosition))
                return false;

            TableSquare oldSquare = m_table[_play.oldPosition.X, _play.oldPosition.Y];
            TableSquare newSquare = m_table[_play.newPosition.X, _play.newPosition.Y];
            Piece   pieceEaten = null;

			if( oldSquare.Piece == null )
			{
				// There's no piece moving in this play
				return false;
			}

			// Now we double check if this play is possible
			//if( !oldSquare.Piece.vetPossibleMovements.Contains( _play.newPosition ) )
			//{
			//    // Trying to make a move to an invalid position
			//    return false;
			//}

			if( newSquare.Piece != null )
			{
				// Check if a piece is eaten
				pieceEaten = newSquare.Piece;

				if( oldSquare.Piece.isBlack == pieceEaten.isBlack )
				{
					// Trying to eat a piece of your own team
					return false;
				}
			}
			
            // Move the piece.
            newSquare.Piece = oldSquare.Piece;
            oldSquare.Piece = null;

			newSquare.Piece.Position = _play.newPosition;

			// Hold the eaten piece for later count or something.
			if( pieceEaten != null )
			{
				pieceEaten.SetPosition( -1, -1 );

				if( pieceEaten.isBlack )
				{
					m_vecBlackPieces.Remove( pieceEaten );
					m_vecDeadBlackPieces.Add( pieceEaten );
				}
				else
				{
					m_vecWhitePieces.Remove( pieceEaten );
					m_vecDeadWhitePieces.Add( pieceEaten );
				}

				if( pieceEaten is King )
					m_bCheckMate = true;
			}

			// Calculate the next possible movements
			if( newSquare.Piece.isBlack )
			{
				//calculateBlackPiecesPosition( false );
				calculateWhitePiecesPosition( m_bVerifyCheck );
			}
			else
			{
				//calculateWhitePiecesPosition( false );
				calculateBlackPiecesPosition( m_bVerifyCheck );
			}

			// Verify if the game is over (checkmate)
			if( m_bVerifyCheck )
			{
				m_bCheckMate = isInCheckMate( !newSquare.Piece.isBlack );
			}

		    return true;
		}

        public bool isPtInTable(Point pt)
        {
            if (pt.X < 0 || pt.X >= TABLE_SIZE)
                return false;
            if (pt.Y < 0 || pt.Y >= TABLE_SIZE)
                return false;

            return true;
        }

		public bool isInCheck( bool _bBlack )
		{
			List<Piece> vecPiecesEnemy;		// Will get the enemy's king position from this list <- to verify if it is in check
			List<Piece> vecPiecesCurrent;	// Will verify if any of the current player's pieces threatens the enemy king
			if( _bBlack )
			{
				vecPiecesEnemy = m_vecBlackPieces;     // <- Verify if the Black king is in check
				vecPiecesCurrent = m_vecWhitePieces;
			}
			else
			{
				vecPiecesEnemy = m_vecWhitePieces;     // <- Verify if the White king is in check
				vecPiecesCurrent = m_vecBlackPieces;
			}
	
			// Retrieves the enemy's king position
			Point ptEnemyKing = new Point( );
			for( int i = 0; i < vecPiecesEnemy.Count; i++ )
			{
				Piece currentPiece = vecPiecesEnemy[ i ];
				if( currentPiece is King )
				{
					ptEnemyKing = currentPiece.Position;
					break;
				}
			}

			// Now goes through every piece of the current player to see if any of them threatens the enemy king
			for( int i = 0; i < vecPiecesCurrent.Count; i++ )
			{
				if( vecPiecesCurrent[ i ].vetPossibleMovements.Contains( ptEnemyKing ) )
				{
					return true;
				}
			}

			return false;
		}

		private bool isInCheckMate( bool _bBlack )
        {
			List<Piece> vecPieces;		// Verify if the enemy (_bBlack) can move any piece to avoid the checkmate
			if( _bBlack )
			{
				vecPieces = m_vecBlackPieces;
			}
			else
			{
				vecPieces = m_vecWhitePieces;
			}

			// If the enemy player can't move any piece (to defend his king), then he's in checkmate
			for( int i = 0; i < vecPieces.Count; i++ )
			{
				Piece currentPiece = vecPieces[ i ];
				
				List<Point> possibleMoves = currentPiece.vetPossibleMovements;
				if( possibleMoves.Count > 0 )
				{
					return false;
				}
			}

			return true;
		}

		public void calculateBlackPiecesPosition( bool _bVerifyCheck )
		{
			for( int i = 0; i < m_vecBlackPieces.Count; i++ )
			{
				m_vecBlackPieces[ i ].calculatePossiblePositions( _bVerifyCheck );
			}
		}

		public void calculateWhitePiecesPosition( bool _bVerifyCheck )
		{
			for( int i = 0; i < m_vecWhitePieces.Count; i++ )
			{
				m_vecWhitePieces[ i ].calculatePossiblePositions( _bVerifyCheck );
			}
		}

		static public Table operator +( Table table, Play play )
		{
		    // return a copy of the table modified with the play.
            Table newTable = (Table)table.Clone();
            newTable.move(play);
            return newTable;
		}

		static public Play operator -(Table newTable, Table oldTable)
		{
		    // return the play that represents the difference between these 2 tables.

            TableSquare tableSquareNew1 = null,
                        tableSquareOld1 = null,
                        tableSquareNew2 = null,
                        tableSquareOld2 = null;

            Point       pos1 = new Point(),
                        pos2 = new Point();

            bool bFinish = false;
            for (int nRow = 0; nRow < TABLE_SIZE; ++nRow)
            {
                for (int nCol = 0; nCol < TABLE_SIZE; ++nCol)
                {
                    TableSquare tableSquareTempNew = newTable.m_table[nRow, nCol],
                                tableSquareTempOld = oldTable.m_table[nRow, nCol];

                    
                    if ((tableSquareTempNew.Piece == null) && (tableSquareTempOld.Piece == null))
                        continue;
                    if ((tableSquareTempNew.Piece != null) && (tableSquareTempOld.Piece != null)
                        && tableSquareTempNew.Piece.Equals(tableSquareTempOld.Piece))
                        continue;

                    // The piece changed between these two tables.
                    //  Save the positions.
                    if ((tableSquareNew1 == null) && (tableSquareOld1 == null))
                    {
                        tableSquareNew1 = tableSquareTempNew;
                        tableSquareOld1 = tableSquareTempOld;
                        pos1.X = nRow;
                        pos1.Y = nCol;
                    }
                    else
                    {
                        tableSquareNew2 = tableSquareTempNew;
                        tableSquareOld2 = tableSquareTempOld;
                        pos2.X = nRow;
                        pos2.Y = nCol;

                        bFinish = true;
                        break;
                    }
                }

                if (bFinish)
                    break;
            }

            if (!bFinish)
                return null;

            Play play = new Play();
            if ((tableSquareOld1.Piece != null) && (tableSquareNew1.Piece == null))
            {
                play.oldPosition = pos1;
                play.newPosition = pos2;
            }
            else if ((tableSquareOld2.Piece != null) && (tableSquareNew2.Piece == null))
            {
                play.oldPosition = pos2;
                play.newPosition = pos1;
            }

            return play;
		}

        public
		int
		TableSize
		{
			get
			{
				return TABLE_SIZE;
			}
		}

		public
		bool
		Check
		{
			get
			{
				return m_bCheck;
			}

			set
			{
				m_bCheck = value;
			}
		}

		public
		bool
		CheckMate
		{
			get
			{
				return m_bCheckMate;
			}

			//set
			//{
			//    m_bCheckMate = value;
			//}
		}

		public bool VerifyCheck
		{
			get
			{
				return m_bVerifyCheck;
			}

			set
			{
				m_bVerifyCheck = value;
			}
		}

		static public 
        void SetSelfImage(Texture2D _texture)
        {
            m_selfImage = _texture;
        }

        static public
        Texture2D GetSelfImage()
        {
            return m_selfImage;
        }

        public
        List<Piece> GetListDeadBlackPieces()
        {
            return m_vecDeadBlackPieces;
        }

        public
        List<Piece> GetListDeadWhitePieces()
        {
            return m_vecDeadWhitePieces;
        }

		public
		void
		resetTable( )
		{
			m_vecBlackPieces = new List<Piece>( );
			m_vecDeadBlackPieces = new List<Piece>( );
			m_vecWhitePieces = new List<Piece>( );
			m_vecDeadWhitePieces = new List<Piece>( );

			m_bCheck = false;
			m_bCheckMate = false;
			m_bVerifyCheck = true;

			int nRow = 0;
			int nCol = 0;
			Piece piece;

			// Team pieces.
			// First row - Black - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Queen( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new King( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );
			++nCol;
			piece = new Rook( nRow, nCol, BLACK, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecBlackPieces.Add( piece );

			// Second row - Black - Pawns
			++nRow;
			for( nCol = 0; nCol < TABLE_SIZE; ++nCol )
			{
				piece = new Pawn( nRow, nCol, BLACK, this );
				m_table[ nRow, nCol ].Piece = piece;
				m_vecBlackPieces.Add( piece );
			}

			// Third to sixth rows - Empty
			piece = null;
			for( nRow = 2; nRow < 6; nRow++ )
			{
				for( nCol = 0; nCol < TABLE_SIZE; ++nCol )
				{
					m_table[ nRow, nCol ].Piece = null;
				}
			}

			// Seventh row - White - Pawns
			for( nCol = 0; nCol < TABLE_SIZE; ++nCol )
			{
				piece = new Pawn( nRow, nCol, WHITE, this );
				m_table[ nRow, nCol ].Piece = piece;
				m_vecWhitePieces.Add( piece );
			}

			nRow++;
			nCol = 0;

			// Last row - White - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Queen( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new King( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Bishop( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Knight( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
			++nCol;
			piece = new Rook( nRow, nCol, WHITE, this );
			m_table[ nRow, nCol ].Piece = piece;
			m_vecWhitePieces.Add( piece );
		}


        #region ICloneable Members

        public object Clone()
        {
			//Table table = ( Table )this.MemberwiseClone( );
			//table.VerifyCheck = this.VerifyCheck;
			Table table = ( Table )this.MemberwiseClone( );

			table.m_table = new TableSquare[ TABLE_SIZE, TABLE_SIZE ];
			for( int nRow = 0; nRow < TABLE_SIZE; ++nRow )
            {
                for (int nCol = 0; nCol < TABLE_SIZE; ++nCol)
                {
                    table.m_table[nRow, nCol] = ( TableSquare )this.m_table[ nRow, nCol ].Clone( );
                }
            }

			table.m_vecBlackPieces = new List<Piece>( );
			for( int i = 0; i < this.m_vecBlackPieces.Count; i++ )
			{
				Piece newPiece = ( Piece )this.m_vecBlackPieces[ i ].Clone( );
				newPiece.ParentTable = table;
				table.m_vecBlackPieces.Add( newPiece );
				table.m_table[ newPiece.Position.X, newPiece.Position.Y ].Piece = newPiece;
			}

			table.m_vecWhitePieces = new List<Piece>( );
			for( int i = 0; i < this.m_vecWhitePieces.Count; i++ )
			{
				Piece newPiece = ( Piece )this.m_vecWhitePieces[ i ].Clone( );
				newPiece.ParentTable = table;
				table.m_vecWhitePieces.Add( newPiece );
				table.m_table[ newPiece.Position.X, newPiece.Position.Y ].Piece = newPiece;
			}

			table.m_vecDeadBlackPieces = new List<Piece>( );
			for( int i = 0; i < this.m_vecDeadBlackPieces.Count; i++ )
			{
				Piece newPiece = ( Piece )this.m_vecDeadBlackPieces[ i ].Clone( );
				newPiece.ParentTable = table;
				table.m_vecDeadBlackPieces.Add( newPiece );
			}

			table.m_vecDeadWhitePieces = new List<Piece>( );
			for( int i = 0; i < this.m_vecDeadWhitePieces.Count; i++ )
			{
				Piece newPiece = ( Piece )this.m_vecDeadWhitePieces[ i ].Clone( );
				newPiece.ParentTable = table;
				table.m_vecDeadWhitePieces.Add( newPiece );
			}

			return ( object )table;
        }

        #endregion
    }
}
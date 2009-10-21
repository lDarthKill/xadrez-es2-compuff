using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xadrez;

namespace Xadrez
{
    public class Table
    {
        // Definitions
		const bool BLACK = true;
		const bool WHITE = false;
		const int TABLE_SIZE = 8;

	
		public class TableSquare // Squares of a table. Can hold a piece (or not).
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
        }

        static private Texture2D  m_selfImage;
        private TableSquare[ , ]  m_table;
		private List< Piece >     m_vecDeadBlackPieces;
        private List< Piece >     m_vecDeadWhitePieces;

		public Table( )
		{
			m_table = new TableSquare[ TABLE_SIZE, TABLE_SIZE ];
            m_vecDeadBlackPieces = new List<Piece>( );
            m_vecDeadWhitePieces = new List<Piece>( );

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
            m_table[nRow, nCol] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Queen( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new King( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Rook( nRow, nCol, BLACK, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );

            // Second row - Black - Pawns
            ++nRow;
            for (nCol = 0; nCol < TABLE_SIZE; ++nCol)
            {
				piece = new Pawn( nRow, nCol, BLACK, this );
				boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
				m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
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
			}

			nRow++;
			nCol = 0;
			//bBlack = !bBlack;

			// Last row - White - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Queen( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new King( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Rook( nRow, nCol, WHITE, this );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );

        }

		public TableSquare getTableSquare( int posX, int posY )
		{
			return m_table[ posX, posY ];
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
			List<Point> possibleMoves = oldSquare.Piece.getPossiblePositions( );
			if( !possibleMoves.Contains( _play.newPosition ) )
			{
				// Trying to make a move to an invalid position
				return false;
			}

			if( newSquare.Piece != null )
			{
				// Check if a piece is eaten
				pieceEaten = newSquare.Piece;

				if( oldSquare.Piece.Black == pieceEaten.Black )
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

				if( pieceEaten.Black )
					m_vecDeadBlackPieces.Add( pieceEaten );
				else
					m_vecDeadWhitePieces.Add( pieceEaten );
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

        public bool IsInCheckMate(bool bBlack)
        {
            throw new NotImplementedException();
        }

		static public Table operator +(Table table, Play play)
		{
		    // 2do - returns a copy of the old table modified with the play.
		    throw new NotImplementedException();
		}

		static public Play operator -(Table newTable, Table oldTable)
		{
		    // 2do - return the play that represents the difference between these 2 tables.
		    throw new NotImplementedException();
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
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xadrez;

namespace Xadrez
{
    class Table
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

			//public bool hasPiece( )
			//{
			//    return (m_piece != null);
			//}
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

		//// @//param: bBlack - true if black pieces are at the top.
        private void
		initTable( )
        {
			int nRow = 0;
			int nCol = 0;
            Piece piece;
			Rectangle boundingBox;

            // Team pieces.
            // First row - Black - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
            m_table[nRow, nCol] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Queen( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new King( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Rook( nRow, nCol, BLACK );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );

            // Second row - Black - Pawns
            ++nRow;
            for (nCol = 0; nCol < TABLE_SIZE; ++nCol)
            {
				piece = new Pawn( nRow, nCol, BLACK );
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
				piece = new Pawn( nRow, nCol, WHITE );
				boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
				m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
			}

			nRow++;
			nCol = 0;
			//bBlack = !bBlack;

			// Last row - White - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
			piece = new Rook( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Queen( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new King( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Bishop( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Knight( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );
            ++nCol;
			piece = new Rook( nRow, nCol, WHITE );
			boundingBox = new Rectangle( ( 36 + ( nCol * 71 ) ), ( 35 + ( nRow * 71 ) ), 71, 71 );
			m_table[ nRow, nCol ] = new TableSquare( piece, boundingBox );

        }

		//public void getPossibleMovement(Point pt)
		//{
		//    Piece piece = getPiece(pt);
		//    if (piece == null)
		//        return;

		//    PieceMovement movement = piece.Movement;
		//}

        private Piece getPiece(Point pt)
        {
            if (!isPtInTable(pt))
                return null;

            return m_table[pt.Y, pt.X].Piece;
        }

		//public bool move(Point ptOld, Point ptNew)
		//{
		//    if (!isPtInTable(ptOld))
		//        return false;

		//    if (!isPtInTable(ptNew))
		//        return false;

		//    TableSquare oldSquare = m_table[ptOld.Y, ptOld.X];
		//    TableSquare newSquare = m_table[ptNew.Y, ptNew.X];
		//    Piece   pieceEaten = null;

		//    // Verify if the piece can move and which are the consequences.
		//    if (newSquare.hasPiece())
		//    {
		//        bool bBlack = oldSquare.Piece.IsBlack();
		//        if (newSquare.Piece.IsBlack() != bBlack)
		//            pieceEaten = newSquare.Piece; // Ate a piece of the other team.
		//        else
		//            return false; // Trying to move to a square with piece of the same team.
		//    }

		//    // Move the piece.
		//    m_table[ptNew.Y, ptNew.X].Piece = oldSquare.Piece;
		//    m_table[ptOld.Y, ptOld.X].Piece = null;

		//    if (!pieceEaten.Equals(null))
		//    {
		//        // Hold the eaten piece for later count or something.
		//        if (pieceEaten.IsBlack())
		//            m_vecDeadBlackPieces.Add(pieceEaten);
		//        else
		//            m_vecDeadWhitePieces.Add(pieceEaten);
		//    }

		//    return true;
		//}

        private bool isPtInTable(Point pt)
        {
            if (pt.X <= 0 || pt.X >= TABLE_SIZE)
                return false;
            if (pt.Y <= 0 || pt.Y >= TABLE_SIZE)
                return false;

            return true;
        }

        public bool IsInCheckMate(bool m_bBlack)
        {
            throw new NotImplementedException();
        }

		//static public Table operator +(Table table, Play play)
		//{
		//    // 2do - returns a copy of the old table modified with the play.
		//    throw new NotImplementedException();
		//}

		//static public Play operator -(Table newTable, Table oldTable)
		//{
		//    // 2do - return the play that represents the difference between these 2 tables.
		//    throw new NotImplementedException();
		//}

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
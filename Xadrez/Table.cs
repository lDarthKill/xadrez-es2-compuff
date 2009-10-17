using System;
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
        private class TableSquare // Squares of a table. Can hold a piece (or not).
        {
            Piece m_piece = null;

            public Piece Piece
            {
                get { return this.m_piece; }
                set { this.m_piece = value; }
            }

            public bool hasPiece()
            {
                return (m_piece != null);
            }
        }

        static Texture2D        m_selfImage;
        private TableSquare[,]  m_table;
        private List<Piece>     m_vecDeadBlackPieces;
        private List<Piece>     m_vecDeadWhitePieces;
        const int               TABLE_SIZE = 8;

        public Table()
        {
            m_table = new TableSquare[TABLE_SIZE, TABLE_SIZE];
            m_vecDeadBlackPieces = new List<Piece>();
            m_vecDeadWhitePieces = new List<Piece>();

            initTable(true);
        }

        // @param: bBlack - true if black pieces are in the bottom.
        private void
        initTable(bool bBlack)
        {
            int     nRow = 0,
                    nCol = 0;
            Piece piece;

            // Team pieces.
            // First row - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
            piece = new Rook(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Knight(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Bishop(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Queen(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new King(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Bishop(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Knight(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Rook(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;

            // Second row - Pawns
            ++nRow;
            for (nCol = 0; nCol < TABLE_SIZE; ++nCol)
            {
                piece = new Pawn(nRow, nCol, bBlack);
                m_table[nRow, nCol].Piece = piece;
            }


            nRow = TABLE_SIZE-1;
            nCol = 0;
            bBlack = !bBlack;

            // Other team pieces
            // First row - Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
            piece = new Rook(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Knight(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Bishop(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Queen(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new King(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Bishop(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Knight(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Rook(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;

            // Second row - Pawns
            --nRow;
            for (nCol = 0; nCol < TABLE_SIZE; ++nCol)
            {
                piece = new Pawn(nRow, nCol, bBlack);
                m_table[nRow, nCol].Piece = piece;
            }
        }

        public bool
        move(Point ptOld, Point ptNew)
        {
            if (!isPtInTable(ptOld))
                return false;

            if (!isPtInTable(ptNew))
                return false;

            TableSquare oldSquare = m_table[ptOld.Y, ptOld.X];
            TableSquare newSquare = m_table[ptNew.Y, ptNew.X];
            Piece   pieceEaten = null;

            // Verify if the piece can move and which are the consequences.
            if (newSquare.hasPiece())
            {
                bool bBlack = oldSquare.Piece.IsBlack();
                if (newSquare.Piece.IsBlack() != bBlack)
                    pieceEaten = newSquare.Piece; // Ate a piece of the other team.
                else
                    return false; // Trying to move to a square with piece of the same team.
            }

            // Move the piece.
            m_table[ptNew.Y, ptNew.X].Piece = oldSquare.Piece;
            m_table[ptOld.Y, ptOld.X].Piece = null;

            if (!pieceEaten.Equals(null))
            {
                // Hold the eaten piece for later count or something.
                if (pieceEaten.IsBlack())
                    m_vecDeadBlackPieces.Add(pieceEaten);
                else
                    m_vecDeadWhitePieces.Add(pieceEaten);
            }

            return true;
        }

        private bool
        isPtInTable(Point pt)
        {
            if (pt.X <= 0 || pt.X >= TABLE_SIZE)
                return false;
            if (pt.Y <= 0 || pt.Y >= TABLE_SIZE)
                return false;

            return true;
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

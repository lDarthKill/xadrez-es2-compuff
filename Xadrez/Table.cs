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
        }

        static Texture2D selfImage;
        private TableSquare[,] m_table;
        const int TABLE_SIZE = 8;

        public Table()
        {
            m_table = new TableSquare[TABLE_SIZE, TABLE_SIZE];
            initTable(true);
        }

        // @param: bBlack - true if black pieces are in the bottom.
        private void initTable(bool bBlack)
        {
            int     nRow = 0,
                    nCol = 0;
            Piece piece;

            // Black pieces.
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
            nCol = 0;
            ++nRow;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;


            nRow = TABLE_SIZE-1;
            nCol = 0;
            bBlack = !bBlack;

            // White pieces
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
            nCol = 0;
            --nRow;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
            piece = new Pawn(nRow, nCol, bBlack);
            m_table[nRow, nCol].Piece = piece;
            ++nCol;
        }

        static public 
        void SetSelfImage(Texture2D _texture)
        {
            selfImage = _texture;
        }

        static public
        Texture2D GetSelfImage()
        {
            return selfImage;
        }


    }
}

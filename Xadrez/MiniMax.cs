using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xadrez;
using Microsoft.Xna.Framework;

namespace IA
{
    class MiniMax
    {
        private bool    m_bBlack;
        private bool    m_bTeamTemp;
        private Table   m_winnerTable;
        
        public MiniMax()
        {
        }

        public void setTeam(bool bBlack)
        {
            m_bBlack = bBlack;
        }

        public Play getCPUPlay(Table table)
        {
            negamax(table, 2);
            if (m_winnerTable == null)
                return null;

            Play play = m_winnerTable - table;
            m_winnerTable = null;
            return play;
        }

        private int negamax(Table table, int depth)
        {
            if (/*table.IsInCheckMate(m_bBlack) || */(depth == 0))
            {
                //m_winnerTable = table;
                return evaluateTable(table);
            }
            else
            {
                if ((depth % 2) == 1)
                    m_bTeamTemp = m_bBlack;
                else
                    m_bTeamTemp = !m_bBlack;

                int alpha = int.MinValue;
                List<Table> lstTables = generateTables(table);
                foreach (Table childTable in lstTables)
                {
                    int alphaTmp = Math.Max(alpha, -negamax(childTable, depth - 1));
                    if (alphaTmp > alpha)
                    {
                        alpha = alphaTmp;
                        m_winnerTable = childTable;
                    }
                }

                return alpha;
            }

            /*
            ROTINA minimax(nó, profundidade)
                SE nó é um nó terminal OU profundidade = 0 ENTÃO
                    RETORNE o valor da heurística do nó
                SENÃO
                    α ← -∞                       { a avaliação é idêntica para ambos os jogadores }
                    PARA CADA filho DE nó
                        α ← max(α, -minimax(filho, profundidade-1))
                    FIM PARA
                    RETORNE α
                FIM SE
            FIM ROTINA
            */
        }

        private int evaluateTable(Table table)
        {
            int nValue = 0;

            for (int nRow = 0; nRow < table.TableSize; ++nRow)
            {
                for (int nCol = 0; nCol < table.TableSize; ++nCol)
                {
                    Piece piece = table.getTableSquare(nCol, nRow).Piece;
                    if (piece == null)
                        continue;

                    if (m_bBlack != piece.Black)
                    {
                        if (piece is Queen)
                        {
                            nValue += 50;
                        }
                        else if (piece is Bishop)
                        {
                            nValue += 25;
                        }
                        else if (piece is Rook)
                        {
                            nValue += 20;
                        }
                        else if (piece is Knight)
                        {
                            nValue += 10;
                        }
                        else if (piece is Pawn)
                        {
                            nValue += 5;
                        }
                    }
                    else
                    {
                        if (piece is Queen)
                        {
                            nValue -= 30;
                        }
                        else if (piece is Bishop)
                        {
                            nValue -= 15;
                        }
                        else if (piece is Rook)
                        {
                            nValue -= 10;
                        }
                        else if (piece is Knight)
                        {
                            nValue -= 5;
                        }
                        else if (piece is Pawn)
                        {
                            nValue -= 2;
                        }
                    }
                }
            }

            return nValue;
        }

        private List<Table> generateTables(Table table)
        {
            List<Table> lstTables = new List<Table>();

            for (int nRow = 0; nRow < table.TableSize; ++nRow)
            {
                for (int nCol = 0; nCol < table.TableSize; ++nCol)
                {
                    Table.TableSquare tableSquare = table.getTableSquare(nRow, nCol);

                    // Verify if the is a piece in this square.
                    if (tableSquare.Piece == null)
                        continue;

                    // Verify if the piece is mine.
                    if (tableSquare.Piece.Black != m_bTeamTemp)
                        continue;
                    
                    List<Point> lstPositions = tableSquare.Piece.getPossiblePositions();
                    foreach (Point position in lstPositions)
                    {
                        Play play = new Play();
                        play.oldPosition = new Point(nRow, nCol);
                        play.newPosition = position;
                        Table newTable = table + play;
                        lstTables.Add(newTable);
                    }
                }
            }

            return lstTables;
        }
    }
}

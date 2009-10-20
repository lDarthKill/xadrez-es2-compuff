using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xadrez;

namespace IA
{
    class MiniMax
    {
        private bool    m_bBlack;
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
            negamax(table, 3);
            return (m_winnerTable - table);
        }

        private int negamax(Table table, int depth)
        {
            if (table.IsInCheckMate(m_bBlack) || (depth == 0))
            {
                m_winnerTable = table;
                return evaluateTable(table);
            }
            else
            {
                int alpha = int.MinValue;
                List<Table> lstTables = generateTables(table);
                foreach (Table childTable in lstTables)
                {
                    int alphaTmp = Math.Max(alpha, -negamax(childTable, depth - 1));
                    if (alphaTmp > alpha)
                    {
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
            throw new NotImplementedException();
        }

        private List<Table> generateTables(Table table)
        {
            throw new NotImplementedException();
        }
    }
}

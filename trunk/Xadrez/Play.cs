using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Xadrez
{
    public class Play
    {
        Point   m_ptOld,
                m_ptNew;

        public Point oldPosition
        {
            get { return m_ptOld; }
            set { m_ptOld = value; }
        }

        public Point newPosition
        {
            get { return m_ptNew; }
            set { m_ptNew = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstTypes;

namespace Xadrez
{
    public class Piece
    {
        bool m_bAlive;
        bool m_bBlack;
        
//        static Texture2D selfImageBlack;
//        static Texture2D selfImageWhite;

        Point m_position = new Point(-1, -1);

        public Piece(bool bBlack)
        {
            this.m_bBlack = bBlack;
        }

        public Piece(int posX, int posY, bool bBlack)
        {
            SetPosition(posX, posY);
            this.m_bBlack = bBlack;
        }

        public Point getPosition()
        {
            return m_position;
        }

        public void SetPosition(int _posX, int _posY)
        {
            m_position.X = _posX;
            m_position.Y = _posY;
        }

        public bool IsAlive()
        {
            return m_bAlive;
        }

        public void SetAlive(bool _bAlive)
        {
            m_bAlive = _bAlive;
        }

        public bool IsBlack()
        {
            return m_bBlack;
        }

        public void SetBlack(bool bBlack)
        {
            this.m_bBlack = bBlack;
        }

/*        public static void SetSelfImageBlack(Texture2D _texture)
        {
            selfImageBlack = _texture;
        }

        public static void SetSelfImageWhite(Texture2D _texture)
        {
            selfImageWhite = _texture;
        }
 */
    }
}

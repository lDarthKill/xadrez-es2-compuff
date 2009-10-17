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
        public class PieceMovement
        {
            // 2do - How to describe movement rules?
        }

        private     bool            m_bAlive;
        private     bool            m_bBlack;
        private     Point           m_position;
        protected   PieceMovement   m_movement;
        
//        static Texture2D selfImageBlack;
//        static Texture2D selfImageWhite;

        public Piece(bool bBlack)
        {
            this.m_bBlack = bBlack;
            m_position = new Point();
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

        public PieceMovement Movement
        {
            get { return m_movement; }
            // Tácio - Doesn`t need a "set" accessor, the movement of a piece is immutable.
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

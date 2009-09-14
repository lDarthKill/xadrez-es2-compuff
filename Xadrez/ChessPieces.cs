using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstTypes;

namespace Xadrez
{
    public class ChessPieces
    {
        bool bAlive;
        bool bBlack;
        
//        static Texture2D selfImageBlack;
//        static Texture2D selfImageWhite;

        Table_Position position;

        public ChessPieces()
        {
        }

        public Table_Position GetPosition()
        {
            return position;            
        }

        public void SetPosition(int _posX, int _posY)
        {
            position.X = _posX;
            position.Y = _posY;
        }

        public bool IsAlive()
        {
            return bAlive;
        }

        public void SetAlive(bool _bAlive)
        {
            bAlive = _bAlive;
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

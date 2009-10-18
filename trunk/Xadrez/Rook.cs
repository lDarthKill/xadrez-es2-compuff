using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class Rook : Xadrez.Piece
    {
        static Texture2D selfImageBlack;
        static Texture2D selfImageWhite;

        public Rook(int _posX, int _posY, bool bBlack) : base(_posX, _posY, bBlack)
        {
        }

        public
        bool
        TestRules(int _posX, int _posY)
        {
            return false;
        }

        public static void SetSelfImageBlack(Texture2D _texture)
        {
            selfImageBlack = _texture;
        }

        public static void SetSelfImageWhite(Texture2D _texture)
        {
            selfImageWhite = _texture;
        }
    }
}

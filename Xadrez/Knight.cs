﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class Knight : Xadrez.ChessPieces
    {
        static Texture2D selfImageBlack;
        static Texture2D selfImageWhite;

        public Knight(int _posX, int _posY)
        {
            SetPosition(_posX, _posY);
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
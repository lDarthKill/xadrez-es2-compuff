using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class Table
    {
        static Texture2D selfImage;

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

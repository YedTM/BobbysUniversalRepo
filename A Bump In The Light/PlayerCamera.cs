using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bump_in_the_Light
{
    internal class PlayerCamera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            Matrix position = Matrix.CreateTranslation(
                - target.Position.X - (target.PosX),
                0,
                0);

            Matrix offset = Matrix.CreateTranslation(
                (Game1.screenWidth / 2) - 45,
                0, 
                0);

            Transform = position * offset;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bump_in_the_Light
{
    /// <summary>
    /// This class acts as the general framework for most of the other
    /// classes in this project. It has fields for the texture and rectangles
    /// of the objects. It contains a Draw method as well.
    /// </summary>
    public abstract class GameObject : Component
    {
        //Field Section
        protected Texture2D gameAsset;
        protected Rectangle assetPosition;

        //Property Section
        /// <summary>
        /// Allows other classes to read and set the assetPosition field
        /// </summary>
        public Rectangle Position
        {
            get
            {
                return assetPosition;
            }
            set
            {
                assetPosition = value;
            }
        }

        /// <summary>
        /// Allows other classes to read and set the assetPosition's width
        /// </summary>
        public int PositionWidth
        {
            get
            {
                return assetPosition.Width;
            }
            set
            {
                assetPosition.Width = value;
            }
        }

        //Constructor Section
        public GameObject(Texture2D asset, Rectangle position)
        {
            this.gameAsset = asset;
            this.assetPosition = position;
        }

        //Method Section
        /// <summary>
        /// A draw method meant to be overriden by its child classes
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1</param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }

    }
}

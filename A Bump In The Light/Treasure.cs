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
    public class Treasure : GameObject
    {

        private Rectangle position;
        private Texture2D texture;
        private bool holdingTreasure;

        /// <summary>
        /// Allows other classes to read the texture field
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Allows other classes to read and set the holdingTreasure field
        /// </summary>
        public bool HoldingTreasure
        {
            get { return holdingTreasure; }
            set { holdingTreasure = value; }
        }


        //Constructor
        public Treasure(Texture2D asset, Rectangle properties) : base(asset, properties)
        {
            texture = asset;
            position = properties;
            holdingTreasure = false;
        }

        //Method Section
        /// <summary>
        /// This draws the treasure to the game window
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1</param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// This update method overrides the necessary update method from
        /// GameObject
        /// </summary>
        /// <param name="gameTime">gameTime from Game1</param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// This method checks if the player has interesected with the treasure
        /// and will set holdingTreasure to true if it does
        /// </summary>
        /// <param name="player">The player of the game</param>
        public void AcquireTreasure(Player player)
        {
            if (position.Intersects(player.McsteelyBody) && player.Lives > 0)
            {
                holdingTreasure = true;
            }
        }

        /// <summary>
        /// This draws the treasure to the game window
        /// </summary>
        /// <param name="gametime">gameTime from Game1</param>
        /// <param name="sb">spriteBatch from Game1</param>
        public override void Draw(GameTime gametime, SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }




    }
}

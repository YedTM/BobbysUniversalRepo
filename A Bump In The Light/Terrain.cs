using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bump_in_the_Light
{
    // Terrain is a child of GameObject
    public class Terrain : GameObject
    {

        private Rectangle position;    // The terrain's position
        private Texture2D texture;     // The terrain's texture
        private Rectangle topOfPlatform;
        private GameTime gameTime;
        private bool sideOfWall;

        private bool onPlatform;

        /*
        public Rectangle Posistion
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public Terrain(Texture2D asset, Rectangle properties) : base(asset, properties)
        {
            this.texture = asset;
            this.position = properties;
        }

        public virtual void Update(GameTime gameTime, List<Terrain> terrains)
        { }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gametime, SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }

        public bool IsTouchingLeft(Terrain terrain, Player player)
        {
            return this.Posistion.Right + player.VelocityX > terrain.Posistion.Left &&
                   this.Posistion.Left < terrain.Posistion.Left &&
                   this.Posistion.Bottom > terrain.Posistion.Top &&
                   this.Posistion.Top < terrain.Posistion.Bottom;
        }
        public bool IsTouchingRight(Terrain terrain, Player player)
        {
            return this.Posistion.Left + player.VelocityX < terrain.Posistion.Right &&
                   this.Posistion.Right > terrain.Posistion.Right &&
                   this.Posistion.Bottom > terrain.Posistion.Top &&
                   this.Posistion.Top < terrain.Posistion.Bottom;
        }
        public bool IsTouchingTop(Terrain terrain, Player player)
        {
            return this.Posistion.Bottom + player.VelocityY > terrain.Posistion.Top &&
                   this.Posistion.Top < terrain.Posistion.Top &&
                   this.Posistion.Right > terrain.Posistion.Left &&
                   this.Posistion.Left < terrain.Posistion.Right;
        }
        public bool IsTouchingBottom(Terrain terrain, Player player)
        {
            return this.Posistion.Top + player.VelocityX < terrain.Posistion.Bottom &&
                   this.Posistion.Bottom > terrain.Posistion.Bottom &&
                    this.Posistion.Right > terrain.Posistion.Left &&
                   this.Posistion.Left < terrain.Posistion.Right;
        }
        */

        // Original iteration
        //
        

        /// <summary>
        /// Parameterized constructor which initializes Terrain's
        /// fields with the GameObject base fields
        /// </summary>
        /// <param name="asset">The terrain texture</param>
        /// <param name="properties">The platform size and position</param>
        public Terrain(Texture2D asset, Rectangle properties) : base(asset, properties)
        {
            texture = asset;
            position = properties;
            topOfPlatform = new Rectangle(position.X, position.Y - 1, position.Width, 1);
            onPlatform = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool OnPlatform
        {
            get
            {
                return onPlatform;
            }
        }
        /// <summary>
        /// Read only property for the Rectangle position
        /// of the terrain
        /// </summary>
        public Rectangle Position
        {
            get { return position; }
        }

        /// <summary>
        /// Read only property for the Texture2d
        /// texture of the terrain
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }


        public Rectangle TopOfPlatform
        {
            get { return topOfPlatform; }
        }


        /// <summary>
        /// Draws the Terrain to the screen using its
        /// texture, position, and setting its color to white
        /// </summary>
        /// <param name="sb">SpriteBatch object</param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Override method purely for compiling
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public override void Update(GameTime gameTime) { }


        public void IsSteppedOn(Player player)
        {

            Rectangle interactionSpace = Rectangle.Intersect(position, player.McsteelyBody);

            // Checks if the player and platform are touching
            if (position.Intersects(player.McsteelyBody))
            {

                player.HasJumped = false;

                // Rectangle object initialized as the intersecting
                // space between the player and the platform

                // Runs if interactionSpace's height is greater than it's width
                if (interactionSpace.Height > interactionSpace.Width)
                {

                    player.CannotJump = true;

                    // Moves player to the right of the platform
                    if (position.X - player.McSteelyBodyX < 0)
                    {
                        player.PosX += interactionSpace.Width;
                        player.PosY += 5;
                        sideOfWall = true;
                    }
                    // Moves player to the left of the platform
                    else if (position.X - player.McSteelyBodyX > 0)
                    {
                        player.PosX -= interactionSpace.Width;
                        player.PosY += 5;
                        sideOfWall = true;
                    }
                }

                // Runs if interactionSpace's width is greater than it's height
                if (interactionSpace.Height < interactionSpace.Width)
                {

                    player.CannotJump = false;

                    // Moves player below the platform
                    if (position.Y - player.PosY < 0)
                    {
                        player.PosY += interactionSpace.Height;
                        if (onPlatform == false)
                        {
                            player.HasJumped = true;
                        }

                    }
                    else if (position.Y - player.PosY > 0)
                    {
                        player.PosY -= interactionSpace.Height;
                        player.Gravity = false;
                        player.Jumping = false;
                        onPlatform = true;
                    }
                }
            }
            else if (interactionSpace.Height == 0 && interactionSpace.Width == 0)
            {
                player.Gravity = true;
                //Debug.WriteLine("Hello!");
            }


        }


        ///// <summary>
        ///// Creates collision between the player and a platform
        ///// </summary>
        ///// <param name="player">The player</param>
        //public void IsSteppedOn(Player player)
        //{
        //    bool playerJumping = player.HasJumped;


        //    if (topOfPlatform.Intersects(player.McsteelyBody))
        //    {
        //        player.Gravity = false;
        //    }

        //    // Checks if the player and platform are touching
        //    if (position.Intersects(player.McsteelyBody))
        //    {

        //        player.HasJumped = false;
        //        player.HasDashed = false;

        //        // Rectangle object initialized as the intersecting
        //        // space between the player and the platform
        //        Rectangle interactionSpace = Rectangle.Intersect(position, player.McsteelyBody);

        //        // Runs if interactionSpace's height is greater than it's width
        //        if (interactionSpace.Height > interactionSpace.Width)
        //        {
        //            player.HasJumped = true;

        //            // Moves player to the right of the platform
        //            if (position.X - player.McSteelyBodyX < 0)
        //            {
        //                player.PosX += interactionSpace.Width;
        //            }
        //            // Moves player to the left of the platform
        //            else if (position.X - player.McSteelyBodyX > 0)
        //            {
        //                player.PosX -= interactionSpace.Width;
        //            }
        //        }

        //        // Runs if interactionSpace's width is greater than it's height
        //        else if (interactionSpace.Height < interactionSpace.Width)
        //        {
        //            // Moves player below the platform
        //            if (position.Y - player.PosY < 0)
        //            {
        //                player.PosY += interactionSpace.Height;
        //            }
        //            // Moves player above the platform
        //            else if (position.Y - player.PosY > 0)
        //            {
        //                player.PosY -= interactionSpace.Height;
        //            }
        //        }
        //    }

        //    else if (topOfPlatform.Intersects(player.McsteelyBody) == false)
        //    {
        //        player.Gravity = true;
        //    }



        //    playerJumping = player.HasJumped;


        //    //if (topOfPlatform.Intersects(player.McsteelyBody) == false && playerJumping != player.HasJumped)
        //    //{
        //    //    player.HasJumped = true;
        //    //}

        //    //if (topOfPlatform.Intersects(player.McsteelyBody) == false && player.PosY + player.PlayerTexture.Height != 1080)
        //    //{
        //    //    player.HasJumped = true;
        //    //}

        //}

        public override void Draw(GameTime gametime, SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }


        public bool StandingOnTop(Rectangle rectangle)
        {
            if (topOfPlatform.Intersects(rectangle))
            {
                return true;
            }
            return false;
        }

    }
}

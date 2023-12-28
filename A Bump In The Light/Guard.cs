using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A_Bump_in_the_Light
{
    /// <summary>
    /// This enum determines where the guard and light faces
    /// and if they move in their faced direction
    /// </summary>
    public enum GuardStates
    {
        FaceRight, 
        MoveRight, 
        FaceLeft, 
        MoveLeft
    }

    /// <summary>
    /// This class is used to create the Guard Enemies and cause them to
    /// move around the game. The guard and its light has the ability to 
    /// move and face in different directions depending on the enum state
    /// it is in, which is determined by how many frames have gone by.
    /// </summary>
    public class Guard : Enemy
    {
        //Field Section
        private GuardStates guardState;
        private GuardStates previousState;
        private Vector2 guardHand;

        private int numSheetSprites;
        private int spriteWidth;

        private double fps;
        private double secondsPerFrame;
        private int guardCurrentFrame;
        private double timeCounter;

        //Properties Section
        /// <summary>
        /// Allows the guardState to be read by other classes
        /// </summary>
        public GuardStates GuardState
        {
            get 
            { 
                return guardState; 
            }
        }

        //Constructor
        public Guard(Texture2D asset, Rectangle position, int movement, Texture2D lightTexture,
                      LightStates lightState, Rectangle lightProperties, GuardStates guardState) 
                    : base(asset, position, movement, lightTexture, lightState, lightProperties)
        {
            this.guardState = guardState;
            previousState = GuardStates.FaceLeft;
            guardHand = new Vector2(position.X + position.Width, position.Y);
            lightProperties.X = (int)guardHand.X;
            lightProperties.Y = (int)guardHand.Y;

            numSheetSprites = 6;

            spriteWidth = asset.Width / numSheetSprites;

            PositionWidth = spriteWidth;

            fps = 24.0;                    // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;   // How long each animation frame lasts
            timeCounter = 0;               // Time passed since animation
            guardCurrentFrame = 1;      // Sprite sheet's first animation frame is 1 (not 0)
        }

        //Method Section
        /// <summary>
        /// Draws the guard in two directions, right and left. Also draws
        /// the guard's light in the respective direction as well
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1</param>
        public override void Draw(SpriteBatch sb)
        {
            // Each case of this switch statement draws the respective
            // moving or walking animation that the GuardState requires.
            // The guard's light is also drawn in accordance with the direction
            // the guard is facing
            switch (guardState)
            {
                case GuardStates.FaceRight:
                    DrawGaurdStanding(SpriteEffects.None, sb);
                    sb.Draw(lightTexture, new Rectangle(
                        lightProperties.X, 
                        lightProperties.Y, 
                        (int)(lightProperties.Width * 1.3), 
                        lightProperties.Height), null, Color.White, 0, new Vector2(0, 0), 
                        SpriteEffects.FlipHorizontally, 1);
                    break;
                case GuardStates.MoveRight:
                    DrawGuardWalking(SpriteEffects.None, sb);
                    sb.Draw(lightTexture, new Rectangle(
                        lightProperties.X, 
                        lightProperties.Y, 
                        (int)(lightProperties.Width * 1.3), 
                        lightProperties.Height), null, Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 1);
                    break;
                case GuardStates.FaceLeft:
                    DrawGaurdStanding(SpriteEffects.FlipHorizontally, sb);
                    sb.Draw(lightTexture, new Rectangle(
                        lightProperties.X - 20, 
                        lightProperties.Y, 
                        (int)(lightProperties.Width * 1.3), 
                        lightProperties.Height), null, Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.None, 1);
                    break;
                case GuardStates.MoveLeft:
                    DrawGuardWalking(SpriteEffects.FlipHorizontally, sb);
                    sb.Draw(lightTexture, new Rectangle(
                        lightProperties.X - 20, 
                        lightProperties.Y, 
                        (int)(lightProperties.Width * 1.3), 
                        lightProperties.Height), null, Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.None, 1);
                    break;
            }
        }

        /// <summary>
        /// The guard and light will move either left or right depending
        /// on the guard state they are in. They will move for a chosen amount
        /// of frames before switching to a new guard state. The guard and camera will remain
        /// stationary for 120 frames after moving in a direction
        /// </summary>
        /// <param name="gameTime">GameTime from Game1</param>
        /// <param name="gameState">The enum state Game1 is in</param>
        public override void Update(GameTime gameTime, GameState gameState)
        {
            // The guard will only update if the GameState is Game to avoid
            // guards moving when they shouldn't
            if (gameState == GameState.Game)
            {
                switch (guardState)
                {

                    case GuardStates.FaceRight:
                        // If the guard was previously facing left
                        if (previousState == GuardStates.FaceLeft)
                        {
                            //The guard's X is moved to the right side of the guard,
                            //with the light being positioned where the hand is
                            guardHand.X = Position.X + Position.Width;
                            lightProperties.X = (int)guardHand.X;
                            // If 120 frames pass, then the guard will begin to move right
                            if (frameCounter < 120)
                            {
                                frameCounter++;
                            }
                            else
                            {
                                frameCounter = 0;
                                previousState = guardState;
                                guardState = GuardStates.MoveRight;
                            }
                        }
                        // If the guard was moving to the right
                        else if (previousState == GuardStates.MoveRight)
                        {
                            // 120 frames will be counted before the guard faces to the left
                            if (frameCounter < 120)
                            {
                                frameCounter++;
                            }
                            else
                            {
                                frameCounter = 0;
                                previousState = guardState;
                                guardState = GuardStates.FaceLeft;
                            }
                        }
                        break;
                    case GuardStates.MoveRight:
                        //The guard and light will move to the right 5 units
                        //as long as the amount of frames that have passed is below the
                        //movement field amount
                        assetPosition.X += 5;
                        lightProperties.X += 5;
                        if (frameCounter < movement)
                        {
                            frameCounter++;
                        }
                        //The guard will face right if the amount of frames exceeds the movement
                        //field
                        else 
                        {
                            frameCounter = 0;
                            previousState = guardState;
                            guardState = GuardStates.FaceRight;
                        }
                        break;
                    case GuardStates.FaceLeft:
                        //If the guard was previously facing right
                        if (previousState == GuardStates.FaceRight)
                        {
                            //The guard's X is moved to the left side of the guard,
                            //with the light being positioned where the hand is (offset by the
                            //width of the light)
                            guardHand.X = Position.X;
                            lightProperties.X = (int)guardHand.X - lightProperties.Width;

                            //After 120 frames have passed, the guard will begin to move left
                            if (frameCounter < 120)
                            {
                                frameCounter++;
                            }
                            else
                            {
                                frameCounter = 0;
                                previousState = guardState;
                                guardState = GuardStates.MoveLeft;
                            }
                        }
                        //If the guard was previously moving left
                        else if (previousState == GuardStates.MoveLeft)
                        {
                            //Then the guard will face right after 120 frames have passed
                            if (frameCounter < 120)
                            {
                                frameCounter++;
                            }
                            else
                            {
                                frameCounter = 0;
                                previousState = guardState;
                                guardState = GuardStates.FaceRight;
                            }
                        }
                        break;
                    case GuardStates.MoveLeft:
                        // The guard and his light will begin to move 5 units to the left
                        assetPosition.X -= 5;
                        lightProperties.X -= 5;
                        //Until the framecounter exceeds the movement field and causes the 
                        //guard to face left
                        if (frameCounter < movement)
                        {
                            frameCounter++;
                        }
                        else
                        {
                            frameCounter = 0;
                            previousState = guardState;
                            guardState = GuardStates.FaceLeft;
                        }
                        break;


                }
                //The guard's animation is then updated to ensure the right animation
                //frame is being displayed
                UpdateAnimation(gameTime);
            }
        }

        /// <summary>
		/// Helper for updating guard animation based on time
		/// </summary>
		/// <param name="gameTime">Info about time from MonoGame</param>
		private void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds / 2;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                guardCurrentFrame++;

                if (guardCurrentFrame >= 6)
                {
                    guardCurrentFrame = 0;
                }

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }

        /// <summary>
        /// If the light's rectangle intersects with the player's rectangle
        /// then the method returns true
        /// </summary>
        /// <param name="thief">The player class's object</param>
        /// <returns>True if the rectangle's intersect</returns>
        public override bool Detection(Player thief)
        {
            if (lightProperties.Intersects(thief.McsteelyBody))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
		/// Draws Gaurd with a walking animation.
		/// </summary>
		/// <param name="flip">Should he be flipped horizontally or vertically?</param>
		private void DrawGuardWalking(SpriteEffects flip, SpriteBatch sb)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            sb.Draw(
                gameAsset,                                      // Whole sprite sheet
                Position,                                       // Position of the Guard sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    guardCurrentFrame * spriteWidth,            // - Left edge
                    0,                                          // - Top of sprite sheet
                    spriteWidth,                                // - Width 
                    gameAsset.Height),                          // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet                                         
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flip"></param>
        private void DrawGaurdStanding(SpriteEffects flip, SpriteBatch sb)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.
            sb.Draw(
                gameAsset,                                   // Whole sprite sheet
                Position,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    0,                                          // - Left edge
                    0,                                          // - Top of sprite sheet
                    spriteWidth,                        // - Width 
                    gameAsset.Height),                       // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet                       
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// A Draw method that uses the guard states to draw different 
        /// guards frames to simulate movement
        /// </summary>
        /// <param name="gametime">Gametime from Game1</param>
        /// <param name="sb">SpriteBatch from Game1</param>
        public override void Draw(GameTime gametime, SpriteBatch sb)
        {
            
            switch (guardState)
            {
                case GuardStates.FaceLeft:
                    guardState = GuardStates.FaceLeft;
                    DrawGuardWalking(SpriteEffects.FlipHorizontally, sb);
                    break;
                case GuardStates.FaceRight:
                    guardState = GuardStates.FaceRight;
                    DrawGuardWalking(SpriteEffects.None, sb);
                    break;
                case GuardStates.MoveLeft:
                    guardState = GuardStates.MoveLeft;
                    DrawGuardWalking(SpriteEffects.FlipHorizontally, sb);
                    break;
                case GuardStates.MoveRight:
                    guardState = GuardStates.MoveRight;
                    DrawGuardWalking(SpriteEffects.None, sb);
                    break;
                   
            }

            if (guardState == GuardStates.FaceLeft || guardState == GuardStates.MoveLeft)
            {
                guardState = GuardStates.FaceLeft;
            }
            else if (guardState == GuardStates.FaceRight || guardState == GuardStates.MoveRight)
            {
                guardState = GuardStates.FaceRight;
            }

        }

    }
}

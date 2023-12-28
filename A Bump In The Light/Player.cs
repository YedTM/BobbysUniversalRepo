using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace A_Bump_in_the_Light
{
    /// <summary>
    /// Diffrent states for movment and animation of character
    /// </summary>
    public enum PlayerState
    {
        MoveLeft,
        MoveRight,
        FaceLeft,
        FaceRight,
        JumpingLeft,
        JumpingRight
    }
    public class Player : Terrain
    {
        //Fields Section
        private int lives;
        private bool isHolding;
        private int movementSpeed;
        private bool gravity = true;

        // Tells if player has jumped
        private bool hasJumped;
        private bool hasDashed;
        private int dashMeter;
        private bool cannotJump;

        private PlayerState steelyState;

        // texture, position, feet position, and velocity
        private Texture2D playerTexture;
        private Vector2 charPosition;
        private Vector2 velocity;
        private static Rectangle playerBody;
        private Rectangle mcsteelyBody;
        private Rectangle playerFeet;
        private Color myColor;
        private Terrain terrain;

        // Data for sprite Sheet
        private int numSheetSprites;
        private int jumpingNumSheetSprites;
        private int idleNumSheetSprites;
        private int spriteWidth;
        private int jumpingSpriteWidth;
        private int idleSpriteWidth;


        // Animation Data
        private int mcSteelyCurrentFrame;
        private int mcSteelyJumpingCurrentFrame;
        private int mcSteelyIdleCurrentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;
        private double timeCounterJumping;
        private double timeCounterIdle;
        private KeyboardState currentKybState;
        private KeyboardState previousKybState;

        private Texture2D jumpingAsset;
        private Texture2D idleAsset;

        private float velX;

        private double previousYVel;

        private bool jumping;

        /// <summary>
        /// X velocity of the character
        /// </summary>
        public float VelX
        {
            get
            {
                return velX;
            }
            set
            {
                velX = value;
            }
        }
        /// <summary>
        /// Able to set chosen color 
        /// </summary>
        public Color MyColor
        {
            set { myColor = value; }
        }

        /// <summary>
        /// Checks if the player has jumped, can return and set value
        /// </summary>
        public bool HasJumped
        {
            get { return hasJumped; }
            set { hasJumped = value; }
        }

        /// <summary>
        /// Check if character has dashed 
        /// </summary>
        public bool HasDashed
        {
            get { return hasDashed; }
            set { hasDashed = value; }
        }

        /// <summary>
        /// Checks for gravity in use
        /// </summary>
        public bool Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        public bool CannotJump
        {
            set { cannotJump = value; }
        }


        /// <summary>
        /// Checks if mcsteely is holding, returns true if he is
        /// </summary>
        public bool IsHolding
        {
            get
            {
                return isHolding;
            }
            set
            {
                isHolding = value;
            }
        }
        /// <summary>
        /// Returns and sets lives
        /// </summary>
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
            }
        }
        /// <summary>
        /// Retrusn and sets the current player state
        /// </summary>
        public PlayerState PlayerState
        {
            get
            {
                return steelyState;
            }
            set
            {
                steelyState = value;
            }
        }
        /// <summary>
        /// Retrusn and sets the current player body
        /// </summary>
        public Rectangle McsteelyBody
        {
            get
            {
                return mcsteelyBody;
            }
            set
            {
                mcsteelyBody = value;
            }
        }

        /// <summary>
        /// Player position
        /// </summary>
        public float PosX
        {
            get
            {
                return charPosition.X;
            }
            set
            {
                charPosition.X = value;
            }
        }

        /// <summary>
        /// Player position
        /// </summary>
        public float PosY
        {
            get
            {
                return charPosition.Y;
            }
            set
            {
                charPosition.Y = value;
            }
        }

        /// <summary>
        /// Read only property which returns the playerFeet rectangle
        /// </summary>
        public Rectangle PlayerFeet
        {
            get { return playerFeet; }
            set { playerFeet = value; }
        }

        /// <summary>
        /// Get and set property for the height of the player's feet
        /// </summary>
        public int PlayerFeetY
        {
            get { return playerFeet.Y; }
            set { playerFeet.Y = value; }
        }
        /// <summary>
        /// Get and set property for the height of the player's feet
        /// </summary>
        public int PlayerFeetX
        {
            get { return playerFeet.X; }
            set { playerFeet.X = value; }
        }
        /// <summary>
        /// Get and set property for the height of the player's feet
        /// </summary>
        public float VelocityX
        {
            get { return velocity.X; }
            set { velocity.X = value; }
        }
        /// <summary>
        /// Mcsteely y velocity
        /// </summary>
        public float VelocityY
        {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        /// <summary>
        /// Player Texture
        /// </summary>
        public Texture2D PlayerTexture
        {
            get { return playerTexture; }
        }

        /// <summary>
        /// Checks if character is jumping
        /// </summary>
        public bool Jumping
        {
            get
            {
                return jumping;
            }
            set
            {
                jumping = value;
            }
        }

        /// <summary>
        /// Player body X pos
        /// </summary>
        public int McSteelyBodyX
        {
            get { return mcsteelyBody.X; }
            set { mcsteelyBody.X = value; }
        }
        
        /// <summary>
        /// Player body Y pos
        /// </summary>
        public int McSteelyBodyY
        {
            get { return mcsteelyBody.Y; }
            set { mcsteelyBody.Y = value; }
        }

        /// <summary>
        /// Creates our main character for the game
        /// </summary>
        /// <param name="asset">Texture for character</param>
        /// <param name="newPosition">position for character</param>
        /// <param name="lives">Num of lives for character</param>
        /// <param name="isHolding">If character is holding treasure</param>
        /// <param name="movementSpeed">Movement speed of character, NOT SET UP YET REMOVE WHEN SET</param>
        /// <param name="playerState">Player state, left or right</param>
        /// <param name="playerState">A rectangle that tracks the bottom of the player. Equal to the player's
        /// width and at the bottom of their model.</param>
        public Player(Texture2D asset, Texture2D jumpingAsset, Texture2D idleAsset, Vector2 newPosition, int lives,
                      bool isHolding, int movementSpeed, PlayerState playerState, Rectangle playerFeet) : base(asset, playerBody)
        {
            // Sets all values given
            this.idleAsset = idleAsset;
            this.jumpingAsset = jumpingAsset;
            this.lives = lives;
            this.isHolding = isHolding;
            this.movementSpeed = movementSpeed;
            this.playerTexture = asset;
            this.charPosition = newPosition;
            this.hasJumped = false;
            this.playerFeet = new Rectangle(playerBody.X + (playerBody.X - playerFeet.X), 
                playerBody.Y + (playerBody.Y - playerFeet.Y), 50, 50);
            this.steelyState = playerState;
            myColor = Color.White;
            cannotJump = true;

            // Character hitbox
            this.mcsteelyBody = new Rectangle(-50, -50, 50, 74);

            // Animation data //
            numSheetSprites = 9;
            jumpingNumSheetSprites = 8;
            idleNumSheetSprites = 14;
            velX = 5;

            spriteWidth = asset.Width / numSheetSprites;

            jumpingSpriteWidth = jumpingAsset.Width / jumpingNumSheetSprites;

            idleSpriteWidth = idleAsset.Width / idleNumSheetSprites;

            fps = 24.0;                    // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;   // How long each animation frame lasts
            timeCounter = 0;               // Time passed since animation
            timeCounterJumping = 0;
            timeCounterIdle = 0;
            mcSteelyCurrentFrame = 1;      // Sprite sheet's first animation frame is 1 (not 0)

            gravity = true;
        }
        //Methods Section
        //All methods are currently placeholders
        public override void Draw(SpriteBatch sb)
        {
            // Used for changing state for movment based on current state,
            // and drawing the current state charcter is in
            switch (steelyState)
            {
                case PlayerState.FaceLeft:
                    steelyState = PlayerState.FaceLeft;
                    DrawSteelyStanding(SpriteEffects.FlipHorizontally, sb);
                    break;
                case PlayerState.FaceRight:
                    steelyState = PlayerState.FaceRight;
                    DrawSteelyStanding(SpriteEffects.None, sb);
                    break;
                case PlayerState.MoveLeft:
                    steelyState = PlayerState.MoveLeft;
                    DrawSteelyWalking(SpriteEffects.FlipHorizontally, sb);
                    break;
                case PlayerState.MoveRight:
                    steelyState = PlayerState.MoveRight;
                    DrawSteelyWalking(SpriteEffects.None, sb);
                    break; 
                case PlayerState.JumpingLeft:
                    steelyState = PlayerState.JumpingLeft;
                    DrawSteelyJumping(SpriteEffects.FlipHorizontally, sb);
                    break;
                case PlayerState.JumpingRight:
                    steelyState = PlayerState.JumpingRight;
                    DrawSteelyJumping(SpriteEffects.None, sb);
                    break;

            }

            // Switches which way player is moving
            if (steelyState == PlayerState.FaceLeft || steelyState == PlayerState.MoveLeft)
            {
                steelyState = PlayerState.FaceLeft;
            }
            else if (steelyState == PlayerState.FaceRight || steelyState == PlayerState.MoveRight || (int)VelocityY == 0 && jumping == false)
            {
                steelyState = PlayerState.FaceRight;
            }
            else if ((int)VelocityY == 0 && jumping == false)
            {
                steelyState = PlayerState.FaceRight;
            }
            
        }

        public override void Update(GameTime gameTime)
        {

            // Positon is equal to the movment of velocity, 
            // Sets character position based on velocity and change in position
            charPosition += velocity;
            mcsteelyBody.Y = (int)charPosition.Y;
            mcsteelyBody.X = (int)charPosition.X;

            // Change in feet of character 
            playerFeet.X = (int)charPosition.X;
            playerFeet.Y = (int)charPosition.Y;

            // Gets state of keyboard
            currentKybState = Keyboard.GetState();

            // Detects if the player is currently jumping right and changes animation to show the change
            if (currentKybState.IsKeyDown(Keys.Space) && currentKybState.IsKeyDown(Keys.D) || ((int)VelocityY != 0) && currentKybState.IsKeyDown(Keys.D) && ((int)VelocityY != 1))
            {
                steelyState = PlayerState.JumpingRight;
                velocity.X = (velX);
            }

            // Detects if the player is currently jumping right and changes animation to show the change
            else if (currentKybState.IsKeyDown(Keys.Space) && currentKybState.IsKeyDown(Keys.A) || ((int)VelocityY != 0) && currentKybState.IsKeyDown(Keys.A) && ((int)VelocityY != 1))
            {
                steelyState = PlayerState.JumpingLeft;
                velocity.X = -(velX);
            }

            // Detects if the player is currently jumping right and changes animation to show the change
            else if (currentKybState.IsKeyDown(Keys.Space) || ((int)VelocityY != 0) && (VelocityX != 0) & ((int)VelocityY != 1))
            {
                steelyState = PlayerState.JumpingRight;
            }

            // Move right and set right state
            else if (currentKybState.IsKeyDown(Keys.D) && !currentKybState.IsKeyDown(Keys.Space))
            {
                velocity.X = (velX);
                //playerFeet.X += 7;
                steelyState = PlayerState.MoveRight;
            }
            // Move left and set left state
            else if (currentKybState.IsKeyDown(Keys.A) && !currentKybState.IsKeyDown(Keys.Space))
            {
                velocity.X = -(velX);
                //playerFeet.X -= 7;
                steelyState = PlayerState.MoveLeft;
            }

            // Do not move if not moving
            else
            {
                velocity.X = 0f;
                //playerFeet.X += 0;

            }

            // If player presses space, jump by increasing postion and velocity y, set has jumped to true 
            // to ensure that character does not jump more
            if (currentKybState.IsKeyDown(Keys.Space) && hasJumped == false && cannotJump == false)
            {
                charPosition.Y -= 10;
                playerFeet.Y -= 10;
                velocity.Y = -13.5f;
                //playerFeet.Y -= 10;
                hasJumped = true;
                cannotJump = true;
                //Currently here WILL BE CHANGED
                //steelyState = PlayerState.Jumping;
            }

            // Do not change y velocity if not jumping
            if (hasJumped == false)
            {
                jumping = false;
                velocity.Y = 0f;
                cannotJump = false;
                //playerFeet.Y = 0;
                
            }

            // Run Dash
            Dash();

            // Sets previous state of keyboard
            previousKybState = currentKybState;

            // Sets previous y Velocity
            previousYVel = VelocityY;

            // Updates all animations
            UpdateAnimation(gameTime);
            UpdateJumpingAnimation(gameTime);
            UpdateIdleAnimation(gameTime);
        }

        /// <summary>
        /// How a charcter can AIR dash currently, in both directions
        /// </summary>
        public void Dash()
        {
            if (currentKybState.IsKeyDown(Keys.LeftShift) && currentKybState.IsKeyDown(Keys.D))
            {
                velocity.X =+ 10;
                timeCounter += .01;
            }
            else if (currentKybState.IsKeyDown(Keys.LeftShift) && currentKybState.IsKeyDown(Keys.A))
            {
                velocity.X =-10;
                timeCounter += .01;
            }
        }

        public bool GrabTreasure(Treasure collectible)
        {
            return true;
        }

        /// <summary>
		/// Helper for updating McSteely's animation based on time
		/// </summary>
		/// <param name="gameTime">Info about time from MonoGame</param>
		private void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                mcSteelyCurrentFrame++;

                if (mcSteelyCurrentFrame >= 9)
                {
                    mcSteelyCurrentFrame = 0;
                }

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateIdleAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounterIdle += (((gameTime.ElapsedGameTime.TotalSeconds)) / 2);

            // Has enough time passed to flip to the next frame?
            if (timeCounterIdle > secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                mcSteelyIdleCurrentFrame++;

                if (mcSteelyIdleCurrentFrame >= 14)
                {
                    mcSteelyIdleCurrentFrame = 0;
                    //wdone = true;
                }

                // Reset the time counter
                timeCounterIdle -= secondsPerFrame;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateJumpingAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounterJumping += (((gameTime.ElapsedGameTime.TotalSeconds) / 4 + .0015));

            // Has enough time passed to flip to the next frame?
            if (timeCounterJumping > secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                mcSteelyJumpingCurrentFrame++;

                if (mcSteelyJumpingCurrentFrame >= 8)
                {
                    mcSteelyJumpingCurrentFrame = 0;
                    //wdone = true;
                }

                // Reset the time counter
                timeCounterJumping -= secondsPerFrame;
            }
        }

        /// <summary>
		/// Draws McSteely with a walking animation.
		/// </summary>
		/// <param name="flip">Should he be flipped horizontally or vertically?</param>
		private void DrawSteelyWalking(SpriteEffects flip, SpriteBatch sb)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            sb.Draw(
                playerTexture,                                  // Whole sprite sheet
                charPosition,                                   // Position of the McSteely sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    mcSteelyCurrentFrame * spriteWidth,         // - Left edge
                    0,                                          // - Top of sprite sheet
                    spriteWidth,                                // - Width 
                    playerTexture.Height),                      // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// Draws Mcsteely in a standing position. He is not animated yet.
        /// </summary>
        /// <param name="flip">Should he be flipped horizontally or vertically?</param>
        private void DrawSteelyStanding(SpriteEffects flip, SpriteBatch sb)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.
            sb.Draw(
                idleAsset,                                  // Whole sprite sheet
                charPosition,                                   // Position of the McSteely sprite
               new Rectangle(                                                 // Which portion of the sheet is drawn:
                    mcSteelyIdleCurrentFrame * idleSpriteWidth,         // - Left edge
                    0,                                                 // - Top of sprite sheet
                    idleSpriteWidth,                                // - Width 
                    idleAsset.Height),
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// Draws Mcsteely in a standing position. He is not animated yet.
        /// </summary>
        /// <param name="flip">Should he be flipped horizontally or vertically?</param>
        private void DrawSteelyJumping(SpriteEffects flip, SpriteBatch sb)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.
            sb.Draw(
                jumpingAsset,                                   // Whole sprite sheet
                charPosition,                                   // Position of the McSteely sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    mcSteelyJumpingCurrentFrame * jumpingSpriteWidth,         // - Left edge
                    0,                                          // - Top of sprite sheet
                    jumpingSpriteWidth,                                // - Width 
                    jumpingAsset.Height),                       // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        public override void Draw(GameTime gametime, SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }

        // DO NOT TOUCH
        /// <summary>
        /// Pulls player towards the ground if they're in the air
        /// </summary>
        public void SimulateGravity()
        {

            // If character has jumped, increase velocity
            if (gravity && PosY + mcsteelyBody.Height != 1000)
            {
                    hasJumped = true;
                    jumping = true;
                    float jump = 1;
                    velocity.Y += 0.50f * jump;
                    playerFeet.Y += (int)(0.50 * jump);
            }
        }

    }

}
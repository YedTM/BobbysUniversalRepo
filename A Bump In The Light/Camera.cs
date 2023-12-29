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
    /// This enum determines where the camera and light faces
    /// </summary>
    public enum CameraStates
    {
        FaceRight, 
        FaceDown, 
        FaceLeft
    }

    /// <summary>
    /// This class is used to create and draw the enemy camera.
    /// The camera is stationary and faces in different directions depending on
    /// the enum state. The enum state will change depending on how many frames 
    /// have passed. The camera also has the ability to deduct lives from the player
    /// if the player intersects with the cameras light.
    /// </summary>
    public class Camera : Enemy
    {
        //Field Section
        private CameraStates cameraState;

        private Texture2D assetDown;
        private Texture2D lightTextureDown;
        private Texture2D lightTextureSide;

        //Property Section
        /// <summary>
        /// Allows other classes to read the state of the Camera
        /// </summary>
        public CameraStates CameraState
        {
            get 
            { 
                return cameraState; 
            }
        }

        //Constructor Section
        public Camera(Texture2D asset, Texture2D assetDown, Rectangle position, 
                       int movement, Texture2D lightTexture, 
                       Texture2D lightTextureDown, Texture2D lightTextureSide,
                       LightStates lightState, Rectangle lightProperties, 
                       CameraStates cameraState)
                     : base(asset, position, movement, lightTexture, lightState, lightProperties)
        {
            this.cameraState = cameraState;
            this.assetDown = assetDown;

            this.lightTextureDown = lightTextureDown;
            this.lightTextureSide = lightTextureSide;
        }

        //Method Section
        /// <summary>
        /// Draws the camera and light textures facing right, left, and down 
        /// depending on the camera state.
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1</param>
        public override void Draw(SpriteBatch sb)
        {
            //Each state has a different light rotation that needs to be changed
            //each time the camera is drawn
            switch (cameraState)
            {
                case CameraStates.FaceDown:
                    sb.Draw(assetDown, Position, Color.White);
                    //When the camera faces down, the light must be rotated downwards and 
                    //shifted to the right to make up for the rotation
                    sb.Draw(lightTextureDown, new Rectangle(
                        (Position.X + (int)(lightProperties.Width * 0.42)), 
                        (Position.Y + lightProperties.Height + 
                        (int)(lightProperties.Height * 0.20)), 
                        lightProperties.Width, lightProperties.Height), 
                        null, Color.White, 0f, 
                        new Vector2(lightProperties.Width / 2f, lightProperties.Height / 2f),
                        SpriteEffects.None, 1);
                    break;
                case CameraStates.FaceLeft:
                    sb.Draw(gameAsset, Position, null, Color.White, 0, 
                        new Vector2(0, 0), SpriteEffects.FlipHorizontally, 1);
                    //When the camera faces to the left, the light must be flipped hortizontally
                    //and rotated to be facing towards the left. It is also shifted to the left
                    //and up to make up for the coordinate changes made by rotating the texture
                    sb.Draw(lightTextureSide, new Rectangle(
                        (Position.X + (int)(lightProperties.Width * 0.42)) - 36,
                        (Position.Y + lightProperties.Height + lightProperties.Height / 4) - 19,
                        (int)(lightProperties.Width * 1.25) , lightProperties.Height), null, 
                        Color.White, 0f, new Vector2(lightProperties.Width / 2f, 
                        lightProperties.Height / 2f), SpriteEffects.FlipHorizontally, 1);
                    break;
                case CameraStates.FaceRight:
                    //When the camera faces to the right, the light must be rotated to be facing
                    //towards the right. It is shifted up and to the right to make up for the 
                    //rotation changing the texture's coordinates
                    sb.Draw(gameAsset, Position, Color.White);
                    sb.Draw(lightTextureSide, new Rectangle(
                        (Position.X + (int)(lightProperties.Width * 0.42)) + 27,
                        (Position.Y + lightProperties.Height + lightProperties.Height / 4) - 20,
                        (int)(lightProperties.Width * 1.25), lightProperties.Height), null, 
                        Color.White, 0f, new Vector2(lightProperties.Width / 2f, 
                        lightProperties.Height / 2f), SpriteEffects.None, 1);
                    break;
            }

            
        }

        /// <summary>
        /// The camera and the light will face down left or right depending
        /// on the camera state. The state of the camera changes after a set
        /// amount of frames have gone by.
        /// </summary>
        /// <param name="gameTime">GameTime from Game1</param>
        /// <param name="gameState">The enum state Game1 is in</param>
        public override void Update(GameTime gameTime, GameState gameState)
        {
            //The camera should only update when the gamestate is in game
            //to avoid the cameras from moving when the game isn't running
            if (gameState == GameState.Game)
            {
                switch (cameraState)
                {
                    //The light is moved to fit directly under the camera asset
                    case CameraStates.FaceDown:
                        lightProperties.X = Position.X + 10;
                        lightProperties.Y = Position.Y + Position.Height;
                        //If enough frames pass to surpass the movement field, then
                        //the camera will face to the left
                        if (frameCounter < movement)
                        {
                            frameCounter++;
                        }
                        else
                        {
                            frameCounter = 0;
                            cameraState = CameraStates.FaceLeft;
                        }
                        break;
                    //The light is moved to be under slightly under the camera and
                    //to be to the left of the camera asset
                    case CameraStates.FaceLeft:
                        lightProperties.X = Position.X - Position.Width + 55;
                        lightProperties.Y = Position.Y + Position.Height - 10;
                        //If enough frames pass to surpass the movement field, then
                        //the camera will face to the right
                        if (frameCounter < movement)
                        {
                            frameCounter++;
                        }
                        else
                        {
                            frameCounter = 0;
                            cameraState = CameraStates.FaceRight;
                        }
                        break;
                    //The light is moved to be slightly under the camera asset
                    //and slightly to the right of it as well
                    case CameraStates.FaceRight:
                        lightProperties.X = Position.X + Position.Width - 25;
                        lightProperties.Y = Position.Y + Position.Height - 10;
                        //If enough frames pass to surpass the movement field, then
                        //the camera will face downwards
                        if (frameCounter < movement)
                        {
                            frameCounter++;
                        }
                        else
                        {
                            frameCounter = 0;
                            cameraState = CameraStates.FaceDown;
                        }
                        break;
                }

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
        /// Overrides a Draw method that cames from the component class,
        /// is not used for drawing the camera
        /// </summary>
        /// <param name="gametime">GameTime from Game1</param>
        /// <param name="sb">SpriteBatch from Game1</param>
        public override void Draw(GameTime gametime, SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }

    }
}

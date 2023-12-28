

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A_Bump_in_the_Light
{
    /// <summary>
    /// This enums determines if the light detects the player
    /// </summary>
    public enum LightStates
    {
        Light,
        Dark
    }

    /// <summary>
    /// This class as the framework for the Guard and Camera classes.
    /// It has fields for the light textures and properties of the lights, while
    /// also having fields to cause the future enum states to change in
    /// the child enemy classes. It also adds an Update method meant to 
    /// be overriden.
    /// </summary>
    public abstract class Enemy : GameObject
    {
        //Field Section
        protected int movement;
        protected int frameCounter;
        protected Texture2D lightTexture;
        protected Rectangle lightProperties;
        protected LightStates lightState;
        protected Stopwatch movementStopwatch;
        protected bool transitionStop;
        protected bool hasDetected;

        //Properties
        /// <summary>
        /// Allows other classes to read and alter the movement field
        /// </summary>
        public int Movement
        {
            get
            {
                return movement;
            }
            set
            {
                movement = value;
            }
        }

        /// <summary>
        /// Allows other classes to read and alter the frameCounter field
        /// </summary>
        public int FrameCounter
        {
            get
            {
                return frameCounter;
            }
            set
            {
                frameCounter = value;
            }

        }

        /// <summary>
        /// Allows other classes to read the lightProperties field
        /// </summary>
        public Rectangle LightProperties
        {
            get
            {
                return lightProperties;
            }
        }

        /// <summary>
        /// Allows other classes to read the lightState field
        /// </summary>
        public LightStates LightState
        {
            get
            {
                return lightState;
            }
        }

        /// <summary>
        /// Allows other classes to read and set the hasDetected field
        /// </summary>
        public bool HasDetected
        {
            get
            {
                return hasDetected;
            }
            set
            {
                hasDetected = value;
            }
        }

        //Constructor
        public Enemy(Texture2D asset, Rectangle position, int movement, Texture2D lightTexture,
                     LightStates lightState, Rectangle lightProperties) : base(asset, position)
        {
            this.movement = movement;
            frameCounter = 0;
            this.lightTexture = lightTexture;
            this.lightState = lightState;
            this.lightProperties = lightProperties;
            transitionStop = false;
            
        }

        //Method Section
        /// <summary>
        /// A Draw method meant to be overriden by the child classes
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1</param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(gameAsset, Position, Color.White);
        }

        /// <summary>
        /// An Update method meant to be overriden by the child classes
        /// </summary>
        /// <param name="gameTime">GameTime from Game1</param>
        /// <param name="gameState">GameTime from Game1</param>
        public abstract void Update(GameTime gameTime, GameState gameState);

        /// <summary>
        /// An Update method meant to be overriden by the child classes
        /// </summary>
        /// <param name="gameTime">GameTime from Game1</param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// A Detection method meant to be overriden by the child classes
        /// </summary>
        /// <param name="thief">The player</param>
        /// <returns>True if detected</returns>
        public abstract bool Detection(Player thief);


    }
}

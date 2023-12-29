using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace A_Bump_in_the_Light
{
    //Delegate
    public delegate void OnButtonClickDelegate();

    /// <summary>
    /// This class is gonna make buttons
    /// We will use those buttons to make stuff happen in menus
    /// Buttons are cool
    /// </summary>
    public class ButtonClass 
    {
        //Variables
        private Rectangle rect;
        private Texture2D buttonImg;
        public event OnButtonClickDelegate OnLeftButtonClick;
        private MouseState prevMState;

        //Properties

        //Constructors
        /// <summary>
        /// Makes a button with a defined image and location
        /// </summary>
        /// <param name="buttonImg"> image </param>
        /// <param name="rect"> hitbox and location </param>
        public ButtonClass(Texture2D buttonImg, Rectangle rect)
        {
            this.rect = rect;
            this.buttonImg = buttonImg;
        }

        //Functions
        /// <summary>
        /// Checks to see if button has been clicked
        /// </summary>
        public void Update()
        {
            MouseState mState = Mouse.GetState();

            // Left button click
            if (mState.LeftButton == ButtonState.Released &&
                prevMState.LeftButton == ButtonState.Pressed &&
                rect.Contains(mState.Position))
            {
                if (OnLeftButtonClick != null)
                {
                    //Call all methods attached to this button
                    OnLeftButtonClick();
                }
            }
            prevMState = mState;
        }

        /// <summary>
        /// Draw the button
        /// </summary>
        /// <param name="spriteBatch">used to draw the button</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the button itself
            spriteBatch.Draw(buttonImg, rect, Color.White);
        }
    }
}

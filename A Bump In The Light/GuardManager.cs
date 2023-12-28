using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace A_Bump_in_the_Light
{
    /// <summary>
    /// This acts as the manager class of the guards as it reads in and
    /// builds multiple guards from a text file
    /// </summary>
    public class GuardManager
    {
        //Field Section
        private List<Guard> guards;

        //Constructor Sectiom
        public GuardManager()
        {
            guards = new List<Guard>();
        }

        //Method Section
        /// <summary>
        /// This method will read in the GuardInfo text file and create a list
        /// of guards with the information found within it.
        /// </summary>
        /// <param name="guardFile">The text file being read</param>
        /// <param name="guardTexture">The universal texture of each guard</param>
        /// <param name="lightTexture">The universal light texture of each guard</param>
        public void LoadGuardData(string guardFile, Texture2D guardTexture, 
                                  Texture2D lightTexture)
        {
            string[] splitText;
            string lineFromFile = "";
            Guard loadedGuard;
            StreamReader reader = null;
            LightStates lightState = LightStates.Light;
            GuardStates guardState = GuardStates.MoveRight;

            
            try
            {
                reader = new StreamReader(guardFile);

                //Each line in the GuardInfo text file is read until it reaches the end
                //of the file
                while (((lineFromFile = reader.ReadLine()) != null))
                {
                    //Each line is then split to receive the proper information
                    splitText = lineFromFile.Split(',');

                    if (splitText[3] == "Light")
                    {
                        lightState = LightStates.Light;
                    }
                    else if (splitText[3] == "Dark")
                    {
                        lightState = LightStates.Dark;
                    }

                    //0 = guard X, 1 = guard Y, 2 = movement, 3 = LightState, 4 = light X
                    //5 = light Y
                    //The information found in the file is then used to create a new guard
                    loadedGuard = new Guard(guardTexture, 
                                            new Rectangle(
                                                int.Parse(splitText[0]), 
                                                int.Parse(splitText[1]), 
                                                guardTexture.Width, 
                                                guardTexture.Height), 
                                            int.Parse(splitText[2]), lightTexture, lightState, 
                                            new Rectangle(
                                                int.Parse(splitText[4]), 
                                                int.Parse(splitText[5]), 
                                                (int)(lightTexture.Width * 0.7), 
                                                lightTexture.Height), 
                                            guardState);
                    //Which is then added to the guard list
                    guards.Add(loadedGuard);
                }

            }
            catch (Exception fileError)
            {
                Console.WriteLine(fileError.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// This method runs the Draw method of each guard in the guard list
        /// </summary>
        /// <param name="sb">Spritebatch from Game1</param>
        public void Draw(SpriteBatch sb)
        {
            foreach (Guard g in guards)
            {
                g.Draw(sb);
            }
        }

        /// <summary>
        /// This method runs the Update method of each guard in the guard list
        /// </summary>
        /// <param name="gameTime">GameTime from Game1</param>
        /// <param name="gameState">The current enum state of the game</param>
        public void Update(GameTime gameTime, GameState gameState)
        {
            foreach (Guard g in guards)
            {
                g.Update(gameTime, gameState);
            }
        }

        /// <summary>
        /// This method checks if the guards in the guard list have detected the
        /// player or not and will take lives from the player if the guards have
        /// detected the player.
        /// </summary>
        /// <param name="thief">The player</param>
        public void Detection(Player thief)
        {
            foreach (Guard g in guards)
            {
                if (g.Detection(thief))
                {
                    if (g.HasDetected == false)
                    {
                        thief.Lives--;
                        g.HasDetected = true;
                    }
                }
                else
                {
                    g.HasDetected = false;
                }
                
            }
        }



    }
}

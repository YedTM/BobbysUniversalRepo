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
    /// This acts as the manager class of the cameras as it reads in and
    /// builds multiple guards from a text file
    /// </summary>
    internal class CameraManager
    {
        //Field Section
        private List<Camera> cameras;

        //Constructor Sectiom
        public CameraManager()
        {
            cameras = new List<Camera>();

        }

        //Method Section
        /// <summary>
        /// This method will read in the CameraInfo text file and creates a list
        /// of cameras with the information found within it.
        /// </summary>
        /// <param name="cameraFile">The text file being read</param>
        /// <param name="cameraTexture">The universal texture used by all cameras</param>
        /// <param name="cameraDown">The downward facing texture of all cameras</param>
        /// <param name="lightTexture">The universal light texture used 
        /// by all camera lights</param>
        /// <param name="lightTextureDown">The downward facing light texture 
        /// used by all camera lights</param>
        /// <param name="lightTextureSide">The side facing light texture 
        /// used by all camera lights</param>
        public void LoadCameraData(string cameraFile, Texture2D cameraTexture, 
                                   Texture2D cameraDown, Texture2D lightTexture, 
                                   Texture2D lightTextureDown, Texture2D lightTextureSide)
        {
            string[] splitText;
            string lineFromFile = "";
            Camera loadedCamera;
            StreamReader reader = null;
            LightStates lightState = LightStates.Light;
            CameraStates cameraState = CameraStates.FaceRight;

            try
            {
                reader = new StreamReader(cameraFile);

                //This will read each line of the CameraInfo text file until it reaches
                //the end of the file and can no longer read any more lines
                while (((lineFromFile = reader.ReadLine()) != null))
                {
                    //The information found in the file is then split into seperate parts
                    //in order to use them in the creation of new cameras
                    splitText = lineFromFile.Split(',');

                    if (splitText[3] == "Light")
                    {
                        lightState = LightStates.Light;
                    }
                    else if (splitText[3] == "Dark")
                    {
                        lightState = LightStates.Dark;
                    }

                    //0 = camera X, 1 = camera Y, 2 = movement, 3 = LightState, 4 = light X
                    //5 = light Y
                    //Each piece of information found in the file is then used to create
                    //a new camera
                    loadedCamera = new Camera(cameraTexture,
                                              cameraDown,
                                            new Rectangle(
                                                int.Parse(splitText[0]),
                                                int.Parse(splitText[1]),
                                                (int)(cameraTexture.Width * 0.45),
                                                (int)(cameraTexture.Height * 0.45)),
                                            int.Parse(splitText[2]), lightTexture, 
                                            lightTextureDown, lightTextureSide, lightState,
                                            new Rectangle(
                                                int.Parse(splitText[4]),
                                                int.Parse(splitText[5]),
                                                (int)(lightTexture.Width * 0.41),
                                                (int)(lightTexture.Height * 1.15)
                                                ),
                                            cameraState);
                    //Which is then added to the camera list
                    cameras.Add(loadedCamera);
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
        /// This method runs the Draw method of each camera in the camera list
        /// </summary>
        /// <param name="sb">Spritebatch from Game1</param>
        public void Draw(SpriteBatch sb)
        {
            foreach (Camera c in cameras)
            {
                c.Draw(sb);
            }
        }

        /// <summary>
        /// This method runs the Update method of each camera in the camera list
        /// </summary>
        /// <param name="gameTime">GameTime from Game1</param>
        /// <param name="gameState">The current enum state of the game</param>
        public void Update(GameTime gameTime, GameState gameState)
        {
            foreach (Camera c in cameras)
            {
                c.Update(gameTime, gameState);
            }
        }

        /// <summary>
        /// This method checks if the cameras in the camera list have detected the
        /// player or not and will take lives from the player if the cameras have
        /// detected the player.
        /// </summary>
        /// <param name="thief">The player</param>
        public void Detection(Player thief)
        {
            foreach (Camera c in cameras)
            {
                if (c.Detection(thief))
                {
                    if (c.HasDetected == false)
                    {
                        thief.Lives--;
                        c.HasDetected = true;
                    }
                }
                else
                {
                    c.HasDetected = false;
                }

            }
        }


    }
}

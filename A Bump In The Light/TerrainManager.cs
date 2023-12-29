using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bump_in_the_Light
{
    internal class TerrainManager
    {
        //Field Section
        private List<Terrain> platforms;

        //Constructor Sectiom
        public TerrainManager()
        {
            platforms = new List<Terrain>();


        }

        //Method Section
        /// <summary>
        /// This method takes in a text file and reads the information within it
        /// to create a list of terrain objects, which are then printed to the
        /// game window
        /// </summary>
        /// <param name="platformFile">The text file being read from</param>
        /// <param name="terrainTexture">The texture of the terrain</param>
        public void LoadTerrainData(string platformFile, Texture2D terrainTexture)
        {
            string[] splitText;
            string lineFromFile = "";
            Terrain loadedTerrain;
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(platformFile);

                //Each line from the file is read until there is none left
                //to read
                while (((lineFromFile = reader.ReadLine()) != null))
                {
                    //The information is then split in order to properly
                    //organize and place the date found within the lines
                    splitText = lineFromFile.Split(',');

                    //0 = X, 1 = Y, 2 = width, 3 = height
                    //The terrain is then created
                    loadedTerrain = new Terrain(terrainTexture,
                                            new Rectangle(
                                                int.Parse(splitText[0]),
                                                int.Parse(splitText[1]),
                                                int.Parse(splitText[2]),
                                                int.Parse(splitText[3])));
                    //and then added to the terrain list
                    platforms.Add(loadedTerrain);
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
        /// This draws each terrain in the terrain list
        /// </summary>
        /// <param name="sb">SpriteBatch from Game1</param>
        public void Draw(SpriteBatch sb)
        {
            foreach (Terrain p in platforms)
            {
                p.Draw(sb);
            }
        }

        /// <summary>
        /// This runs the IsSteppedOn method for each terrain in
        /// the terrain list
        /// </summary>
        /// <param name="mcStealy">The player character</param>
        public void Update(Player mcStealy)
        {
            foreach (Terrain p in platforms)
            {
                p.IsSteppedOn(mcStealy);
            }
        }
    }
}

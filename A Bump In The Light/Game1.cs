using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace A_Bump_in_the_Light
{
    /// <summary>
    /// Will keep track of what "State" the game is in
    /// </summary>
    public enum GameState
    {
        MainMenu,
        Settings,
        Game,
        Pause,
        GameOver,
        PitGameOver,
        WinScreen
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private PlayerCamera playerCamera;

        private List<Component> components;

        private Player mcStealy;
        private GuardManager setOfGuards;
        private CameraManager setOfCameras;
        private TerrainManager terrainSet;
        GameState gameState;
        KeyboardState kb;
        KeyboardState pkb;
        private bool erinMode;

        public MouseState currentMouse;
        public int xPos;
        public int yPos;

        public Texture2D gameButtonTexture;
        public Texture2D playerFeetTexture;
        public Texture2D guardTexture;
        public Texture2D guardLightTexture;
        public Texture2D cameraTexture;
        public Texture2D cameraTextureDown;
        public Texture2D LightSide;
        public Texture2D LightDown;
        public Texture2D quitButtonTexture;
        public Texture2D gameOverTexture;
        public Texture2D idleAnimation;
        public Rectangle playerFeetRect;
        public ButtonClass startButton;
        public ButtonClass quitButton;
        public Texture2D terrainTexture;
        public Terrain terrain;
        public Terrain terrain2;
        public SpriteFont arial30;
        public Treasure treasure;
        public Texture2D treasureTexture;
        public Rectangle treasureRectangle;
        public Rectangle winGate;
        public Texture2D winGateTexture;
        public Texture2D mainMenuArt;
        public Texture2D background;
        public Texture2D winScreen;
        public Texture2D window;
        public Texture2D painting;
        public Texture2D pitGameOverTexture;

        public static int screenHeight;

        public static int screenWidth;

        // Escape Music
        public Song escapeMusic;
        public Song escapeMusic2;

        public Random songRng;

        public Song mainMusic;

        public Song victoryMusic1;
        public Song victoryMusic2;

        public bool victoryMusicPlaying;

        public bool playOnce;

        public bool escapePlaying;

        // The time that the timer was started
        public TimeSpan startTime;
        // The time elapsed since the timer was started
        public TimeSpan elapsedTime;

        public int countDownTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            screenHeight = _graphics.PreferredBackBufferWidth;
            screenWidth = _graphics.PreferredBackBufferWidth;

            gameState = GameState.MainMenu;
            setOfGuards = new GuardManager();
            setOfCameras = new CameraManager();
            terrainSet = new TerrainManager();
            // Set the timer start time to the current time
            startTime = TimeSpan.Zero;

            countDownTime = 60 * 55;

            treasureRectangle = new Rectangle(10200, 950, 50, 50);

            playOnce = false;

            victoryMusicPlaying = false;

            songRng = new Random();

            erinMode = false;
            escapePlaying = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerCamera = new PlayerCamera();

            playerFeetTexture = Content.Load<Texture2D>("playerFeetPlaytestImage");

            mcStealy = new Player(
                Content.Load<Texture2D>("McSteelyBandana (2)"), 
                Content.Load<Texture2D>("Jumping_Animation"),
                Content.Load<Texture2D>("CharacterMock-Idle"),
                new Vector2(-50, -50),
                5, 
                false, 
                20, 
                PlayerState.MoveRight, 
                playerFeetRect);

            gameButtonTexture = Content.Load<Texture2D>("finalPlayButton");
            quitButtonTexture = Content.Load<Texture2D>("finalQuitButton");
            background = Content.Load<Texture2D>("TestBackgroundImg");
            terrainTexture = Content.Load<Texture2D>("platformRemake2");
            guardTexture = Content.Load<Texture2D>("GaurdWalking (1)");
            guardLightTexture = Content.Load<Texture2D>("NewGuardLight");
            cameraTexture = Content.Load<Texture2D>("CameraFacingWithLight");
            cameraTextureDown = Content.Load<Texture2D>("CameraDownWithLight");
            LightDown = Content.Load<Texture2D>("DownLight3.0");
            LightSide = Content.Load<Texture2D>("SideLight");
            arial30 = Content.Load<SpriteFont>("arial30");
            treasureTexture = Content.Load<Texture2D>("finalTreasure");
            winGateTexture = Content.Load<Texture2D>("ExitDoor");
            mainMenuArt = Content.Load<Texture2D>("finalMainScreenFixed");
            setOfGuards.LoadGuardData("../../../GuardInfo.txt", guardTexture, guardLightTexture);
            setOfCameras.LoadCameraData("../../../CameraInfo.txt", cameraTexture, cameraTextureDown, LightDown, LightDown, LightSide);
            terrainSet.LoadTerrainData("../../../TerrainInfo.txt",terrainTexture);
            gameOverTexture = Content.Load<Texture2D>("GameOverScreen");
            winScreen = Content.Load<Texture2D>("McWinScreen");
            window = Content.Load<Texture2D>("Window");
            painting = Content.Load<Texture2D>("FancyPainting");
            pitGameOverTexture = Content.Load<Texture2D>("pitGameOverTexture");

            escapeMusic2 = Content.Load<Song>("RazorMind");

            escapeMusic = Content.Load<Song>("06 It's Pizza Time!");

            mainMusic = Content.Load<Song>("WaitStealth");

            victoryMusic1 = Content.Load<Song>("Victory1.0");

            victoryMusic2 = Content.Load<Song>("Victory2.0");

            // New rectangle for the player's feet at X = 0, Y = 0, and the dimensions of the texture file      
            playerFeetRect = new Rectangle(mcStealy.PlayerFeet.X, mcStealy.PlayerFeet.Y, 800, 800);

            // New terrain using the terrainTexture and a rectangle at X = 30, Y = 5, and dimensions
            // equal to the width of the texture and 1.
            //terrain = new Terrain(terrainTexture, new Rectangle(980, 970, 400, 51));
            //terrain2 = new Terrain(terrainTexture, new Rectangle(700, 450, 400, 51));
            

            startButton = new ButtonClass
                (
                    gameButtonTexture,
                    new Rectangle(600,500,700,500)
                );

            quitButton = new ButtonClass
                (
                    quitButtonTexture,
                    new Rectangle(600, 200, 600, 500)
                );

            treasure = new Treasure(treasureTexture, treasureRectangle);


            winGate = new Rectangle(0 - mcStealy.PlayerTexture.Width, 0, 1, _graphics.PreferredBackBufferHeight);

            components = new List<Component>()
            {
                mcStealy,
                
               
            };


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            kb = Keyboard.GetState();

            

            //FSM to represent which state the game is in. Switches state based on actions taken by user 
            switch (gameState)
            {
                case GameState.MainMenu:

                    // Resets escape music and move speed to normal values
                    MediaPlayer.Stop();
                    MediaPlayer.IsRepeating = true;
                    escapePlaying = false;
                    mcStealy.VelX = 7;
                    victoryMusicPlaying = false;

                    startButton.OnLeftButtonClick += StartGame;
                    startButton.Update();
                    mcStealy.Lives = 5;
                    treasure.HoldingTreasure = false;
                    mcStealy.HasJumped = true;

                    setOfGuards.Update(gameTime, gameState);
                    setOfCameras.Update(gameTime, gameState);
                    terrainSet.Update(mcStealy);
                    escapePlaying = false;

                    countDownTime = 60 * 55;

                    gameTime.TotalGameTime = TimeSpan.Zero;

                    elapsedTime = TimeSpan.Zero;
                    countDownTime = 60 * 55;
                    playOnce = false;
                    break;


                case GameState.Settings:

                    break;

                case GameState.Game:

                    currentMouse = Mouse.GetState();
                    xPos = currentMouse.X;
                    yPos = currentMouse.Y;

                    /*
                    foreach (Component component in components)
                    {
                        component.Update(gameTime);
                    }

                    playerCamera.Follow(mcStealy);
                    */

                    
                    mcStealy.Update(gameTime);

                    mcStealy.SimulateGravity();

                    if (erinMode)
                    {
                        mcStealy.VelocityY = 0;


                        mcStealy.McSteelyBodyY = 950;
                        mcStealy.Gravity = false;
                        mcStealy.HasJumped = true;
                    }

                    setOfGuards.Update(gameTime, gameState);
                    if (erinMode == false)
                    {
                        setOfGuards.Detection(mcStealy);
                    }
                    setOfCameras.Update(gameTime, gameState);
                    if (erinMode == false)
                    {
                        setOfCameras.Detection(mcStealy);
                    }
                    
                    if (erinMode == false)
                    {
                        terrainSet.Update(mcStealy);
                    }
                    

                    if (kb.IsKeyDown(Keys.E) && pkb.IsKeyDown(Keys.LeftControl))
                    {
                        erinMode = true;
                    }

                    if (kb.IsKeyDown(Keys.F) && pkb.IsKeyDown(Keys.LeftControl))
                    {
                        erinMode = false;
                    }

                    if (kb.IsKeyDown(Keys.Enter) && pkb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.Pause;
                    }

                    if (mcStealy.McSteelyBodyY == 2000)
                    {
                        mcStealy.Lives = 0;
                    }

                    if(mcStealy.Lives == 0)
                    {
                        gameState = GameState.GameOver;
                    }
                    if (mcStealy.McsteelyBody.Y > 2000)
                    {
                        gameState = GameState.PitGameOver;
                    }

                    treasure.AcquireTreasure(mcStealy);

                    if (treasure.HoldingTreasure == true && mcStealy.McsteelyBody.Intersects(winGate))
                    {
                        gameState = GameState.WinScreen;
                    }

                    if (escapePlaying == false && !playOnce)
                    {
                        MediaPlayer.Play(mainMusic);
                        MediaPlayer.IsRepeating = true;
                        playOnce = true;
                    }

                    if (escapePlaying == false && treasure.HoldingTreasure)
                    {
                        if (songRng.NextInt64(0, 100) > 50)
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.Play(escapeMusic2);
                            MediaPlayer.IsRepeating = true;
                            escapePlaying = true;
                            mcStealy.VelX = 7;
                        }
                        else
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.Play(escapeMusic);
                            MediaPlayer.IsRepeating = true;
                            escapePlaying = true;
                            mcStealy.VelX = 7;
                        }
                    }
                   

                    //terrain.IsSteppedOn(mcStealy);
                    //terrain2.IsSteppedOn(mcStealy);

                    // Update the elapsed time since the timer was started


                    if (!treasure.HoldingTreasure)
                    {
                        elapsedTime = gameTime.TotalGameTime;
                    }

                    else
                    {
                        countDownTime--;
                    }

                    if (countDownTime < 0)
                    {
                        gameState = GameState.GameOver;
                        elapsedTime = TimeSpan.Zero;
                        gameTime.TotalGameTime = TimeSpan.Zero;
                    }
                    break;

                case GameState.Pause:
                    startButton.OnLeftButtonClick += Unpause;
                    quitButton.OnLeftButtonClick += ReturnTitle;
                    startButton.Update();
                    quitButton.Update();

                    setOfGuards.Update(gameTime, gameState);
                    setOfCameras.Update(gameTime, gameState);
                    terrainSet.Update(mcStealy);

                    break;

                case GameState.GameOver:

                    if (kb.IsKeyDown(Keys.Enter) && pkb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.MainMenu;
                    }
                    escapePlaying = false;

                    if (escapePlaying == false && !treasure.HoldingTreasure)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.IsRepeating = true;
                        escapePlaying = false;
                        mcStealy.VelX = 7;
                    }

                    elapsedTime = TimeSpan.Zero;
                    countDownTime = 60 * 55;
                    gameTime.TotalGameTime = TimeSpan.Zero;
                    playOnce = false;

                    break;

                case GameState.PitGameOver:

                    if (kb.IsKeyDown(Keys.Enter) && pkb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.MainMenu;
                    }

                    escapePlaying = false;

                    if (escapePlaying == false && !treasure.HoldingTreasure)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.IsRepeating = true;
                        escapePlaying = false;
                        mcStealy.VelX = 7;
                    }

                    elapsedTime = TimeSpan.Zero;
                    countDownTime = 60 * 55;
                    gameTime.TotalGameTime = TimeSpan.Zero;
                    playOnce = false;

                    break;

                case GameState.WinScreen:

                    if (!victoryMusicPlaying)
                    {
                        MediaPlayer.Stop();
                    }

                    if (victoryMusicPlaying == false)
                    { 
                        if (songRng.NextInt64(0, 100) > 50)
                        {
                            MediaPlayer.Play(victoryMusic1);
                            MediaPlayer.IsRepeating = true;

                        }
                        else
                        {
                            MediaPlayer.Play(victoryMusic2);
                            MediaPlayer.IsRepeating = true;
                        }
                    }
                    

                    victoryMusicPlaying = true;
                    escapePlaying = false;
                    playOnce = false;
                    if (kb.IsKeyDown(Keys.Enter) && pkb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.MainMenu;
                    }
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            pkb = kb;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            

            switch (gameState)
            {
                case GameState.MainMenu:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        mainMenuArt,
                        new Vector2(0, 0),
                        Color.White
                        );
                    startButton.Draw(_spriteBatch);
                    //_spriteBatch.DrawString(arial30, "Press 'ctrl' key to toggle debug mode", new Vector2(1250,950), Color.White);
                    _spriteBatch.End();
                    break;

                case GameState.Settings:

                    break;

                case GameState.Game:

                    // All bakcground art is drawn //
                    _spriteBatch.Begin(transformMatrix: playerCamera.Transform);

                    _spriteBatch.Draw(
                        window,
                        new Rectangle(200,200, window.Width / 2, window.Height / 2 ),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        painting,
                        new Rectangle(700, 400, painting.Width / 2, painting.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(1200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(3200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(4200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(5200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(6200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        painting,
                        new Rectangle(6800, 300, painting.Width / 2, painting.Height / 2),
                        Color.Gray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(7200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(8200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(9200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );
                    _spriteBatch.Draw(
                        window,
                        new Rectangle(10200, 200, window.Width / 2, window.Height / 2),
                        Color.DimGray
                        );

                    _spriteBatch.End();


                    _spriteBatch.Begin(transformMatrix: playerCamera.Transform);

                    _spriteBatch.Draw(
                        winGateTexture,
                        new Rectangle(-500, 878, winGateTexture.Width, winGateTexture.Height),
                        Color.White
                        );

                    // Draws game to the game window
                    //terrain.Draw(_spriteBatch);

                    //terrain2.Draw(_spriteBatch);
                    terrainSet.Draw(_spriteBatch);

                    if (treasure.HoldingTreasure == false)
                    {
                        treasure.Draw(_spriteBatch);
                    }

                    foreach (Component component in components)
                    {
                        component.Draw(gameTime, _spriteBatch);
                    }

                    playerCamera.Follow(mcStealy);

                    

                    //if (interactionSpace != null)
                    //{
                    //    _spriteBatch.Draw(
                    //        terrainTexture,
                    //        interactionSpace,
                    //        Color.Red
                    //        );
                    //}


                    mcStealy.Draw(_spriteBatch);

                    //Debug.WriteLine(mcStealy.PlayerFeetY);
                    //Debug.WriteLine(mcStealy.PlayerFeetX);

                    setOfGuards.Draw(_spriteBatch);
                    setOfCameras.Draw(_spriteBatch);
                    _spriteBatch.End();

                    _spriteBatch.Begin();

                    _spriteBatch.DrawString(arial30, $"LIVES: {mcStealy.Lives}", new Vector2(0, 0), Color.White);
                    


                    string timeText = String.Format("{0:00}:{1:00}:{2:000}", elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);

                    // Display the elapsed time in the top right corner of the window

                    if (!treasure.HoldingTreasure)
                    {
                        _spriteBatch.DrawString(arial30, timeText,
                       new Vector2(Window.ClientBounds.Width - arial30.MeasureString(elapsedTime.ToString()).X + 100, 0),
                       Color.White);
                    }
                    else
                    {
                        _spriteBatch.DrawString(arial30, "ALARM TRIGGERED, ESCAPE!: " + (countDownTime / 50).ToString(),
                       new Vector2(Window.ClientBounds.Width - arial30.MeasureString(elapsedTime.ToString()).X - 400, 0),
                       Color.Red);
                    }


                    if (treasure.HoldingTreasure == true)
                    {
                        _spriteBatch.Draw(
                            treasure.Texture,
                            new Vector2(Window.ClientBounds.Width - 200, treasure.Texture.Width),
                            Color.White
                            );
                    }

                    _spriteBatch.End();

                    break;

                case GameState.Pause:
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString
                        (
                            arial30,
                            "GAME PAUSED",
                            new Vector2(750, 200),
                            Color.White
                        );
                    startButton.Draw(_spriteBatch);
                    quitButton.Draw(_spriteBatch);

                    _spriteBatch.End();
                    break;

                case GameState.GameOver:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(gameOverTexture, new Vector2(0, 0), Color.White);
                    _spriteBatch.End();
                    break;

                case GameState.PitGameOver:

                    _spriteBatch.Begin();
                    _spriteBatch.Draw(pitGameOverTexture, new Vector2(0, 0), Color.White);
                    _spriteBatch.End();

                    break;

                case GameState.WinScreen:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        winScreen,
                        new Vector2(0, 0),
                        Color.White
                        );
                    _spriteBatch.End();
                    break;
            }

            

            

            base.Draw(gameTime);
        }

        //Used for starting game for first time
        public void StartGame()
        {
            gameState = GameState.Game;
            mcStealy.PosX = 50;
            mcStealy.PosY = 50;
            startButton.OnLeftButtonClick -= StartGame;
        }

        //Used for unpausing
        public void Unpause()
        {
            gameState = GameState.Game;
            startButton.OnLeftButtonClick -= Unpause;
            quitButton.OnLeftButtonClick -= ReturnTitle;
        }

        public void ReturnTitle()
        {
            gameState = GameState.MainMenu;
            startButton.OnLeftButtonClick -= Unpause;
            quitButton.OnLeftButtonClick -= ReturnTitle;
        }
    }
}
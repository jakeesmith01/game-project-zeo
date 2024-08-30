using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gameproj1;

public class Game1 : Game
{
    //GraphicsDeviceManager for the game
    private GraphicsDeviceManager _graphics;

    //SpriteBatch for drawing
    private SpriteBatch _spriteBatch;

    //SpriteFont for game text
    private SpriteFont baskerville;

    //The background texture for the game
    private Texture2D background;

    //The size of the title screen text
    private Vector2 titleTextSize;

    //The size of the exit instructions text
    private Vector2 exitTextSize;

    //The zombie sprite
    private ZombieSprite zombie;

    //The man sprite
    private ManSprite man;

    //The car sprite
    private CarSprite car;

    //The woman sprite
    private WomanSprite woman;



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// </summary>
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        zombie = new ZombieSprite();
        man = new ManSprite();
        car = new CarSprite();
        woman = new WomanSprite();
        

        base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load textures 
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        zombie.LoadContent(Content);
        man.LoadContent(Content);
        car.LoadContent(Content);
        woman.LoadContent(Content);


        background = Content.Load<Texture2D>("postapocalypse1");
        baskerville = Content.Load<SpriteFont>("Baskervville-SC");
        
        titleTextSize = baskerville.MeasureString("Survive The Night");
        exitTextSize = baskerville.MeasureString("Want to quit? Hit 'esc' or 'q'! (B on GamePad)");
        exitTextSize *= 0.5f;       


        // TODO: use this.Content to load your game content here
    }

    /// <summary>
    /// Updates the game based on the gameTime
    /// </summary>
    /// <param name="gameTime">The game time of the game</param>
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Q))
            Exit();

        // TODO: Add your update logic here

        zombie.Update(gameTime);

        if(zombie.Position.X - 120 <= man.Position.X - 45 && !zombie.IsDead){
            zombie.KillZombie();
            man.Attack();
        }

        man.Update(gameTime);

        base.Update(gameTime);
    }

    /// <summary>
    /// Draws the different game elements to the screen
    /// </summary>
    /// <param name="gameTime">The current game time</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkOliveGreen);


        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), Color.White);
        _spriteBatch.DrawString(
            baskerville, 
            "Survive The Night", 
            new Vector2(
                (_graphics.GraphicsDevice.Viewport.Width - titleTextSize.X) / 2,
                (_graphics.GraphicsDevice.Viewport.Height - titleTextSize.Y) / 4),
            Color.White
        );

        _spriteBatch.DrawString(
            baskerville,
            "Want to quit? Hit 'esc' or 'q'! (B on GamePad)",
            new Vector2(
                (_graphics.GraphicsDevice.Viewport.Width - exitTextSize.X) / 2,
                (_graphics.GraphicsDevice.Viewport.Height - exitTextSize.Y) / 1.75f),
            Color.White,
            0f,
            Vector2.Zero,
            0.5f,
            SpriteEffects.None,
            0
        );


        car.Draw(gameTime, _spriteBatch);
        zombie.Draw(gameTime, _spriteBatch);
        man.Draw(gameTime, _spriteBatch);
        woman.Draw(gameTime, _spriteBatch);

        

        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}

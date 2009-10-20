using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Xadrez
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_graphics;

        SpriteBatch m_spriteBatch;

        public Rectangle m_rectTable;

        Table m_gameTable;

        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            m_rectTable.X = 0;
            m_rectTable.Y = 0;
            m_rectTable.Width = 640;
            m_rectTable.Height = 640;

            m_graphics.PreferredBackBufferHeight = 640;
            m_graphics.PreferredBackBufferWidth = 640;
            m_graphics.ApplyChanges();


            m_gameTable = new Table();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading table
            Table.SetSelfImage(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/table.png"));

            //Loading white pieces
            Pawn.SetSelfImageWhite(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white pawn.png"));
            Knight.SetSelfImageWhite(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white knight.png"));
            Bishop.SetSelfImageWhite(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white bishop.png"));
            Rook.SetSelfImageWhite(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white rook.png"));
            King.SetSelfImageWhite(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white king.png"));
            Queen.SetSelfImageWhite(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white queen.png"));

            //Loading black pieces
            Pawn.SetSelfImageBlack(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/brown pawn.png"));
            Knight.SetSelfImageBlack(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/brown knight.png"));
            Bishop.SetSelfImageBlack(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/brown bishop.png"));
            Rook.SetSelfImageBlack(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/brown rook.png"));
            King.SetSelfImageBlack(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/brown king.png"));
            Queen.SetSelfImageBlack(Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/brown queen.png"));


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin();

            m_spriteBatch.Draw(Table.GetSelfImage(), m_rectTable, Color.Beige);
            
            //Rectangle rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(0,6).BoundingBox.X,
            //             (int) m_gameTable.getTableSquare(0,6).BoundingBox.Y,
            //              71,
            //              70);

            Rectangle rectTestPiece = m_gameTable.getTableSquare(0, 6).BoundingBox;
            
            m_spriteBatch.Draw(King.getSelfImageBlack(),
                                m_gameTable.getTableSquare(0, 6).BoundingBox,
                                Color.Beige);

            rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(0,7).BoundingBox.X,
             (int)m_gameTable.getTableSquare(0,7).BoundingBox.Y,
              71,
              71);

            m_spriteBatch.Draw(King.getSelfImageBlack(), rectTestPiece, Color.Beige);

            rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(7, 6).BoundingBox.X,
             (int)m_gameTable.getTableSquare(7, 6).BoundingBox.Y,
              71,
              71);

            m_spriteBatch.Draw(King.getSelfImageWhite(), rectTestPiece, Color.Beige);

            rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(7, 7).BoundingBox.X,
             (int)m_gameTable.getTableSquare(7, 7).BoundingBox.Y,
              71,
              70);

            m_spriteBatch.Draw(King.getSelfImageWhite(), rectTestPiece, Color.Beige); 


            m_spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void ResetGame()
        {

            m_spriteBatch.Draw(King.getSelfImageBlack(),
                    m_gameTable.getTableSquare(0, 6).BoundingBox,
                    Color.Beige);

       
            m_spriteBatch.Draw(King.getSelfImageBlack(),
                                m_gameTable.getTableSquare(0, 6).BoundingBox
                                , Color.Beige);

            rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(0,7).BoundingBox.X,
             (int)m_gameTable.getTableSquare(0,7).BoundingBox.Y,
              71,
              71);

            m_spriteBatch.Draw(King.getSelfImageBlack(), rectTestPiece, Color.Beige);

            rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(7, 6).BoundingBox.X,
             (int)m_gameTable.getTableSquare(7, 6).BoundingBox.Y,
              71,
              71);

            m_spriteBatch.Draw(King.getSelfImageWhite(), rectTestPiece, Color.Beige);

            rectTestPiece = new Rectangle((int)m_gameTable.getTableSquare(7, 7).BoundingBox.X,
             (int)m_gameTable.getTableSquare(7, 7).BoundingBox.Y,
              71,
              70);

            m_spriteBatch.Draw(King.getSelfImageWhite(), rectTestPiece, Color.Beige); 

        }

    }
}
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
using ConstTypes;

namespace Xadrez
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager       m_graphics;

        SpriteBatch                 m_spriteBatch;

        public Rectangle            m_rectTable;
        Point                       m_pointSquareSelection;
        bool                        m_bPieceSelected;
        Texture2D                   m_maskSelection;
        Texture2D                   m_maskSelectionTarget;
        private List<Point>         m_listTargetsMoviments;
        bool                        m_bResetGame;
        Play_Turn                   m_playTurn;

        int m_updateCounts;
        bool m_bCanBePressed;



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


            m_listTargetsMoviments = new List<Point>();

            m_gameTable = new Table();

            m_bPieceSelected = false;
            m_bResetGame = true;

            m_bCanBePressed = false;
            m_updateCounts = 0;

            m_playTurn = Play_Turn.Black_Turn;

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

            //Loading black and white pieces (on temporary instances - but the images are static)
            Pawn pawn = new Pawn(0,0,false,null);
			pawn.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white pawn.png");
			pawn.SelfImageBlack = Texture2D.FromFile( m_graphics.GraphicsDevice, "../../../Content/Textures/brown pawn.png" );

            Knight knight = new Knight(0,0,false,null);
			knight.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white knight.png");
			knight.SelfImageBlack = Texture2D.FromFile( m_graphics.GraphicsDevice, "../../../Content/Textures/brown knight.png" );

            Bishop bishop = new Bishop(0,0,false,null);
			bishop.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white bishop.png");
			bishop.SelfImageBlack = Texture2D.FromFile( m_graphics.GraphicsDevice, "../../../Content/Textures/brown bishop.png" );

            Rook rook = new Rook(0,0,false,null);
			rook.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white rook.png");
			rook.SelfImageBlack = Texture2D.FromFile( m_graphics.GraphicsDevice, "../../../Content/Textures/brown rook.png" );

            King king = new King(0,0,false,null);
			king.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white king.png");
			king.SelfImageBlack = Texture2D.FromFile( m_graphics.GraphicsDevice, "../../../Content/Textures/brown king.png" );

            Queen queen = new Queen(0,0,false,null);
			queen.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white queen.png");
			queen.SelfImageBlack = Texture2D.FromFile( m_graphics.GraphicsDevice, "../../../Content/Textures/brown queen.png" );

            m_maskSelection = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/selection3.png");
            m_maskSelectionTarget = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/targetSelections.png");

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

            if (Keyboard.GetState().IsKeyDown(Keys.F10))
            {
                m_bResetGame = true;
                return;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                m_updateCounts++;
                m_bCanBePressed = true;
                return;
            }

            if ((m_bCanBePressed) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                if (m_updateCounts <= 10)
                    return;


                m_updateCounts = 0;
                Rectangle rectMouse;
                rectMouse.X = Mouse.GetState().X;
                rectMouse.Y = Mouse.GetState().Y;
                rectMouse.Width = 1;
                rectMouse.Height = 1;

                if (ClickOnTarget(rectMouse))
                {
                    m_pointSquareSelection.X = -1;
                    m_pointSquareSelection.Y = -1;
                    m_listTargetsMoviments.Clear();
                    m_bPieceSelected = false;

                    if (m_playTurn == Play_Turn.Black_Turn)
                        m_playTurn = Play_Turn.White_Turn;
                    else
                        m_playTurn = Play_Turn.Black_Turn;
                    
                    
                    base.Update(gameTime);

                    return;
                }

                ClickOnPiece(rectMouse);


            }

            base.Update(gameTime);
        }

        protected bool ClickOnTarget(Rectangle rectMouse)
        {
            if (m_listTargetsMoviments == null)
                return false;

            if (m_listTargetsMoviments.Count == 0)
                return false;

            for (int index = 0; index < m_listTargetsMoviments.Count; index++ )
            {
                Point ptSquare = m_listTargetsMoviments[index];
                Rectangle rectTarget = m_gameTable.getTableSquare(ptSquare.X, ptSquare.Y).BoundingBox;

                if(rectTarget.Intersects(rectMouse) )
                {
                    Play play = new Play();
                    play.newPosition = ptSquare;
                    play.oldPosition = m_pointSquareSelection;
                    m_gameTable.move(play);

                    return true;
                }
            }

            return false;
        }

        protected bool ClickOnPiece(Rectangle rectMouse)
        {
            m_bPieceSelected = false;
            m_listTargetsMoviments.Clear();

            for (int line = 0; line <= 7; line++)
            {
                for (int colunm = 0; colunm <= 7; colunm++)
                {
                    if (m_gameTable.getTableSquare(line, colunm).BoundingBox.Intersects(rectMouse))
                    {
                        if (m_gameTable.getTableSquare(line, colunm).Piece == null)
                            return false;

                        switch(m_playTurn)
                        {
                            case Play_Turn.Black_Turn:
                                if (!(m_gameTable.getTableSquare(line, colunm).Piece.Black))
                                    return false;
                                break;

                            case Play_Turn.White_Turn:
                                if (m_gameTable.getTableSquare(line, colunm).Piece.Black)
                                    return false;
                                break;
                        }

                        m_bPieceSelected = true;
                        m_pointSquareSelection.X = line;
                        m_pointSquareSelection.Y = colunm;

                        
                        m_listTargetsMoviments = m_gameTable.getTableSquare(line, colunm).Piece.getPossiblePositions();
                    }
                }
            }

            return true;


        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            m_spriteBatch.Begin();
            {
                m_spriteBatch.Draw(Table.GetSelfImage(), m_rectTable, Color.Beige);

                if (m_bResetGame)
                {
                    ResetGame();
                    m_bResetGame = false;
                    m_spriteBatch.End();
                    
                    return;
                }

                if (m_bPieceSelected)
                {
                    m_spriteBatch.Draw(m_maskSelection,
                                       m_gameTable.getTableSquare(m_pointSquareSelection.X, m_pointSquareSelection.Y).BoundingBox,
                                       Color.Beige);

                    if (m_listTargetsMoviments.Count > 0)
                    {
                        DrawTargets();
                    }
                }

                DrawGamePieces();

                               
            }
            m_spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void ResetGame()
        {

            m_spriteBatch.Draw(m_gameTable.getTableSquare(0, 0).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 0).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 1 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 1).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 2 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 2).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 3 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 3).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 4 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(5, 4).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 5 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 5).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 6 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 6).BoundingBox
                                , Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 0, 7 ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(0, 7).BoundingBox,
                                Color.Beige);

            for (int j = 0; j <= 7; j++ )
            {
				m_spriteBatch.Draw( m_gameTable.getTableSquare( 1, j ).Piece.SelfImageBlack,
                                m_gameTable.getTableSquare(1, j).BoundingBox,
                                Color.Beige);
            }


            for (int j = 0; j <= 7; j++)
            {
				m_spriteBatch.Draw( m_gameTable.getTableSquare( 6, j ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(6, j).BoundingBox,
                                Color.Beige);
            }

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 0 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 0).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 1 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 1).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 2 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 2).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 3 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 3).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 4 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 4).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 5 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 5).BoundingBox,
                                Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 6 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 6).BoundingBox
                                , Color.Beige);

			m_spriteBatch.Draw( m_gameTable.getTableSquare( 7, 7 ).Piece.SelfImageWhite,
                                m_gameTable.getTableSquare(7, 7).BoundingBox,
                                Color.Beige);

        }

        protected void DrawTargets()
        {
            for (int index = 0; index < m_listTargetsMoviments.Count; index++)
            {
                Point ptSquare = m_listTargetsMoviments[index];
                m_spriteBatch.Draw(m_maskSelectionTarget,
                                    m_gameTable.getTableSquare(ptSquare.X, ptSquare.Y).BoundingBox,
                                    Color.Beige);
            }
        }

        protected void DrawGamePieces()
        {
            for (int line = 0; line <= 7; line++)
            {
                for (int colunm = 0; colunm <= 7; colunm++)
                {
                    if (m_gameTable.getTableSquare(line, colunm).Piece == null)
                        continue;
                    if(m_gameTable.getTableSquare(line, colunm).Piece.Black)
                    {
                        m_spriteBatch.Draw(m_gameTable.getTableSquare(line, colunm).Piece.SelfImageBlack,
                                            m_gameTable.getTableSquare(line, colunm).BoundingBox,
                                            Color.Beige);
                    }
                    else
                    {
                        m_spriteBatch.Draw(m_gameTable.getTableSquare(line, colunm).Piece.SelfImageWhite,
                                            m_gameTable.getTableSquare(line, colunm).BoundingBox,
                                            Color.Beige);
                    }

                }
            }
        }

    }
}
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
using IA;

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
        Texture2D                   m_imgPlayNow;
        Texture2D                   m_imgWhiteButton;
        Texture2D                   m_imgBlackButton;
        Texture2D                   m_imgOpenGame;

        private Rectangle [ ]       m_DeadBlackSpaces;
        private Rectangle [ ]       m_DeadWhiteSpaces;
        private Rectangle           m_rectPlayNow;
        private Rectangle           m_rectButton;
        private Rectangle           m_rectOpenGame;

        private List<Point>         m_listTargetsMovements;
        bool                        m_bResetGame;
        Play_Turn                   m_playTurn;
        bool                        m_bPlayerBlackCPU;
        bool                        m_bPlayerWhiteCPU;
        MiniMax                     m_cpuPlayer;

        SoundEffect                 m_soundKlik;
        SoundEffect                 m_soundKill;

        int m_updateCounts;
        bool m_bCanBePressed;
        bool m_bInitGame;



        Table m_gameTable;

        const int TOTAL_PIECES = 32;
        const int TOTAL_BLACK_PIECES = TOTAL_PIECES/2;
        const int TOTAL_WHITE_PIECES = TOTAL_PIECES/2;

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


            m_listTargetsMovements = new List<Point>();

            m_gameTable = new Table();

            m_bPieceSelected = false;
            m_bResetGame = true;
            m_bInitGame = false;

            m_bCanBePressed = false;
            m_updateCounts = 0;

            m_playTurn = Play_Turn.White_Turn;
            m_bPlayerBlackCPU = true;
            m_bPlayerWhiteCPU = false;
            m_cpuPlayer = new MiniMax();
            m_cpuPlayer.setTeam(m_bPlayerBlackCPU); // CPU is with Black pieces.

            m_DeadBlackSpaces = new Rectangle[TOTAL_BLACK_PIECES];
            m_DeadWhiteSpaces = new Rectangle[TOTAL_WHITE_PIECES];

            InitializeDeadSpaces();

			m_gameTable.calculateWhitePiecesPosition( false );

            m_rectPlayNow.Height = 100;
            m_rectPlayNow.Width = 20;
            m_rectPlayNow.X = (36 / 2) - (m_rectPlayNow.Width/2);
            m_rectPlayNow.Y = (640 / 2) - (m_rectPlayNow.Height/2);

            m_rectButton.Height = 20;
            m_rectButton.Width = m_rectPlayNow.Width;
            m_rectButton.X = (36 / 2) - (m_rectButton.Width / 2);
            m_rectButton.Y = ((m_rectPlayNow.Y + m_rectPlayNow.Height) + 4);


            m_rectOpenGame.Height = 300;
            m_rectOpenGame.Width = 400;
            m_rectOpenGame.X = (m_rectTable.Width / 2) - (m_rectOpenGame.Width/2);
            m_rectOpenGame.Y = (m_rectTable.Height / 2) - (m_rectOpenGame.Height / 2);
            
        }

        protected void InitializeDeadSpaces()
        {
            for (int i = 0; i < (TOTAL_PIECES/2); i++)
            {
                m_DeadBlackSpaces[i] = new Rectangle((36 + (i * 35)), 0, 35, 35);
                m_DeadWhiteSpaces[i] = new Rectangle((36 + (i * 35)), (m_rectTable.Height - 35), 35, 35);                  
            }
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
            pawn.SelfImageBlack = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/light_brown pawn.png");

            Knight knight = new Knight(0,0,false,null);
			knight.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white knight.png");
            knight.SelfImageBlack = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/light_brown knight.png");

            Bishop bishop = new Bishop(0,0,false,null);
			bishop.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white bishop.png");
            bishop.SelfImageBlack = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/light_brown bishop.png");

            Rook rook = new Rook(0,0,false,null);
			rook.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white rook.png");
            rook.SelfImageBlack = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/light_brown rook.png");

            King king = new King(0,0,false,null);
			king.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white king.png");
            king.SelfImageBlack = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/light_brown king.png");

            Queen queen = new Queen(0,0,false,null);
			queen.SelfImageWhite = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/white queen.png");
            queen.SelfImageBlack = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/light_brown queen.png");

            m_maskSelection = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/selection3.png");
            m_maskSelectionTarget = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/targetSelections.png");

            m_imgPlayNow = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/PlayerNow.png");

            m_imgWhiteButton = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/BotãoBranco.png");
            m_imgBlackButton = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/BotãoMarrom.png");

            m_imgOpenGame = Texture2D.FromFile(m_graphics.GraphicsDevice, "../../../Content/Textures/Abertura.png");

			m_soundKlik = Content.Load<SoundEffect>( "Sounds\\KLICK" );
			m_soundKill = Content.Load<SoundEffect>( "Sounds\\BEEP_FM" );

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

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                m_bInitGame = true;
                m_bCanBePressed = true;
            }

            if(!m_bInitGame)
                return;

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

            if (m_bResetGame)
                return;

            if ((m_playTurn == Play_Turn.White_Turn) && (m_bPlayerWhiteCPU))
            {
                m_cpuPlayer.setTeam(false); // White team.
                Play play = m_cpuPlayer.getCPUPlay(m_gameTable);
                m_gameTable.move(play);
                m_soundKlik.Play();
				
				if( m_gameTable.CheckMate )
				{
					m_bInitGame = false;
				}
				else
				{
					changeTurn( );
				}
                
				base.Update(gameTime);

                return;
            }

            if ((m_playTurn == Play_Turn.Black_Turn) && (m_bPlayerBlackCPU))
            {
                m_cpuPlayer.setTeam(true); // Black team.
                Play play = m_cpuPlayer.getCPUPlay(m_gameTable);
                m_gameTable.move(play);
                m_soundKlik.Play();

				if( m_gameTable.CheckMate )
				{
					m_bInitGame = false;
				}
				else
				{
					changeTurn( );
				}
                
				base.Update(gameTime);

                return;
            }

            if ((m_bCanBePressed) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                if (m_updateCounts <= 26)
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
                    m_listTargetsMovements = null;
                    m_bPieceSelected = false;

					if( m_gameTable.CheckMate )
					{
						m_bInitGame = false;
					}
					else
					{
						changeTurn( );
					}
                    
                    base.Update(gameTime);

                    return;
                }

                ClickOnPiece(rectMouse);


            }

            base.Update(gameTime);
        }

        protected bool ClickOnTarget(Rectangle rectMouse)
        {
            if (m_listTargetsMovements == null)
                return false;

            if (m_listTargetsMovements.Count == 0)
                return false;

            for (int index = 0; index < m_listTargetsMovements.Count; index++ )
            {
                Point ptSquare = m_listTargetsMovements[index];
                Rectangle rectTarget = m_gameTable.getTableSquare(ptSquare.X, ptSquare.Y).BoundingBox;

                if(rectTarget.Intersects(rectMouse) )
                {
                    bool bKilled = false;
                    if (m_gameTable.getTableSquare(ptSquare.X, ptSquare.Y).Piece != null)
                    {
                        bKilled = true;
                    }

                    Play play = new Play();
                    play.newPosition = ptSquare;
                    play.oldPosition = m_pointSquareSelection;
                    m_gameTable.move(play);

                    m_soundKlik.Play();

                    if (bKilled)
                        m_soundKill.Play();

                    return true;
                }
            }

            return false;
        }

        protected bool ClickOnPiece(Rectangle rectMouse)
        {
            m_bPieceSelected = false;
            m_listTargetsMovements = null;

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
                                if (!(m_gameTable.getTableSquare(line, colunm).Piece.isBlack))
                                    return false;
                                break;

                            case Play_Turn.White_Turn:
                                if (m_gameTable.getTableSquare(line, colunm).Piece.isBlack)
                                    return false;
                                break;
                        }

                        m_bPieceSelected = true;
                        m_pointSquareSelection.X = line;
                        m_pointSquareSelection.Y = colunm;

                        
                        m_listTargetsMovements = m_gameTable.getTableSquare(line, colunm).Piece.vetPossibleMovements;
                    }
                }
            }

            return true;
        }

        protected void changeTurn()
        {
            if (m_playTurn == Play_Turn.Black_Turn)
                m_playTurn = Play_Turn.White_Turn;
            else
                m_playTurn = Play_Turn.Black_Turn;
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

                if (!m_bInitGame)
                {
                    m_spriteBatch.Draw(m_imgOpenGame,
                                      m_rectOpenGame,
                                      Color.Beige);

                    m_spriteBatch.End();
                    return;
                }

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

                    if (m_listTargetsMovements.Count > 0)
                    {
                        DrawTargets();
                    }
                }

                DrawGamePieces();

                DrawDeadPieces();

                m_spriteBatch.Draw(m_imgPlayNow,
                                   m_rectPlayNow,
                                   Color.Beige);
                
                if(m_playTurn == Play_Turn.Black_Turn)
                    m_spriteBatch.Draw(m_imgBlackButton,
                                   m_rectButton,
                                   Color.Beige);
                else
                    m_spriteBatch.Draw(m_imgWhiteButton,
                                   m_rectButton,
                                   Color.Beige);


                                   
                

                               
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
            for (int index = 0; index < m_listTargetsMovements.Count; index++)
            {
                Point ptSquare = m_listTargetsMovements[index];
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
                    if(m_gameTable.getTableSquare(line, colunm).Piece.isBlack)
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

        protected void DrawDeadPieces()
        {
            List<Piece>  listDeadWhitePieces = m_gameTable.GetListDeadWhitePieces();
            List<Piece>  listDeadBlackPieces = m_gameTable.GetListDeadBlackPieces();

            for (int i = 0; i < listDeadWhitePieces.Count(); i++)
            {
                Piece tempPiece = listDeadWhitePieces[i];
                m_spriteBatch.Draw(tempPiece.SelfImageWhite,
                                    m_DeadWhiteSpaces[i],
                                    Color.White);                                                
            }

            for (int i = 0; i < listDeadBlackPieces.Count(); i++)
            {
                Piece tempPiece = listDeadBlackPieces[i];
                m_spriteBatch.Draw(tempPiece.SelfImageBlack,
                                    m_DeadBlackSpaces[i],
                                    Color.White);
            }
        }



    }
}
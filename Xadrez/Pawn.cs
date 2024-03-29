﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstTypes;


namespace Xadrez
{
    public class Pawn : Xadrez.Piece
    {
        static Texture2D m_selfImageBlack;
        static Texture2D m_selfImageWhite;

		bool m_bFirstMove;

        public Pawn(int _posX, int _posY, bool bBlack, Table _parentTable ) : base(_posX, _posY, bBlack, _parentTable )
        {
			m_bFirstMove = true;
        }

		public
		override
		void
		SetPosition( int _posX, int _posY )
		{
			base.SetPosition( _posX, _posY );
			m_bFirstMove = false;
		}
		public
		override
		void
		calculatePossiblePositions( bool _bVerifyCheck )
		{
			m_vetPossibleMovements.Clear( );

			int TABLE_LIMIT = ParentTable.TableSize - 1;

			// To check if there's a friend piece on the way
			Piece friend;
			Point newPosition;

			if( m_bBlack )
			{
				// If the piece is Black it is facing down and it moves from up to down

				// Forward movement
				if( m_position.X < TABLE_LIMIT )
				{
					friend = ParentTable.getTableSquare( m_position.X + 1, m_position.Y ).Piece;

					if( friend == null )
					{
						newPosition = new Point( m_position.X + 1, m_position.Y );
						if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
						{
							m_vetPossibleMovements.Add( newPosition );
						}
						//else
						//{
						//    m_parentTable.Check = true;
						//}

						if( m_bFirstMove && ( ( m_position.X + 1 ) < TABLE_LIMIT ) )
						{
							friend = ParentTable.getTableSquare( m_position.X + 2, m_position.Y ).Piece;

							if( friend == null )
							{
								newPosition = new Point( m_position.X + 2, m_position.Y );
								if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
								{
									m_vetPossibleMovements.Add( newPosition );
								}
								//else
								//{
								//    m_parentTable.Check = true;
								//}
							}
						}
					}
				}

				// Forward Left movement - in case of eating an enemy
				if( ( m_position.X < TABLE_LIMIT ) && ( m_position.Y > 0 ) )
				{
					friend = ParentTable.getTableSquare( m_position.X + 1, m_position.Y - 1 ).Piece;

					if( ( friend != null ) && !friend.isBlack )
					{
						newPosition = new Point( m_position.X + 1, m_position.Y - 1 );
						if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
						{
							m_vetPossibleMovements.Add( newPosition );
						}
						//else
						//{
						//    m_parentTable.Check = true;
						//}
					}
				}

				// Forward Right movement - in case of eating an enemy
				if( ( ( m_position.X < TABLE_LIMIT ) && m_position.Y < TABLE_LIMIT ) )
				{
					friend = ParentTable.getTableSquare( m_position.X + 1, m_position.Y + 1 ).Piece;

					if( ( friend != null ) && !friend.isBlack )
					{
						newPosition = new Point( m_position.X + 1, m_position.Y + 1 );
						if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
						{
							m_vetPossibleMovements.Add( newPosition );
						}
						//else
						//{
						//    m_parentTable.Check = true;
						//}
					}
				}
			}
			else
			{
				// If the piece is White it is facing up and it moves from down to up

				// Forward movement
				if( m_position.X > 0 )
				{
					friend = ParentTable.getTableSquare( m_position.X - 1, m_position.Y ).Piece;

					if( friend == null )
					{
						newPosition = new Point( m_position.X - 1, m_position.Y );
						if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
						{
							m_vetPossibleMovements.Add( newPosition );
						}
						//else
						//{
						//    m_parentTable.Check = true;
						//}

						if( m_bFirstMove && ( ( m_position.X - 1 ) > 0 ) )
						{
							friend = ParentTable.getTableSquare( m_position.X - 2, m_position.Y ).Piece;

							if( friend == null )
							{
								newPosition = new Point( m_position.X - 2, m_position.Y );
								if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
								{
									m_vetPossibleMovements.Add( newPosition );
								}
								//else
								//{
								//    m_parentTable.Check = true;
								//}
							}
						}
					}
				}

				// Forward Left movement - in case of eating an enemy
				if( ( m_position.X > 0 ) && ( m_position.Y > 0 ) )
				{
					friend = ParentTable.getTableSquare( m_position.X - 1, m_position.Y - 1 ).Piece;

					if( ( friend != null ) && friend.isBlack )
					{
						newPosition = new Point( m_position.X - 1, m_position.Y - 1 );
						if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
						{
							m_vetPossibleMovements.Add( newPosition );
						}
						//else
						//{
						//    m_parentTable.Check = true;
						//}
					}
				}

				// Forward Right movement - in case of eating an enemy
				if( ( m_position.X > 0 ) && ( m_position.Y < TABLE_LIMIT ) )
				{
					friend = ParentTable.getTableSquare( m_position.X - 1, m_position.Y + 1 ).Piece;

					if( ( friend != null ) && friend.isBlack )
					{
						newPosition = new Point( m_position.X - 1, m_position.Y + 1 );
						if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
						{
							m_vetPossibleMovements.Add( newPosition );
						}
						//else
						//{
						//    m_parentTable.Check = true;
						//}
					}
				}
			}

			//return possiblePositions;
		}

		public
		override
		Texture2D
		SelfImageBlack
		{
			get
			{
				return m_selfImageBlack;
			}

			set
			{
				m_selfImageBlack = value;
			}
		}

		public
		override
		Texture2D
		SelfImageWhite
		{
			get
			{
				return m_selfImageWhite;
			}

			set
			{
				m_selfImageWhite = value;
			}
		}


		//public 
		//bool 
		//TestRules(int _posX, int _posY)
		//{
		//    return false;
		//}

		//public static void SetSelfImageBlack(Texture2D _texture)
		//{
		//    selfImageBlack = _texture;
		//}

		//public static void SetSelfImageWhite(Texture2D _texture)
		//{
		//    selfImageWhite = _texture;
		//}

		//public static Texture2D
		//getSelfImageWhite()
		//{
		//    return selfImageWhite;
		//}

		//public static Texture2D
		//getSelfImageBlack()
		//{
		//    return selfImageBlack;
		//}

    }
}

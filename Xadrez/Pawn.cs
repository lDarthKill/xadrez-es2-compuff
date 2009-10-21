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

        public Pawn(int _posX, int _posY, bool bBlack, Table _parentTable ) : base(_posX, _posY, bBlack, _parentTable )
        {
			//m_movement = new PieceMovement();
        }

		public
		override
		List<Point>
		getPossiblePositions( )
		{
			List<Point> possiblePositions = new List<Point>( );

			int iTableLimit = m_parentTable.TableSize - 1;

			//TO DO: Check for "Cheque" conditions

			// To check if there's a friend piece on the way
			Piece friend;

			if( m_bBlack )
			{
				// If the piece is Black it is facing down and it moves from up to down

				// Forward movement
				if( m_position.X < iTableLimit )
				{
					friend = m_parentTable.getTableSquare( m_position.X + 1, m_position.Y ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X + 1, m_position.Y ) );
					}
				}

				// Backward movement
				if( m_position.X > 0 )
				{
					friend = m_parentTable.getTableSquare( m_position.X - 1, m_position.Y ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X - 1, m_position.Y ) );
					}
				}

				// Left movement
				if( m_position.Y > 0 )
				{
					friend = m_parentTable.getTableSquare( m_position.X, m_position.Y - 1 ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X, m_position.Y - 1 ) );
					}
				}

				// Right movement
				if( m_position.Y < iTableLimit )
				{
					friend = m_parentTable.getTableSquare( m_position.X, m_position.Y + 1 ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X, m_position.Y + 1 ) );
					}
				}

				// Forward Left movement
				if( ( m_position.X < iTableLimit ) && ( m_position.Y > 0 ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X + 1, m_position.Y - 1 ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X + 1, m_position.Y - 1 ) );
					}
				}

				// Forward Right movement
				if( ( ( m_position.X < iTableLimit ) && m_position.Y < iTableLimit ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X + 1, m_position.Y + 1 ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X + 1, m_position.Y + 1 ) );
					}
				}

				// Backward Left movement
				if( ( m_position.X > 0 ) && ( m_position.Y > 0 ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X - 1, m_position.Y - 1 ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X - 1, m_position.Y - 1 ) );
					}
				}

				// Backward Right movement
				if( ( m_position.X > 0 ) && ( m_position.Y < iTableLimit ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X - 1, m_position.Y + 1 ).Piece;

					if( !( ( friend != null ) && friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X - 1, m_position.Y + 1 ) );
					}
				}
			}
			else
			{
				// If the piece is White it is facing up and it moves from down to up

				// Forward movement
				if( m_position.X > 0 )
				{
					friend = m_parentTable.getTableSquare( m_position.X - 1, m_position.Y ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X - 1, m_position.Y ) );
					}
				}

				// Backward movement
				if( m_position.X < iTableLimit )
				{
					friend = m_parentTable.getTableSquare( m_position.X + 1, m_position.Y ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X + 1, m_position.Y ) );
					}
				}

				// Left movement
				if( m_position.Y > 0 )
				{
					friend = m_parentTable.getTableSquare( m_position.X, m_position.Y - 1 ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X, m_position.Y - 1 ) );
					}
				}

				// Right movement
				if( m_position.Y < iTableLimit )
				{
					friend = m_parentTable.getTableSquare( m_position.X, m_position.Y + 1 ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X, m_position.Y + 1 ) );
					}
				}

				// Forward Left movement
				if( ( m_position.X > 0 ) && ( m_position.Y > 0 ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X - 1, m_position.Y - 1 ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X - 1, m_position.Y - 1 ) );
					}
				}

				// Forward Right movement
				if( ( m_position.X > 0 ) && ( m_position.Y < iTableLimit ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X - 1, m_position.Y + 1 ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X - 1, m_position.Y + 1 ) );
					}
				}

				// Backward Left movement
				if( ( m_position.X < iTableLimit ) && ( m_position.Y > 0 ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X + 1, m_position.Y - 1 ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X + 1, m_position.Y - 1 ) );
					}
				}

				// Backward Right movement
				if( ( ( m_position.X < iTableLimit ) && m_position.Y < iTableLimit ) )
				{
					friend = m_parentTable.getTableSquare( m_position.X + 1, m_position.Y + 1 ).Piece;

					if( !( ( friend != null ) && !friend.Black ) )
					{
						possiblePositions.Add( new Point( m_position.X + 1, m_position.Y + 1 ) );
					}
				}
			}

			return possiblePositions;
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

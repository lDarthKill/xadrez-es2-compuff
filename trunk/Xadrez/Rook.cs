using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class Rook : Xadrez.Piece
    {
        static Texture2D m_selfImageBlack;
        static Texture2D m_selfImageWhite;

        public Rook(int _posX, int _posY, bool bBlack, Table _parentTable) : base(_posX, _posY, bBlack, _parentTable)
        {
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

			// Forward movement for Black, Backward movement for White
			for( int i = m_position.X; i < iTableLimit; i++ )
			{
				friend = m_parentTable.getTableSquare( i + 1, m_position.Y ).Piece;

				if( ( friend != null ) && ( friend.Black == m_bBlack ) )
				{
					// If there's a piece of the same color on the way, stops before it
					break;
				}

				possiblePositions.Add( new Point( i + 1, m_position.Y ) );

				if( ( friend != null ) && ( friend.Black != m_bBlack ) )
				{
					// If there's a piece of different color on the way, stops on it (allowing to eat it)
					break;
				}
			}

			// Backward movement for Black, Forward movement for White
			for( int i = m_position.X; i > 0; i-- )
			{
				friend = m_parentTable.getTableSquare( i - 1, m_position.Y ).Piece;

				if( ( friend != null ) && ( friend.Black == m_bBlack ) )
				{
					// If there's a piece of the same color on the way, stops before it
					break;
				}

				possiblePositions.Add( new Point( i - 1, m_position.Y ) );

				if( ( friend != null ) && ( friend.Black != m_bBlack ) )
				{
					// If there's a piece of different color on the way, stops on it (allowing to eat it)
					break;
				}
			}

			// Left movement
			for( int i = m_position.Y; i > 0; i-- )
			{
				friend = m_parentTable.getTableSquare( m_position.X, i - 1 ).Piece;

				if( ( friend != null ) && ( friend.Black == m_bBlack ) )
				{
					break;
				}

				possiblePositions.Add( new Point( m_position.X, i - 1 ) );

				if( ( friend != null ) && ( friend.Black != m_bBlack ) )
				{
					break;
				}
			}

			// Right movement
			for( int i = m_position.Y; i < iTableLimit; i++ )
			{
				friend = m_parentTable.getTableSquare( m_position.X, i + 1 ).Piece;

				if( ( friend != null ) && ( friend.Black == m_bBlack ) )
				{
					break;
				}

				possiblePositions.Add( new Point( m_position.X, i + 1 ) );

				if( ( friend != null ) && ( friend.Black != m_bBlack ) )
				{
					break;
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

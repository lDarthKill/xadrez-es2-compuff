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
		void
		calculatePossiblePositions( bool _bVerifyCheck )
		{
			m_vetPossibleMovements.Clear( );

			int TABLE_LIMIT = ParentTable.TableSize - 1;

			// To check if there's a friend piece on the way
			Piece friend;
			Point newPosition;

			// Forward movement for Black, Backward movement for White
			for( int i = m_position.X; i < TABLE_LIMIT; i++ )
			{
				friend = ParentTable.getTableSquare( i + 1, m_position.Y ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					// If there's a piece of the same color on the way, stops before it
					break;
				}

				newPosition = new Point( i + 1, m_position.Y );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}
				//else
				//{
				//    m_parentTable.Check = true;
				//}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					// If there's a piece of different color on the way, stops on it (allowing to eat it)
					break;
				}
			}

			// Backward movement for Black, Forward movement for White
			for( int i = m_position.X; i > 0; i-- )
			{
				friend = ParentTable.getTableSquare( i - 1, m_position.Y ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					// If there's a piece of the same color on the way, stops before it
					break;
				}

				newPosition = new Point( i - 1, m_position.Y );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}
				//else
				//{
				//    m_parentTable.Check = true;
				//}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					// If there's a piece of different color on the way, stops on it (allowing to eat it)
					break;
				}
			}

			// Left movement
			for( int i = m_position.Y; i > 0; i-- )
			{
				friend = ParentTable.getTableSquare( m_position.X, i - 1 ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					break;
				}

				newPosition = new Point( m_position.X, i - 1 );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}
				//else
				//{
				//    m_parentTable.Check = true;
				//}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					break;
				}
			}

			// Right movement
			for( int i = m_position.Y; i < TABLE_LIMIT; i++ )
			{
				friend = ParentTable.getTableSquare( m_position.X, i + 1 ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					break;
				}

				newPosition = new Point( m_position.X, i + 1 );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}
				//else
				//{
				//    m_parentTable.Check = true;
				//}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					break;
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

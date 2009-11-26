using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class Bishop : Xadrez.Piece
    {
        static Texture2D m_selfImageBlack;
        static Texture2D m_selfImageWhite;

        public Bishop(int _posX, int _posY, bool bBlack, Table _parentTable ) : base(_posX, _posY, bBlack, _parentTable)
        {
			//m_movement = new PieceMovement();
        }

		public
		override
		void
		calculatePossiblePositions( bool _bVerifyCheck )
		{
			m_vetPossibleMovements.Clear( );

			int TABLE_LIMIT = m_parentTable.TableSize - 1;

			// To check if there's a friend piece on the way
			Piece friend;
			Point newPosition;

			int row;
			int col;

			// Forward Left movement
			for( row = m_position.X, col = m_position.Y; ( row < TABLE_LIMIT ) && ( col > 0 ); row++, col-- )
			{
				friend = m_parentTable.getTableSquare( row + 1, col - 1 ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					break;
				}

				newPosition = new Point( row + 1, col - 1 );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					break;
				}

			}

			// Forward Right movement
			for( row = m_position.X, col = m_position.Y; ( row < TABLE_LIMIT ) && ( col < TABLE_LIMIT ); row++, col++ )
			{
				friend = m_parentTable.getTableSquare( row + 1, col + 1 ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					break;
				}

				newPosition = new Point( row + 1, col + 1 );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					break;
				}
			}

			// Backward Left movement
			for( row = m_position.X, col = m_position.Y; ( row > 0 ) && ( col > 0 ); row--, col-- )
			{
				friend = m_parentTable.getTableSquare( row - 1, col - 1 ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					break;
				}

				newPosition = new Point( row - 1, col - 1 );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}

				if( ( friend != null ) && ( friend.isBlack != m_bBlack ) )
				{
					break;
				}
			}

			// Backward Right movement
			for( row = m_position.X, col = m_position.Y; ( row > 0 ) && ( col < TABLE_LIMIT ); row--, col++ )
			{
				friend = m_parentTable.getTableSquare( row - 1, col + 1 ).Piece;

				if( ( friend != null ) && ( friend.isBlack == m_bBlack ) )
				{
					break;
				}

				newPosition = new Point( row - 1, col + 1 );
				if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
				{
					m_vetPossibleMovements.Add( newPosition );
				}

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class King : Xadrez.Piece
    {
        static Texture2D m_selfImageBlack;
        static Texture2D m_selfImageWhite;

		public King( int _posX, int _posY, bool bBlack, Table _parentTable )
			: base( _posX, _posY, bBlack, _parentTable )
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

			// Forward movement
			if( m_position.X < TABLE_LIMIT )
			{
				friend = ParentTable.getTableSquare( m_position.X + 1, m_position.Y ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X + 1, m_position.Y );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Backward movement
			if( m_position.X > 0 )
			{
				friend = ParentTable.getTableSquare( m_position.X - 1, m_position.Y ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X - 1, m_position.Y );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Left movement
			if( m_position.Y > 0 )
			{
				friend = ParentTable.getTableSquare( m_position.X, m_position.Y - 1 ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X, m_position.Y - 1 );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Right movement
			if( m_position.Y < TABLE_LIMIT )
			{
				friend = ParentTable.getTableSquare( m_position.X, m_position.Y + 1 ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X, m_position.Y + 1 );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Forward Left movement
			if( ( m_position.X < TABLE_LIMIT ) && ( m_position.Y > 0 ) )
			{
				friend = ParentTable.getTableSquare( m_position.X + 1, m_position.Y - 1 ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X + 1, m_position.Y - 1 );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Forward Right movement
			if( ( ( m_position.X < TABLE_LIMIT ) && m_position.Y < TABLE_LIMIT ) )
			{
				friend = ParentTable.getTableSquare( m_position.X + 1, m_position.Y + 1 ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X + 1, m_position.Y + 1 );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Backward Left movement
			if( ( m_position.X > 0 ) && ( m_position.Y > 0 ) )
			{
				friend = ParentTable.getTableSquare( m_position.X - 1, m_position.Y - 1 ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X - 1, m_position.Y - 1 );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Backward Right movement
			if( ( m_position.X > 0 ) && ( m_position.Y < TABLE_LIMIT ) )
			{
				friend = ParentTable.getTableSquare( m_position.X - 1, m_position.Y + 1 ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					newPosition = new Point( m_position.X - 1, m_position.Y + 1 );
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xadrez
{
    class Knight : Xadrez.Piece
    {
        static Texture2D m_selfImageBlack;
        static Texture2D m_selfImageWhite;

        public Knight(int _posX, int _posY, bool bBlack, Table _parentTable) : base(_posX, _posY, bBlack, _parentTable)
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

			// Down-Left movement
			int row = m_position.X + 2;
			int col = m_position.Y - 1;
			Point newPosition = new Point( row, col );
			
			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack )) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Down-Right movement
			row = m_position.X + 2;
			col = m_position.Y + 1;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Up-Left movement
			row = m_position.X - 2;
			col = m_position.Y - 1;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Up-Right movement
			row = m_position.X - 2;
			col = m_position.Y + 1;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Left-Down movement
			row = m_position.X + 1;
			col = m_position.Y - 2;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Left-Up movement
			row = m_position.X - 1;
			col = m_position.Y - 2;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Right-Down movement
			row = m_position.X + 1;
			col = m_position.Y + 2;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
					if( !_bVerifyCheck || ( isValidMove( newPosition ) && _bVerifyCheck ) )
					{
						m_vetPossibleMovements.Add( newPosition );
					}
				}
			}

			// Right-Up movement
			row = m_position.X - 1;
			col = m_position.Y + 2;
			newPosition = new Point( row, col );

			if( ParentTable.isPtInTable( newPosition ) )
			{
				friend = ParentTable.getTableSquare( row, col ).Piece;

				if( !( ( friend != null ) && ( friend.isBlack == m_bBlack ) ) )
				{
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstTypes;

namespace Xadrez
{
	public abstract class Piece
	{
		// Private attributes
		protected     bool            m_bAlive;
		protected     bool            m_bBlack;
		protected     Point           m_position;
		protected     Table           m_parentTable;
		//protected   PieceMovement   m_movement;

		//        static Texture2D selfImageBlack;
		//        static Texture2D selfImageWhite;

		
		// Constructors
		public 
		Piece( bool bBlack, Table _parentTable ) : this( 0, 0, bBlack, _parentTable )
		{
			
		}

		public
		Piece( int posX, int posY, bool bBlack, Table _parentTable )
		{
			SetPosition( posX, posY );
			m_bBlack = bBlack;
			m_parentTable = _parentTable;
		}

		
		// Public Access Methods and Properties
		public 
		Point 
		Position
		{
			get
			{
				return m_position;
			}

			set
			{
				SetPosition( value.X, value.Y );
			}
		}

		public
		virtual
		void 
		SetPosition( int _posX, int _posY )
		{
			m_position = new Point( _posX, _posY );
		}

		public 
		bool 
		Alive
		{
			get
			{
				return m_bAlive;
			}

			set
			{
				m_bAlive = value;
			}
		}

		public
		bool 
		Black
		{
			get
			{
				return m_bBlack;
			}

			set
			{
				m_bBlack = value;
			}
		}

		
		// Public Abstract Methods and Properties
		public
		abstract
		List<Point>
		getPossiblePositions( );

		public
		abstract
		Texture2D
		SelfImageBlack
		{
			get;
			set;
		}

		public
		abstract
		Texture2D
		SelfImageWhite
		{
			get;
			set;
		}
	}
}

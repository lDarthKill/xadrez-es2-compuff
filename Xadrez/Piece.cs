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
		Piece( int nRow, int nCol, bool bBlack, Table _parentTable )
		{
			m_position = new Point( nRow, nCol );
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
		SetPosition( int nRow, int nCol )
		{
			m_position = new Point( nRow, nCol );
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

        public Piece Clone()
        {
            if (this == null)
                return null;

            return (Piece)this.MemberwiseClone();
        }
		
        public bool Equals(Piece piece)
        {
            if (this.GetType() != piece.GetType())
                return false;
            if (this.Position.X != piece.Position.X)
                return false;
            if (this.Position.Y != piece.Position.Y)
                return false;
            if (this.Alive != piece.Alive)
                return false;
            if (this.Black != piece.Black)
                return false;

            return true;
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

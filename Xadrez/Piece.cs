using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConstTypes;

namespace Xadrez
{
	public abstract class Piece : ICloneable
	{
		// Private attributes
		protected	bool		m_bAlive;
		protected	bool		m_bBlack;
		protected	Point		m_position;
		protected	Table		m_parentTable;

		protected	List<Point>	m_vetPossibleMovements;

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
			m_vetPossibleMovements = new List<Point>( );
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
		isAlive
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
		isBlack
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

		public bool Equals(Piece piece)
        {
            if (this.GetType() != piece.GetType())
                return false;
            if (this.Position.X != piece.Position.X)
                return false;
            if (this.Position.Y != piece.Position.Y)
                return false;
            if (this.isAlive != piece.isAlive)
                return false;
            if (this.isBlack != piece.isBlack)
                return false;

            return true;
        }

		protected bool isValidMove( Point _newPosition )
		{
			Play play = new Play( );
			play.oldPosition = m_position;
			play.newPosition = _newPosition;

			m_parentTable.VerifyCheck = false;
			Table newTable = m_parentTable + play;
			m_parentTable.VerifyCheck = true;

			if( newTable.isInCheck( m_bBlack ) )
			{
				return false;
			}

			return true;
		}

		public List<Point> vetPossibleMovements
		{
			get
			{
				return m_vetPossibleMovements;
			}

			//set
			//{
			//    m_vetPossibleMovements = value;
			//}
		}

		public 
		Table 
		ParentTable
		{
			get
			{
				return m_parentTable;
			}

			set
			{
				m_parentTable = value;
			}
		}

		public
		void
		calculatePossiblePositions( )
		{
			calculatePossiblePositions( true );
		}

	
		// Public Abstract Methods and Properties
		public
		abstract
		void
		calculatePossiblePositions( bool _bVerifyCheck );

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

		#region ICloneable Members
		public object Clone( )
		{
			Piece newPiece = ( Piece )this.MemberwiseClone( );
			newPiece.m_position = new Point( this.m_position.X, this.m_position.Y );
			
			newPiece.m_vetPossibleMovements = new List<Point>( );
			for( int i = 0; i < this.m_vetPossibleMovements.Count; i++ )
			{
				newPiece.m_vetPossibleMovements.Add( new Point( this.m_vetPossibleMovements[ i ].X, this.m_vetPossibleMovements[ i ].Y ) );
			}

			return ( object )newPiece;
		}
		#endregion
	}
}

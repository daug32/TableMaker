using System;
using System.Linq;
using System.Collections.Generic;

namespace TableMaker
{
    public class Column : IColumn
    {
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Round  { get; private set; }
        public int Count { get => _values.Count; }

        public double DefaultValue;
        private List<double> _values;
        
        public Column( string name )
        {
            if( String.IsNullOrEmpty( name ) ) 
            {
                throw new ArgumentException( "Column's name can't be empty" );
            }

            Round = -1;
            Name = name;
            DefaultValue = 0;
            Width = name.Length;
            _values = new List<double>();
        }

        public void AddValue( double value )
        {
            _values.Add( value );
            Resize();
        }

        public void AddValue( float value )
        {
            _values.Add( (double)value );
            Resize();
        }

        public void SetValue( float value, int index ) => SetValue( ( double )value, index );

        public void SetValue( double value, int index )
        {
            if ( index < 0 || index > Count - 1)
            {
                throw new ArgumentException( "Index is out of range" );
            }

            _values[ index ] = value;

            Resize();
        }

        public float GetValueF( int index ) => ( float )GetValue( index );

        public double GetValue(int index)
        {
            if ( index < 0 || index > Count - 1)
            {
                throw new ArgumentException( "Index is out of range" );
            }

            return _values[ index ];
        }

        public string PrintName() => Name + new string(' ', Width - Name.Length);

        public string PrintValue(int index)
        {
            if ( index < 0 || index > Count - 1 )
            {
                throw new ArgumentException( "Index is out of range" );
            }

            var value = _values[ index ];
            if ( Round > -1 ) value = Math.Round( value, Round );
            var stringValue = value.ToString();
            return stringValue + new string( ' ', Width - stringValue.Length );
        }

        public void SetRound(int round)
        {
            Round = round;
            Resize();
        }

        private void Resize()
        {
            var maxWidth = _values.Aggregate( 0, ( int result, double el ) =>
            {
                var temp = el;
                if ( Round > -1 ) temp = Math.Round( el, Round );
                var length = temp.ToString().Length;
                return Math.Max( result, length );
            } );
            Width = maxWidth;
        }
    }
}
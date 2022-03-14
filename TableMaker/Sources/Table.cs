using System;
using System.Linq;
using System.Collections.Generic;

namespace TableMaker
{
    public class Table : ITable
    {
        private List<IColumn> _data;
        public int RowCount { get => _data[0].Count; }
        public string Postfix { get; set; }
        public string Prefix { get; set; }

        public Table()
        {
            _data = new List<IColumn>();
            Postfix = Prefix = "|";
        }

        public void AddColumns( string[] columns )
        {
            if ( columns == null )
            {
                throw new ArgumentException( "Expected string array, null given" );
            }

            foreach ( var column in columns )
                AddColumn( column );
        }

        public void AddColumn( string column )
        {
            if ( ColumnExists( column ) )
            {
                throw new ArgumentException( $"Column '{column}' is already exists" );
            }

            _data.Add( new Column( column ) );
        }

        public bool ColumnExists( string column ) => GetColumnIndex( column ) > -1;

        public void AddValue( string column, float value ) => AddValue( column, ( double )value );

        public void AddValue( string column, double value )
        {
            if ( !ColumnExists( column ) )
            {
                throw new ArgumentException( $"Column with name {column} doesn't exist." );
            }

            for(int i = 0; i < _data.Count; i++ )
            {
                var temp = 0.0;
                if ( _data[ i ].Name == column ) temp = value;
                _data[ i ].AddValue( temp );
            }
        }

        public void SetValue( string column, int index, float value ) => SetValue( column, index, ( double )value );

        public void SetValue( string column, int index, double value )
        {
            ThrowIfRowIndexIsNotValid( index );

            var columnIndex = GetColumnIndex( column );
            if ( columnIndex < 0 )
            {
                throw new ArgumentException( $"Column {column} doesn't exist" );
            }

            _data[ columnIndex ].SetValue( value, index );
        }

        public void AddRow( float[] values )
        {
            if ( values == null )
            {
                throw new ArgumentException( "Expected float array, null given" );
            }

            int maxIndex = values.Length - 1;
            if ( maxIndex >= _data.Count )
            {
                throw new ArgumentException( "Number of values is more than number of columns" );
            }

            for ( int i = 0; i < _data.Count; i++ )
            {
                double value = i > maxIndex ? 0 : values[ i ];
                _data[ i ].AddValue( value );
            }
        }

        public void AddRow( double[] values )
        {
            if ( values == null )
            {
                throw new ArgumentException( "Expected float array, null given" );
            }

            int maxIndex = values.Length - 1;
            if ( maxIndex >= _data.Count )
            {
                throw new ArgumentException( "Number of values is more than number of columns" );
            }

            for ( int i = 0; i < _data.Count; i++ )
            {
                double value = i > maxIndex ? 0 : values[ i ];
                _data[ i ].AddValue( value );
            }
        }

        public void SetRow( int index, float[] values )
        {
            if ( values == null )
            {
                throw new ArgumentException( "Expected float array, null given" );
            }

            ThrowIfRowIndexIsNotValid( index );

            int maxIndex = values.Length - 1;
            if ( maxIndex >= _data.Count )
            {
                throw new ArgumentException( "Number of values is more than number of columns" );
            }

            for ( int i = 0; i < _data.Count; i++ )
            {
                _data[ i ].SetValue( ( double )values[ i ], index );
            }
        }

        public void SetRow( int index, double[] values )
        {
            if ( values == null )
            {
                throw new ArgumentException( "Expected float array, null given" );
            }

            ThrowIfRowIndexIsNotValid( index );

            int maxIndex = values.Length - 1;
            if(maxIndex >= _data.Count)
            {
                throw new ArgumentException( "Number of values is greater than number of columns" );
            }

            for(int i = 0; i < _data.Count; i++ )
            {
                if ( i > maxIndex ) break;
                _data[ i ].SetValue( values[ i ], index );
            }
        }

        public void SetRoundForAll( int round ) => _data.ForEach( el => el.SetRound( round ) );

        public void SetRound( string column, int round )
        {
            var columnIndex = GetColumnIndex( column );
            if ( columnIndex < 0)
            {
                throw new ArgumentException( $"Column {column} doesn't exist" );
            
            }
            _data[ columnIndex ].SetRound( round );
        }

        public float[] GetRowF( int index )
        {
            ThrowIfRowIndexIsNotValid( index );

            var result = new float[ _data.Count ];
            for ( int i = 0; i < _data.Count; i++ )
                result[ i ] = _data[ i ].GetValueF( index );

            return result;
        }

        public double[] GetRow( int index )
        {
            ThrowIfRowIndexIsNotValid( index );

            var result = new double[ _data.Count ];
            for ( int i = 0; i < _data.Count; i++ )
                result[ i ] = _data[ i ].GetValue( index );

            return result;
        }

        public void Calculate( string column, ITable.TableFunction function )
        {
            if ( function == null)
            {
                throw new ArgumentException( "Expected function, null given" );
            }

            int columnIndex = GetColumnIndex( column );
            if ( columnIndex < 0 )
            {
                throw new ArgumentException( $"Column with name {column} doesn't exist" );
            }

            try
            {
                var props = new Dictionary<string, double>();
                _data.ForEach( el => props.Add( el.Name, 0 ) );

                for ( int i = 0; i < RowCount; i++ )
                {
                    _data.ForEach( el => props[ el.Name ] = el.GetValue( i ) );

                    var value = function( props );

                    _data[ columnIndex ].SetValue( value, i );
                }
            }
            catch(KeyNotFoundException ex)
            {
                throw new ArgumentException( "Given function uses unexisting column(s).\n" + ex.Message );
            }
        }

        public float GetValueF( string column, int index ) => ( float )GetValue( column, index );

        public double GetValue( string column, int index )
        {
            ThrowIfRowIndexIsNotValid( index );

            int columnIndex = GetColumnIndex( column );
            if ( columnIndex < 0 )
            {
                throw new ArgumentException( $"Column {column} doesn't exist" );
            }

            return _data[ columnIndex ].GetValue( index );
        }

        public string PrintValue( string column, int index )
        {
            ThrowIfRowIndexIsNotValid( index );
            var columnIndex = GetColumnIndex( column );
            if ( columnIndex < 0 )
            {
                throw new ArgumentException( $"Column {column} doesn't exist" );
            }

            return Prefix + _data[ columnIndex ].PrintValue( index ) + Postfix;
        }

        public override string ToString()
        {
            var result = "";

            _data.ForEach( el => result += Prefix + el.PrintName() + Postfix + ' ');
            result += "\n";

            for ( int i = 0; i < RowCount; i++ )
            {
                _data.ForEach( el => result += Prefix + el.PrintValue( i ) + Postfix + ' ' );
                result += "\n";
            }

            return result;
        }

        private int GetColumnIndex( string columnName )
        {
            if ( String.IsNullOrEmpty( columnName ) )
            {
                throw new ArgumentException( "Column's name can't be empty" );
            }
            return _data.FindIndex( el => el.Name == columnName );
        }

        private void ThrowIfRowIndexIsNotValid( int index )
        {
            var isValidIndex = index > -1 && index < RowCount;
            if ( !isValidIndex )
            {
                throw new ArgumentException( $"Can't get value from the index {index}: index is out of range" );
            }
        }
    }
}
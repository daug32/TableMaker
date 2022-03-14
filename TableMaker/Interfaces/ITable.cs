using System.Collections.Generic;

namespace TableMaker
{
    public interface ITable
    {
        string Postfix { get; set; }
        string Prefix { get; set; }
        int RowCount { get; }

        /// <summary>
        /// Calculates values for each element in a column by using function
        /// of this type
        /// </summary>
        void Calculate( string targetColumn, TableFunction function );
        /// <summary>
        /// Columns can be auto-calculated via this function type 
        /// </summary>
        delegate double TableFunction( Dictionary<string, double> properties );
        /// <summary>
        /// Sets number of digits after the digit separator for every column
        /// Doesn't affect an accuracy
        /// </summary>
        public void SetRoundForAll( int round );
        /// <summary>
        /// Sets number of digits after the digit separator for this column
        /// Doesn't affect an accuracy
        /// </summary>
        public void SetRound( string column, int round );

        /// <summary>
        /// Creates columns with those names
        /// </summary>
        void AddColumns( string[] columns );
        /// <summary>
        /// Creates a column with this name
        /// </summary>
        void AddColumn( string column );
        /// <summary>
        /// Check whether column with name exists or not
        /// </summary>
        bool ColumnExists( string column );

        /// <summary>
        /// Adds new value for the column
        /// </summary>
        void AddValue( string column, double value );
        /// <summary>
        /// Adds new value for the column
        /// </summary>
        void AddValue( string column, float value );
        /// <summary>
        /// Replace or adds value for the column at the row of the index
        /// </summary>
        void SetValue( string column, int index, double value );
        /// <summary>
        /// Replace or adds value for the column at the row of the index
        /// </summary>
        void SetValue( string column, int index, float value );
        /// <summary>
        /// Get double value column at the index
        /// </summary>
        double GetValue( string column, int index );
        /// <summary>
        /// Get float value column at the index
        /// </summary>
        float GetValueF( string column, int index );
        /// <summary>
        /// Get string formatted with according to Width and Pre/Postfixes
        /// </summary>
        string PrintValue( string column, int index );

        /// <summary>
        /// Adds new row, fills existing columns with values
        /// </summary>
        void AddRow( double[] values );
        /// <summary>
        /// Adds new row, fills existing columns with values
        /// </summary>
        void AddRow( float[] values );
        /// <summary>
        /// Returns a double array of values at row from index
        /// </summary>
        double[] GetRow( int index );
        /// <summary>
        /// Returns a float array of values at row from index
        /// </summary>
        float[] GetRowF( int index );

        /// <summary>
        /// Replace existing row or creates the one at the index and 
        /// fills it with values
        /// </summary>
        void SetRow( int index, double[] values );
        /// <summary>
        /// Replace existing row or creates the one at the index and 
        /// fills it with values
        /// </summary>
        void SetRow( int index, float[] values );

        /// <summary>
        /// Get table in string format
        /// </summary>
        string ToString();
    }
}
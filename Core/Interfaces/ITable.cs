using System.Collections.Generic;

namespace TableMaker
{
    public interface ITable
    {
        string Postfix { get; set; }
        string Prefix { get; set; }

        /// <summary>
        /// Calculates values for each element in a column by using function
        /// of this type
        /// </summary>
        void Calculate(string targetColumn, TableFunction function);
        
        /// <summary>
        /// Columns can be auto-calculated via this function type 
        /// </summary>
        delegate float TableFunction(Dictionary<string, float> properties);
        
        /// <summary>
        /// Sets number of digits after the digit separator for every column
        /// Doesn't affect an accuracy
        /// </summary>
        public void SetRoundForAll(int round);
        
        /// <summary>
        /// Sets number of digits after the digit separator for this column
        /// Doesn't affect an accuracy
        /// </summary>
        public void SetRound(string column, int round);


        /// <summary>
        /// Creates columns with those names
        /// </summary>
        void AddColumns(string[] columns);
        
        /// <summary>
        /// Creates a column with this name
        /// </summary>
        void AddColumn(string column);
        
        /// <summary>
        /// Check whether column with name exists or not
        /// </summary>
        bool ColumnExists(string column);


        /// <summary>
        /// Adds new value for the column
        /// </summary>
        void AddValue(string column, string value);
        
        /// <summary>
        /// Adds new value for the column
        /// </summary>
        void AddValue(string column, float value);


        /// <summary>
        /// Adds new row, fills existing columns with values
        /// </summary>
        void AddRow(string[] values);
        
        /// <summary>
        /// Adds new row, fills existing columns with values
        /// </summary>
        void AddRow(float[] values);


        /// <summary>
        /// Replace existing row or creates the one at the index and 
        /// fills it with values
        /// </summary>
        void SetRow(int index, string[] values);
        
        /// <summary>
        /// Replace existing row or creates the one at the index and 
        /// fills it with values
        /// </summary>
        void SetRow(int index, float[] values);


        /// <summary>
        /// Replace or adds value for the column at the row of the index
        /// </summary>
        void SetValue(string column, int index, string value);
        
        /// <summary>
        /// Replace or adds value for the column at the row of the index
        /// </summary>
        void SetValue(string column, int index, float value);
    }
}
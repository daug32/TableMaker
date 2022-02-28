namespace Learning.Table
{
    public interface ITable
    {
        // Calculates values for each element in a column by using function
        // of this type
        void Calculate(string targetColumn, TableFunction function);
        delegate float TableFunction(Dictionary<string, float> properties);
        // Sets number of digits after the digit separator for every column
        // Doesn't affect an accuracy
        public void SetRoundForAll(int round);
        // Sets number of digits after the digit separator for this column
        // Doesn't affect an accuracy
        public void SetRound(string column, int round);


        // Creates columns with those names
        void AddColumns(string[] columns);
        // Creates a column with this name
        void AddColumn(string column);
        // Creates a column with this name and sets its values
        void AddColumn(string column, List<string> values);
        // Check whether column with name exists or not
        bool ColumnExists(string column);


        // Adds new value for the column
        void AddValue(string column, string value);
        // Adds new value for the column
        void AddValue(string column, float value);


        // Adds new row, fills existing columns with values
        void AddRow(string[] values);
        // Adds new row, fills existing columns with values
        void AddRow(float[] values);


        // Replace existing row or creates the one at the index and 
        // fills it with values
        void SetRow(int index, string[] values);
        // Replace existing row or creates the one at the index and 
        // fills it with values
        void SetRow(int index, float[] values);


        // Replace or adds value for the column at the row of the index
        void SetValue(string column, int index, string value);
        // Replace or adds value for the column at the row of the index
        void SetValue(string column, int index, float value);
    }
}
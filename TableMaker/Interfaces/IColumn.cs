namespace TableMaker
{
    public interface IColumn
    {
        string Name { get; }
        int Width { get; }
        int Round { get; }
        int Count { get; }

        /// <summary>
        /// Sets value at row with the index
        /// </summary>
        void SetValue( double value, int index );

        /// <summary>
        /// Sets value at row with the index
        /// </summary>
        void SetValue( float value, int index );
        /// <summary>
        /// Adds new value at row with the index
        /// </summary>
        void AddValue( double value );
        /// <summary>
        /// Adds new value at row with the index
        /// </summary>
        void AddValue( float value );

        /// <summary>
        /// Returns value from row with the index
        /// </summary>
        double GetValue( int index );
        /// <summary>
        /// Returns value from row with the index
        /// </summary>
        float GetValueF( int index );

        /// <summary>
        /// Returns the column's name which have additional 
        /// spaces to fit the column's Width
        /// </summary>
        string PrintName();
        /// <summary>
        /// Returns the column's value which have additional 
        /// spaces to fit the column's Width
        /// </summary>
        string PrintValue( int index );

        /// <summary>
        /// Sets number of digits after digit separator
        /// </summary>
        void SetRound( int round );
    }
}
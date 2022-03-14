namespace TableMaker
{
    public interface IColumn
    {
        string Name { get; } 
        int Width { get; }
        int Round { get; }

        /// <summary>
        /// Sets or adds new value at row with the index
        /// </summary>
        void SetValue(string value, int index);

        /// <summary>
        /// Sets or adds new value at row with the index
        /// </summary>
        void SetValue(float value, int index);
        
        /// <summary>
        /// Returns value from row with the index
        /// </summary>
        string GetValue(int index);
        
        /// <summary>
        /// Returns value from row with the index
        /// </summary>
        float GetValueF(int index);


        /// <summary>
        /// Returns the column's name which have additional 
        /// spaces to fit the column's Width
        /// </summary>
        string PrintName();
        
        /// <summary>
        /// Returns the column's value which have additional 
        /// spaces to fit the column's Width
        /// </summary>
        string PrintValue(int index);
        
        /// <summary>
        /// Sets the number of digits printing 
        /// after decimal separator
        /// </summary>
        void SetRound(int round);
    }
}
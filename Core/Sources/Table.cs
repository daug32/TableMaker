namespace Learning.Table
{
    public class Table : ITable
    {
        private List<Column> _data;
        private int _count = 0; 
        public string Postfix = "";
        public string Prefix = "";
        public Table()
        {
            _data = new();
        }
        public Table(string[] columns)
        {
            _data = new();
            AddColumns(columns);
        }
        
#region columns
        public void AddColumns(string[] columns)
        {
            var count = columns.Count();
            for(int i = 0; i < count; i++)
            {
                AddColumn(columns[i]);
            }
        }
        public void AddColumn(string column)
        {
            if(ColumnExists(column))
            {
                throw new ArgumentException($"Column '{column}' is already exists");
            }

            _data.Add(new Column(column));
        }
        public void AddColumn(string column, List<string> values)
        {   
            AddColumn(column);
            
            var col = _data.Last();

            int index = 0;
            values.ForEach(el => col.SetValue(el, index++));
        }
        public bool ColumnExists(string column)
        {
            if(String.IsNullOrEmpty(column))
            {
                throw new ArgumentException("Column's name can't be null or empty");
            }

            return !(_data.TrueForAll(el => el.Name != column));
        }
#endregion columns

#region add value(s)
        public void AddValue(string column, string value)
        {
            SetValue(column, _count, value);
        }
        public void AddValue(string column, float value)
        {
            AddValue(column, value.ToString());
        }
        public void AddRow(string[] values)
        {
            SetRow(_count, values);
        }
        public void AddRow(float[] values)
        {
            SetRow(_count, values);
        }
#endregion add value(s)
        
#region set value(s)
        public void SetRow(int index, string[] values)
        {
            var valuesCount = values.Count();
            var columnsCount = _data.Count();
            if(valuesCount > columnsCount)
            {
                throw new ArgumentException("Number of values is more then number of columns");
            }
                
            for(int i = 0; i < valuesCount; i++)
            {
                SetValue(_data[i].Name, index, values[i]);
            }
        }     
        public void SetRow(int index, float[] values)
        {
            var valuesCount = values.Count();
            var columnsCount = _data.Count();
            if(valuesCount > columnsCount)
            {
                throw new ArgumentException("Number of values is more then number of columns");
            }
                
            for(int i = 0; i < valuesCount; i++)
            {
                SetValue(_data[i].Name, index, values[i].ToString());
            }
        }     
        public void SetValue(string column, int index, string value)
        {
            if(!ColumnExists(column))
            {
                throw new ArgumentException("Column doesn't exist");
            }
            if(index < 0) 
            {
                throw new ArgumentException("Index can't be less then 0");
            }
            
            if(_count <= index) _count = index + 1;

            var count = _data.Count();
            for(int i = 0; i < count; i++)
            {
                if(_data[i].Name == column)
                    _data[i].SetValue(value, index);
            }
        }
        public void SetValue(string column, int index, float value)
        {
            SetValue(column, index, value.ToString());
        }
#endregion set value(s)

#region other
        public void Calculate(string targetColumn, ITable.TableFunction function)
        {
            if(!ColumnExists(targetColumn)) 
            {
                throw new ArgumentException("Column doesn't exists");
            }
            
            for(int i = 0; i < _count; i++)
            {
                var props = new Dictionary<string, float>();
                _data.ForEach( el => props.Add(el.Name, el.GetValueF(i)) );
                SetValue(targetColumn, i, function(props));                
            }
        }
        public void SetRoundForAll(int round)
        {
            _data.ForEach(el => el.SetRound(round));
        }
        public void SetRound(string column, int round)
        {
            if(!ColumnExists(column))
            {
                throw new ArgumentException($"Column '{column}' doesn't exist");
            }
            for(int i = 0; i < _data.Count; i++)
            {
                if(_data[i].Name == column) _data[i].SetRound(round);
            }
        }
        public override string ToString()
        {
            var result = "";
            _data.ForEach(el => 
                result += Prefix + el.PrintName() + Postfix + " "
            );
            result += "\n";

            for(int i = 0; i < _count; i++)
            {
                _data.ForEach(el =>
                    result += Prefix + el.PrintValue(i) + Postfix + " "
                );
                result += "\n";
            }

            return result;
        }
#endregion other
    
    }
}
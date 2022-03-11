using System;
using System.Collections.Generic;

namespace Learning.Table
{
    class Column : IColumn
    {
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Round  { get; private set; }
        public string DefaultValue { get; set; }
        private List<string> _values;
        
        public Column(string name)
        {
            if(String.IsNullOrEmpty(name)) 
            {
                throw new ArgumentException("Column's name can't be empty");
            }

            Name = name;
            Width = name.Length;
            _values = new();
            DefaultValue = "0";
        }

        public void SetRound(int round) => Round = round;

        public void SetValue(float value, int index) => SetValue(value.ToString(), index);

        public void SetValue(string value, int index)
        {
            if(index < 0) 
            {
                throw new ArgumentException("Index can't be less then 0");
            }
            
            for(int i = _values.Count; i <= index; i++)
            {
                _values.Add(DefaultValue);
            }
            _values[index] = value;
            
            var temp = GetValueF(index);
            if(Round > 0) temp = MathF.Round(temp, Round);            
            value = temp.ToString();
            if(value.Length > Width) Width = value.Length;
        }

        public string GetValue(int index)
        {
            if(index < 0)
            {
                throw new ArgumentException("Index can't be less then 0");
            }

            var result = DefaultValue;
            if(index < _values.Count) 
            {
                result = _values[index];
            }
            
            return result;
        }

        public float GetValueF(int index)
        {
            var value = GetValue(index);
            var result = StringToFloat(value);
            return result;
        }

        public string PrintName()
        {
            var result = Name;
            for(int i = Name.Length - 1; i < Width; i++)
            {
                result += " ";
            }
            return result;
        }

        public string PrintValue(int index)
        {
            var value = GetValueF(index);

            if(Round > 0) value = MathF.Round(value, Round);

            var str = value.ToString();
            for(int i = str.Length - 1; i < Width; i++)
                str += " ";

            return str;
        }

        private float StringToFloat(string value)
        {
            value = value.Replace(',', '.');
            value = value.Replace("f", "");
            
            var culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";

            float temp = float.Parse(value, culture);
            return temp;
        }
    }
}
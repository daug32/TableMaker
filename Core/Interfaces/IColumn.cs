namespace Learning.Table
{
    public interface IColumn
    {
        void SetValue(string value, int index);
        void SetValue(float value, int index);
        
        string GetValue(int index);
        float GetValueF(int index);

        string PrintName();
        string PrintValue(int index);
        void SetRound(int round);
    }
}
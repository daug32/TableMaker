using System;
using TableMaker;
using System.Collections.Generic;

namespace PhysicLabs
{
    class Program
    {
        public static ITable Table = new Table();

        public static void Main()
        {
            FillTable();
            CalculateValues();
            Console.WriteLine(Table);
        }

        private static void FillTable()
        {
            Table.AddColumns(new string[] {"N", "a", "a1", "a2", "dt", "V1", "U1", "U2", "Ke", "Kv", "W", "F"});

            Table.SetRoundForAll(2);
            Table.SetRound("W", 4);
            Table.SetRound("dt", 6);

            Table.AddRow(new float[] {1, 8,  1.08f, 6.75f, 0.120f * 0.001f});
            Table.AddRow(new float[] {2, 10, 1.50f, 8.16f, 0.114f * 0.001f});
            Table.AddRow(new float[] {3, 12, 2.58f, 9.50f, 0.113f * 0.001f});
        }

        private static void CalculateValues()
        {
            var length = 0.36f;
            var m = 44.3f * 0.001f;

            float Square(float value) => value * value; 
            float Radians(float degrees) => degrees * MathF.PI / 180;
            float CalculateVehicle(float degrees) => 2 * MathF.Sqrt(10 * length) * MathF.Sin(0.5f * Radians(degrees));

            Table.Calculate("V1", ( Dictionary<string, float> p) => CalculateVehicle(p["a"]) );
            Table.Calculate("U1", ( Dictionary<string, float> p) => CalculateVehicle(p["a1"]) );
            Table.Calculate("U2", ( Dictionary<string, float> p) => CalculateVehicle(p["a2"]) );

            Table.Calculate("F",  ( Dictionary<string, float> p) => (p["V1"] - p["U1"]) * m / p["dt"] );
            Table.Calculate("Kv", ( Dictionary<string, float> p) => MathF.Abs( (p["U2"] - p["U1"] ) / p["V1"]) );
            Table.Calculate("Ke", ( Dictionary<string, float> p) => ( Square(p["U2"]) + Square(p["U1"]) ) / Square(p["V1"]) );
            Table.Calculate("W",  ( Dictionary<string, float> p) => 0.25f * m * (Square(p["V1"]) - Square(p["U1"]) - Square(p["U2"])) );

        }
    }
}
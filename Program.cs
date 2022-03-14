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
            var columns = new string[]  {"h",           "d",           "mD",         "t"};
            var row1 = new float[]      {40.1f * 0.01f, 10.4f * 0.01f, 258 * 0.001f, 2.266f}; 
            var row2 = new float[]      {40.1f * 0.01f, 10.4f * 0.01f, 389 * 0.001f, 2.347f};
            var row3 = new float[]      {40.1f * 0.01f, 10.4f * 0.01f, 527 * 0.001f, 2.430f};

            // Basic columns
            Table.AddColumns(columns);
            // Calculatable columns
            Table.AddColumns(new string[] {"J", "P", "T", "100 * P/T"});

            Table.SetRoundForAll(4);

            Table.AddRow(row1);
            Table.AddRow(row2);
            Table.AddRow(row3);
        }

        private static void CalculateValues()
        {
            float mK = 124 * 0.001f;
            float mO =  33 * 0.001f;

            float rD = 0.0435f;
            float rK = 0.0520f;
            float rO = 0.0045f;

            Table.Calculate("J", (Dictionary<string, float> p) => 0.5f * (p["mD"] * (rD*rD + rO*rO) + mK * (rK*rK + rD*rD) + mO * rO*rO)); 

            Table.Calculate("P", (Dictionary<string, float> p) => (p["mD"] + mK + mO) * 10 * p["h"]);
            
            Table.Calculate("T", (Dictionary<string, float> p) => 
            {
                var m = (p["mD"] + mK + mO);
                var dd = (2*rO) * (2*rO);

                var result = 2 * (p["h"] * p["h"]) / (p["t"] * p["t"]);
                System.Console.WriteLine("2 * (hh) / (tt) = result");
                System.Console.WriteLine($"2 * ({ p["h"] } * { p["h"] }) / ({ p["t"] } * { p["t"] }) = { result }");;

                var result2 = m + 4 * p["J"] / dd;
                System.Console.WriteLine("m + 4 * J / dd = result");
                System.Console.WriteLine($"{m} + 4 * { p["J"] } / {dd} = { result2 }");

                System.Console.WriteLine(result * result2);
                System.Console.WriteLine();

                return result * (m + 4 * p["J"] / dd);
            });
            
            Table.Calculate("100 * P/T", (Dictionary<string, float> p) => 100f * p["P"] / p["T"]);
        }
    }
}
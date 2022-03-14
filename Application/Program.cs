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
            Console.WriteLine( Table );
        }

        private static void FillTable()
        {
            Table.AddColumns( new string[] { "N", "a", "a1", "a2", "dt", "V1", "U1", "U2", "Ke", "Kv", "W", "F" } );

            Table.SetRoundForAll( 2 );
            Table.SetRound( "W", 4 );
            Table.SetRound( "dt", 6 );

            Table.AddRow( new double[] { 1, 8, 1.08, 6.75, 0.120 * 0.001 } );
            Table.AddRow( new double[] { 2, 10, 1.50, 8.16, 0.114 * 0.001 } );
            Table.AddRow( new double[] { 3, 12, 2.58, 9.50, 0.113 * 0.001 } );
        }

        private static void CalculateValues()
        {
            var length = 0.36;
            var m = 44.3 * 0.001;

            double Square( double value ) => value * value;
            double Radians( double degrees ) => degrees * Math.PI / 180;
            double CalculateVehicle( double degrees ) => 2 * Math.Sqrt( 10 * length ) * Math.Sin( 0.5f * Radians( degrees ) );

            Table.Calculate( "V1", ( Dictionary<string, double> p ) => CalculateVehicle( p[ "a" ] ) );
            Table.Calculate( "U1", ( Dictionary<string, double> p ) => CalculateVehicle( p[ "a1" ] ) );
            Table.Calculate( "U2", ( Dictionary<string, double> p ) => CalculateVehicle( p[ "a2" ] ) );

            Table.Calculate( "F", ( Dictionary<string, double> p ) => ( p[ "V1" ] - p[ "U1" ] ) * m / p[ "dt" ] );
            Table.Calculate( "Kv", ( Dictionary<string, double> p ) => Math.Abs( ( p[ "U2" ] - p[ "U1" ] ) / p[ "V1" ] ) );
            Table.Calculate( "Ke", ( Dictionary<string, double> p ) => ( Square( p[ "U2" ] ) + Square( p[ "U1" ] ) ) / Square( p[ "V1" ] ) );
            Table.Calculate( "W", ( Dictionary<string, double> p ) => 0.25f * m * ( Square( p[ "V1" ] ) - Square( p[ "U1" ] ) - Square( p[ "U2" ] ) ) );

        }
    }
}   
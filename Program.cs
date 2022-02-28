using Learning.Table;

var table = new Table(
    new string[] {"N", "a", "a1", "a2", "dt", "V1", "U1", "U2", "Ke", "Kv", "W", "F"}
);
table.Prefix = table.Postfix = "|";
table.SetRoundForAll(2);
table.SetRound("W", 4);

table.AddRow(new float[] {1, 8,  1.08f, 6.75f, 0.120f * 0.001f});
table.AddRow(new float[] {2, 10, 1.50f, 8.16f, 0.114f * 0.001f});
table.AddRow(new float[] {3, 12, 2.58f, 9.50f, 0.113f * 0.001f});

#region make calculations

    var length = 0.36f;
    var mass = 44.3f * 0.001f;

    float Square(float value) 
    { 
        return value * value; 
    }
    float CalculateVehicle(float degrees) 
    {
        degrees = degrees * MathF.PI / 180;
        return 2 * MathF.Sqrt(10 * length) * MathF.Sin(0.5f * degrees);
    }
    table.Calculate("V1", (Dictionary<string, float> p) => { return CalculateVehicle(p["a"]); });
    table.Calculate("U1", (Dictionary<string, float> p) => { return CalculateVehicle(p["a1"]); });
    table.Calculate("U2", (Dictionary<string, float> p) => { return CalculateVehicle(p["a2"]); });
    table.Calculate("Ke", (Dictionary<string, float> p) => 
    { 
        return (Square(p["U2"]) + Square(p["U1"])) / Square(p["V1"]);
    });
    table.Calculate("Kv", (Dictionary<string, float> p) => 
    { 
        return MathF.Abs( (p["U2"] - p["U1"]) / p["V1"] );
    });
    table.Calculate("W", (Dictionary<string, float> p) => 
    { 
        return 0.25f * mass * (Square(p["V1"]) - Square(p["U1"]) - Square(p["U2"]));
    });
    table.Calculate("F", (Dictionary<string, float> p) => 
    { 
        return (p["V1"] - p["U1"]) * mass / p["dt"];
    });
#endregion make calculations

Console.WriteLine(table);
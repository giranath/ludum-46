using Godot;
using System;

public class ConsoleWriterDisaster : Disaster
{
    public override bool IsAvailable()
    {
        return true;
    }

    public override void Process()
    {
        GD.Print($"A disaster was printed to the console oh noes!");
    }
}

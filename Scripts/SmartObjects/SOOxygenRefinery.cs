using Godot;
using System;

public class SOOxygenRefinery : SmartObject
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private  bool isOn = false;


    [Export]
    NodePath RoomPath;

    [Export]
    int OnCycle = 120;

    [Export]
    float cycleDuration = 1.0f;

    private TimedRepeater repeater;

    private Room room;

    void RefuelOxygen(Item item)
    {
        GD.Print("Oxygen refinery ON");
        repeater = new TimedRepeater(cycleDuration, OnCycle, GiveOxygen);
        repeater.BindEnded(EndOxygenGeneration);
        item.QueueFree();
    }

    void GiveOxygen(int count)
    {
    }

    void EndOxygenGeneration()
    {
        GD.Print("Oxygen refinery OFF");
    }

    public override void _Process(float delta)
    {
        repeater._Process(delta);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		base._Ready();

        room = GetNode<Room>(RoomPath);

        itemActionMap.Add(itemType.PowerCell, RefuelOxygen);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

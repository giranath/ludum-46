using Godot;
using System;

public class FuelCellGenerator : Node
{
	private TimedRepeater TimedRepeater;
	
	[Export]
	public int SpawnRate = 60;

	[Export]
	NodePath DropLocationPath;

	private Node2D DropLocation;

	PackedScene fuelCellResource;

	private void SpawnFuel(int count)
	{
		FuelCell fuelCell = (FuelCell)fuelCellResource.Instance();
		DropLocation.AddChild(fuelCell);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fuelCellResource = GD.Load<PackedScene>("res://Inventory/FuelCell.tscn");

		TimedRepeater = new TimedRepeater(SpawnRate, 0, SpawnFuel);

		DropLocation = GetNode<Node2D>(DropLocationPath);
	}

	public override void _Process(float delta)
	{
		TimedRepeater._Process(delta);
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

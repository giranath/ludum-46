using Godot;
using Godot.Collections;
using System;

public class SOOxygenRefinery : SmartObject
{
	[Export]
	NodePath RoomPath;

	[Export]
	int OnCycle = 10;

	[Export]
	float cycleDuration = 1.0f;

	[Export]
	float cycleOxygenOutput = 1.0f;

	private TimedRepeater repeater;

	private Room room;

	[Export]
	NodePath buttonPath;

	Button button;

	[Export]
	Array<NodePath> particlePaths;

	Array<Particles2D> particles = new Array<Particles2D>() ;

	void RefuelOxygen(Item item)
	{
		GD.Print("Oxygen refinery ON");

		button.TurnOn();
		showParticle(true);

		repeater = new TimedRepeater(cycleDuration, OnCycle, GiveOxygen);
		repeater.BindEnded(EndOxygenGeneration);
		item.QueueFree();
	}

	void GiveOxygen(int count)
	{
		room.Graph.TryAddPropertyOfRoom(room, "oxygen", cycleOxygenOutput);
	}

	void EndOxygenGeneration()
	{
		GD.Print("Oxygen refinery OFF");
		button.TurnOff();
		showParticle(false);
	}

	public override void _Process(float delta)
	{
		if(repeater != null)
		{
			repeater._Process(delta);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		room = GetNode<Room>(RoomPath);

		itemActionMap.Add(itemType.PowerCell, RefuelOxygen);

		button = GetNode<Button>(buttonPath);
		button.TurnOff();

		foreach(var path in particlePaths)
		{
			var particle = GetNode<Particles2D>(path);
			particles.Add(particle);
		}

		showParticle(false);

	}

	void showParticle(bool show)
	{
		foreach (var particle in particles)
		{
			particle.Emitting = show;
		}
	}
}

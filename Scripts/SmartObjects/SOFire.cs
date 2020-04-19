using Godot;
using System;

public class SOFire : SmartObject
{
	[Export]
	public NodePath RoomPath;

	[Export]
	public float destroyTime;

	[Export]
	public float OxygenCosumptionTime;

	[Export]
	public float oxygenConsumption;

	[Export]
	public NodePath ParticlesPath;

	private TimedRepeater DestroyCooldown;

	private TimedRepeater OxygenEater;
	
	private Particles2D particles;

	private Room Room;

	private int fisted = 10;

	public void PutOut(Item item)
	{
		if (item == null)
		{
			if (fisted == 0)
			{
				fisted = 10;
			}

			gameState.player.Damage(1);

			DestroyCooldown = new TimedRepeater(destroyTime, 1, Elapsed);

			particles.Emitting = false;
		}
		else
		{
			DestroyCooldown = new TimedRepeater(destroyTime, 1, Elapsed);

			particles.Emitting = false;
		}        
	}

	private void Elapsed(int count)
	{
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		itemActionMap.Add(itemType.FireExtinguisher, PutOut);
		itemActionMap.Add(itemType.None, PutOut);
		particles = GetNode<Particles2D>(ParticlesPath);
		Room = GetNode<Room>(RoomPath);

		OxygenEater = new TimedRepeater(OxygenCosumptionTime, 0, ConsumeOxygen);
	}

	public override void _Process(float delta)
	{
		if (DestroyCooldown != null)
		{
			DestroyCooldown._Process(delta);
		}

		if (OxygenEater != null)
		{
			OxygenEater._Process(delta);
		}
	}

	private void ConsumeOxygen(int count)
	{
		if(!particles.Emitting)
		{
			return;
		}


		float currentRoomOxygen = 0.0f;
		if (Room != null)
		{
			Room.Graph.TryGetRoomProperty(Room, "oxygen", out currentRoomOxygen);
		}

		float oxygenChange = -oxygenConsumption;

		bool CanConsume = currentRoomOxygen > 0.0f;
		if (CanConsume)
		{
			float consumedOxygen = Mathf.Min(currentRoomOxygen, oxygenConsumption);
			oxygenChange = -consumedOxygen;
		}

		if (Room != null)
		{
			Room.Graph.TryAddPropertyOfRoom(Room, "oxygen", -oxygenConsumption);
		}
	}
}

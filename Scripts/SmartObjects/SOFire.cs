using Godot;
using System;

public class SOFire : SmartObject, Destroyable
{
	[Export]
	public NodePath RoomPath;

	[Export]
	public float destroyTime;

	[Export]
	public float OxygenCosumptionTime;

	[Export]
	public float oxygenConsumption;

	private TimedRepeater OxygenEater;
	
	private Particles2D particles;

	private Room Room;

	private int fisted = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		particles = GetNode<Particles2D>("./Particles2D");
		Room = GetNode<Room>(RoomPath);

		gameState.destroyables.Add(this);
	}

	public override void _Process(float delta)
	{
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

		if(playerInside)
		{
			gameState.player.Damage(1);
		}

		float currentRoomOxygen = 0.0f;
		if (Room != null)
		{
			Room.Graph.TryGetRoomProperty(Room, "oxygen", out currentRoomOxygen);
		}

		float oxygenChange = -oxygenConsumption;

		bool CanConsume = currentRoomOxygen > 0.0001f;
		if (CanConsume)
		{
			float consumedOxygen = Mathf.Min(currentRoomOxygen, oxygenConsumption);
			oxygenChange = -consumedOxygen;
		}
		else
		{
			FireHydrant extinguisheritem = new FireHydrant();
			Repair(extinguisheritem);
		}

		if (Room != null)
		{
			Room.Graph.TryAddPropertyOfRoom(Room, "oxygen", -oxygenConsumption);
		}
	}

	public void Destroy()
	{
		particles.Emitting = true;
		itemActionMap.Add(itemType.FireExtinguisher, Repair);
		itemActionMap.Add(itemType.None, Repair);
		OxygenEater = new TimedRepeater(OxygenCosumptionTime, 0, ConsumeOxygen);
	}

	public void Repair(Item item)
	{
		if (item == null)
		{
			fisted -= 1;
			gameState.player.Damage(1);

			if (fisted == 0)
			{
				fisted = 10;

				particles.Emitting = false;
				OxygenEater = null;
				itemActionMap.Clear();
			}
		}
		else
		{
			particles.Emitting = false;
			OxygenEater = null;
			itemActionMap.Clear();
		}
	}
}


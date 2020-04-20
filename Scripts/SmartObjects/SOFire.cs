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

	bool hurtPlayer = false;

	enum State
	{
		lit,
		unlit
	}

	State currentState = State.unlit;

	void ChangeState(State newState)
	{
		currentState = newState;

		switch (currentState)
		{
			case State.lit:
				particles.Emitting = true;
				OxygenEater = new TimedRepeater(OxygenCosumptionTime, 0, ConsumeOxygen);
				break;
			case State.unlit:
				particles.Emitting = false;
				OxygenEater = null;
				break;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		itemActionMap.Add(itemType.FireExtinguisher, Repair);
		itemActionMap.Add(itemType.None, Repair);

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

		if(hurtPlayer)
		{
			gameState.player.Damage(5);
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
		ChangeState(State.lit);
	}

	public void Repair(Item item)
	{
		if(currentState == State.lit)
		{
			if (item == null)
			{
				fisted -= 1;
				gameState.player.Damage(1);

				if (fisted == 0)
				{
					fisted = 10;

					ChangeState(State.unlit);
					itemActionMap.Clear();

                    gameState.uiManager.DialogUI.SetText(3, "You punched the fire extinguish it, ouch.", Colors.Yellow);
				}
			}
			else
			{
				ChangeState(State.unlit);
                gameState.uiManager.DialogUI.SetText(3, "You use the fire extinguisher to stop the fire.", Colors.Green);
            }
		}
	}
	
	private void bodyEntered(object body)
	{
		var characterMovement = ((Node)body) as CharacterMovement;

		if (characterMovement != null)
		{
			hurtPlayer = true;
		}
	}
	
	private void bodyExited(object body)
	{
		var characterMovement = ((Node)body) as CharacterMovement;

		if (characterMovement != null)
		{
			hurtPlayer = false;
		}
	}
}





using Godot;
using System;

public class CharacterMovement : KinematicBody2D
{
	private CharacterStates CharacterState { get; set; } = new CharacterStateIdle();
	private Inventory inventory;

	private TimedRepeater oxygenTimer;


	[Signal]
	public delegate void OxygenChanged(float oxygenValue);

	[Signal]
	public delegate void HealthChaged(float healthValue);

	[Export]
	public float oxygenTimerDuration = 1.0f;

	[Export]
	public float maxHealth = 100.0f;

	public float health = 100.0f;

	[Export]
	public float oxygenConsumption = 1.0f;

	[Export]
	public float maxOxygen = 100.0f;

	float oxygen = 100.0f;

	[Export]
	public float RunSpeed;
	
	[Export]
	public float ClimbingSpeed;
	
	[Export]
	public float JumpForce;

	public int Climb = 0;

	public Vector2 Velocity;

	private bool FloatCompare(float a, float b, float epsilon = 0.00001f )
	{
		return Math.Abs(a - b) <= epsilon;
	}

	private void ConsumeOxygen(int count)
	{
		if (FloatCompare(oxygen, 0))
		{
			Damage(1.0f);
		}
		else
		{

			float oxygenChange = -oxygenConsumption;

			//Todo try consuming room oxygen
			bool CanConsume = true;
			if (CanConsume)
			{
				oxygenChange = oxygenConsumption;
			}
			oxygen = Mathf.Clamp(oxygen + oxygenConsumption, 0.0f, maxOxygen);

			EmitSignal(nameof(OxygenChanged), oxygen);
		}
	}

	public void Damage(float damage)
	{
		GD.Print("Damage : " + damage.ToString());
		health = Mathf.Clamp(health - damage, 0.0f, maxHealth);

		EmitSignal(nameof(HealthChaged), health);
		
		if (FloatCompare(health, 0))
		{
			Dead();
		}
	}


	private void Dead()
	{
		GD.Print("Dead ouch!");
	}


	public Inventory GetInventory()
	{
		return inventory;
	}

	public KinematicBody2D GetBody()
	{
		return this;
	}

	public override void _Ready()
	{
		GameState gamestate = GetNode<GameState>("/root/GameState");
		gamestate.player = this;

		inventory = GetNode<Inventory>("./Inventory");

		oxygenTimer = new TimedRepeater(oxygenTimerDuration, 0, ConsumeOxygen);

	}

	public bool CanClimb()
	{
		if(Climb > 0)
		{
			return true;
		}

		return false;
	}

	public override void _Process(float delta)
	{
		oxygenTimer._Process(delta);
		HandleAnimation();
	}

	public override void _PhysicsProcess(float delta)
	{
		Velocity.x = 0;

		CharacterState = CharacterState.HandleInput(this, delta);
	}

	public void HandleAnimation()
	{

	}

	public Item GetItemInHand(Inventory.Hand hand) {
		return inventory.GetItemInHand(hand);
	}

	public Item GetItemInCurrentHand() {
		return inventory.GetCurrentItem();
	}
}

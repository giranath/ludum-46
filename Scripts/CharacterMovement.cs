using Godot;
using System;

public class CharacterMovement : KinematicBody2D
{
	private CharacterStates CharacterState { get; set; } = new CharacterStateIdle();
	private Inventory inventory;

	[Export]
	public float RunSpeed;
	
	[Export]
	public float ClimbingSpeed;
	
	[Export]
	public float JumpForce;

	public int Climb = 0;

	public Vector2 Velocity;

	public KinematicBody2D GetBody()
	{
		return this;
	}

	public override void _Ready()
	{
        GameState gamestate = GetNode<GameState>("/root/GameState");
        gamestate.player = this;

		inventory = GetNode<Inventory>("./Inventory");
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

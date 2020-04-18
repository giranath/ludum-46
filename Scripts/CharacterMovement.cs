using Godot;
using System;

public class CharacterMovement : Node2D
{
	private CharacterStates CharacterState { get; set; } = new CharacterStateIdle();
		
	[Export]
	public NodePath Body;

	public bool CanClimb = false;

	public Vector2 Velocity;

	public override void _Ready()
	{
		CanClimb = false;
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
}

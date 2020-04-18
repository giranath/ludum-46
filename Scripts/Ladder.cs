using Godot;
using System;

public class Ladder : Node
{
	public override void _Ready()
	{

	}

	public void OnBodyEntered(object body)
	{
		var characterMovement = ((Node)body).GetParent() as CharacterMovement;

		if (characterMovement != null)
		{
			characterMovement.CanClimb = true;
		}
	}
	public void OnBodyExited(object body)
	{
		var characterMovement = ((Node)body).GetParent() as CharacterMovement;

		if (characterMovement != null)
		{
			characterMovement.CanClimb = false;
		}
	}
}

using Godot;
using System;

public class Ladder : Node
{
	public override void _Ready()
	{

	}

	public void OnBodyEntered(object body)
	{
		var characterMovement = (CharacterMovement)((Node)body).GetParent();

		if (characterMovement != null)
		{
			characterMovement.CanClimb = true;
		}
	}
	public void OnBodyExited(object body)
	{
		var characterMovement = (CharacterMovement)((Node)body).GetParent();

		if (characterMovement != null)
		{
			characterMovement.CanClimb = false;
		}
	}
}

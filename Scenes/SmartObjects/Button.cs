using Godot;
using System;

public class Button : Sprite
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void TurnOn()
	{
		Modulate = new Color(0, 1, 0);
	}
	
	public  void TurnOff()
	{
		Modulate = new Color(1, 0, 0);
	}
	
}

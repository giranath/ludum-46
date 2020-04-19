using Godot;
using System;

public class ToolBox : Item
{
	
	ToolBox()
	{
		Type = itemType.ToolBox;
	}
	
	public override void act()
	{
		GD.Print("Use toolBox");
	}
}

using Godot;
using System;

public class testArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	[Signal]
	public delegate void AreaClicked();
	
	private void _on_ClickArea_input_event(object viewport, object @event, int shape_idx)
{
	if (@event is InputEventMouseButton btn)
		{
			if(btn.ButtonIndex == (int)ButtonList.Left && btn.IsPressed())
			{
				GD.Print("SO Clicked");
				EmitSignal(nameof(AreaClicked));
			}
		}
	
}
}




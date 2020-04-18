using Godot;
using System;

public class DialogUI : Node
{
	[Export]
	public NodePath DialogLabelPath;

	private Label DialogLabel;

	private TimedRepeater TimedRepeater;
	
	public override void _Ready()
	{
		DialogLabel = GetNode<Label>(DialogLabelPath);
	}
	public override void _Process(float delta)
	{
		if(TimedRepeater != null)
		{
			TimedRepeater._Process(delta);
		}
	}

	public void SetText(float timeShown, string text)
	{
		if(TimedRepeater == null)
		{
			DialogLabel.Text = text;
			TimedRepeater = new TimedRepeater(timeShown, 1, TimerExpired);
		}
	}

	public void TimerExpired(int count)
	{
		DialogLabel.Text = "";
		TimedRepeater = null;
	} 
}

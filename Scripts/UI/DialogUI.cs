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
        GD.Print("1");
        if (TimedRepeater == null)
        {
            GD.Print("2");
            DialogLabel.Text = text;
			TimedRepeater = new TimedRepeater(timeShown, 1, TimerExpired);
		}
	}

	public void TimerExpired(int count)
    {
        GD.Print("3");
        DialogLabel.Text = "";
		TimedRepeater = null;
	} 
}

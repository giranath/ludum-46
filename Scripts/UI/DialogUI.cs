using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class DialogUI : Node
{
	[Export]
	public NodePath DialogLabelPath;

	private Label DialogLabel;

	private TimedRepeater TimedRepeater;

	private Dictionary<string, float> messageQueue = new Dictionary<string, float>();
	
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
		else
		{
			if(messageQueue.Count != 0)
			{
				var message = messageQueue.First();

				SetText(message.Value, message.Key);
				messageQueue.Remove(message.Key);
			}
		}
	}

	public void SetText(float timeShown, string text)
	{
		if (TimedRepeater == null && messageQueue.Count == 0)
		{
			DialogLabel.Text = text;
			TimedRepeater = new TimedRepeater(timeShown, 1, TimerExpired);
		}
		else
		{
			if(!messageQueue.ContainsKey(text))
			{ 
				messageQueue.Add(text, timeShown);
			}
		}
	}

	public void TimerExpired(int count)
	{
		DialogLabel.Text = "";
		TimedRepeater = null;
	} 
}

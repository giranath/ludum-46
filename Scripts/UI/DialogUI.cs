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

	private List<Message> messageQueue = new List<Message>();
	
	private class Message
	{
		public String message;

		public float time;

		public Color color;
	}

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

				ShowMessage(message);
				messageQueue.Remove(message);
			}
		}
	}

	private void ShowMessage(Message message)
	{
		if (TimedRepeater == null)
		{
			DialogLabel.Text = message.message;
			DialogLabel.AddColorOverride("font_color", message.color);
			TimedRepeater = new TimedRepeater(message.time, 1, TimerExpired);
		}
	}

	public void SetText(float timeShown, string text, Color color)
	{
		if(!messageQueue.Any(x => x.message.Equals(text)))
		{ 
			messageQueue.Add(new Message 
			{ 
				message = text,
				color = color,
				time = timeShown
			});
		}
	}

	public void TimerExpired(int count)
	{
		DialogLabel.Text = "";
		TimedRepeater = null;
	} 
}

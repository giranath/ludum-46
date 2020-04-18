using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class DisasterManager : Node
{
	[Export]
	public Array<NodePath> DisastersPath;

	[Export]
	public float DisasterCooldown;

	private List<Disaster> Disasters;

	private TimedRepeater TimedRepeater;

	public override void _Ready()
	{
		if(DisastersPath.Count == 0)
		{
			GD.PrintErr($"{nameof(DisasterManager)} : {this.GetPath()} - Does not have any disaster =(");
		}
		else
		{
			Disasters = new List<Disaster>(DisastersPath.Count);

			foreach (var path in DisastersPath)
			{
				var disaster = GetNode<Disaster>(path);

				if (disaster != null)
				{
					Disasters.Add(disaster);
				}
				else
				{
					GD.PrintErr($"{this.GetPath()} - Could not load disaster {path}");
				}
			}

			TimedRepeater = new TimedRepeater(DisasterCooldown, 0, LaunchDisaster);
		}
	}
	public override void _Process(float delta)
	{
		TimedRepeater._Process(delta);
	}

	public void LaunchDisaster(int count)
	{
		var rand = new RandomNumberGenerator();

		Disaster disaster = null;

		do
		{
			var i = rand.RandiRange(0, Disasters.Count - 1);
			disaster = Disasters[i];
		} while (!disaster.IsAvailable());

		disaster.Process();
	}

}

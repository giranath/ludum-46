using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class DisasterManager : Node
{
	[Export]
	public Array<NodePath> DisastersPath;

	[Export]
	public float DisasterCooldownMin = 30.0f;

	[Export]
	public float DisasterCooldownMax = 80.0f;

    [Signal]
    public delegate void OnDisasterApplied();

	private List<Disaster> Disasters;

	private TimedRepeater TimedRepeater;

	RandomNumberGenerator rand = new RandomNumberGenerator();

	public UIManager uiManager = null;

	public GameState gameState = null;

	public override void _Ready()
	{
        rand.Seed = Seed.Create();
		gameState = GetNode<GameState>("/root/GameState");

		if (DisastersPath.Count == 0)
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

			TimedRepeater = new TimedRepeater(rand.RandfRange(DisasterCooldownMin, DisasterCooldownMax), 1, LaunchDisaster);
		}
	}
	public override void _Process(float delta)
	{
		TimedRepeater._Process(delta);
	}

	public void LaunchDisaster(int count)
	{

		Disaster disaster = null;
		do
		{
			var i = rand.RandiRange(0, Disasters.Count - 1);
			disaster = Disasters[i];
		} while (!disaster.IsAvailable());


		gameState.uiManager.DialogUI.SetText(5, "A disaster happened in the ship", Colors.Red);
		disaster.Process();
        EmitSignal(nameof(OnDisasterApplied));

		TimedRepeater = new TimedRepeater(rand.RandfRange(DisasterCooldownMin, DisasterCooldownMax), 1, LaunchDisaster);
	}

}

using Godot;
using System;

public class SOMedicalStation : SmartObject
{
	[Export]
	public float cooldown;

	[Export]
	public NodePath CooldownSpritePath;

	private Node2D CooldownSprite;

	private TimedRepeater StationCooldown;

	public void heal(Item item)
	{
		GameState gamestate = GetNode<GameState>("/root/GameState");
		string CurrentHealth = "";

		if(gamestate.player.health <= 25)
		{
			CurrentHealth = "You appear to be dieing.";
		}

		if (gamestate.player.health <= 50)
		{
			CurrentHealth = "You appear to be hurt.";
		}

		if (gamestate.player.health <= 70)
		{
			CurrentHealth = "You appear to be fine.";
		}

		if (gamestate.player.health > 99)
		{
			CurrentHealth = "You are in perfect shape and form.";
		}

		if (StationCooldown == null)
		{ 
			if(item != null)
			{ 
				gamestate.player.Damage(-25f);

				StationCooldown = new TimedRepeater(cooldown, 1, CooldownElapsed);
				CooldownSprite.Visible = true;
			}

			gamestate.uiManager.DialogUI.SetText(2, $"{CurrentHealth}");
		}
		else
		{
			gamestate.uiManager.DialogUI.SetText(2, $"You cannot reuse the station so soon. {CurrentHealth}");
		}
	}

	private void CooldownElapsed(int count)
	{
		StationCooldown = null;
		CooldownSprite.Visible = false;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		itemActionMap.Add(itemType.Syringe, heal);
		itemActionMap.Add(itemType.None, heal);
		CooldownSprite = GetNode<Node2D>(CooldownSpritePath);
	}

	public override void _Process(float delta)
	{
		if(StationCooldown != null)
		{ 
			StationCooldown._Process(delta);
		}
	}
}

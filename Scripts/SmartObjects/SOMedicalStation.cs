using Godot;
using System;

public class SOMedicalStation : SmartObject
{
	[Export]
	public float cooldown;

	[Export]
	public NodePath CooldownSpritePath;
	
	[Export]
	public NodePath ParticlesPath;

	private Node2D CooldownSprite;

	private TimedRepeater StationCooldown;

	private TimedRepeater ParticlesSwitcher;

	private Particles2D Particles;

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
				ParticlesSwitcher = new TimedRepeater(1, 1, ParticleSwitch);
				CooldownSprite.Visible = true;
			}

			gamestate.uiManager.DialogUI.SetText(2, $"{CurrentHealth}");
			Particles.Emitting = true;

		}
		else
		{
			gamestate.uiManager.DialogUI.SetText(2, $"You cannot reuse the station so soon. {CurrentHealth}");
		}
	}

	private void ParticleSwitch(int count)
	{
		ParticlesSwitcher = null;
		Particles.Emitting = false;
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
		Particles = GetNode<Particles2D>(ParticlesPath);
	}

	public override void _Process(float delta)
	{
		if(StationCooldown != null)
		{ 
			StationCooldown._Process(delta);
		}

		if (ParticlesSwitcher != null)
		{
			ParticlesSwitcher._Process(delta);
		}
	}
}

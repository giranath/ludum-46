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
		if (StationCooldown == null)
		{ 
			gameState.player.Damage(-25f);

			StationCooldown = new TimedRepeater(cooldown, 1, CooldownElapsed);
			ParticlesSwitcher = new TimedRepeater(1, 1, ParticleSwitch);
			CooldownSprite.Visible = true;
            Particles.Emitting = true;
            gameState.uiManager.DialogUI.SetText(2, $"You bandage yourself.", Colors.Green);
		}
		else
		{
			gameState.uiManager.DialogUI.SetText(2, $"The station has not refilled yet.", Colors.White);
		}
	}

    public void healthCheck(Item item)
    {
        gameState.uiManager.DialogUI.SetText(2, $"{GetCurrentHealth()}", Colors.White);
    }

    private string GetCurrentHealth()
    {
        if (gameState.player.health >= 99)
        {
            return "You are in perfect shape and form.";
        }

        if (gameState.player.health <= 25)
        {
            return "You appear to be dieing.";
        }

        if (gameState.player.health <= 50)
        {
            return "You appear to be hurt.";
        }

        if (gameState.player.health < 99)
        {
            return "You appear to be fine.";
        }



        return "You are unsure wether you are alive or not";
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

    public void robot(Item item)
    {
        gameState.uiManager.DialogUI.SetText(3, "Maybe if I was a robot.", Colors.White);
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		itemActionMap.Add(itemType.Syringe, heal);
		itemActionMap.Add(itemType.None, healthCheck);
        itemActionMap.Add(itemType.ToolBox, robot);
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

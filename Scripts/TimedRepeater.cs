using System;

public class TimedRepeater
{
	public Action<int> OnCooldown;

	private float Cooldown;

	private float Current;

	private int Count = 0;

	private int Repeatition;

	private Action endAction;

	private bool done = false;

	public TimedRepeater(float cooldown, int repeat = 0, Action<int> onCooldown = null)
	{
		if (onCooldown != null)
		{
			OnCooldown += onCooldown;
		}

		Cooldown = cooldown;
		Repeatition = repeat;
		Current = Cooldown;
	}

	public void _Process(float delta)
	{
		Current -= delta;

		if (IsActive())
		{
			Count += 1;
			OnCooldown.Invoke(Count);
			Current = Cooldown;

			done = IsDone();
			if(done)
			{
				endAction?.Invoke();
			}
		}
	}

	private bool IsActive()
	{
		if(!done && (Repeatition == 0 || Count <= Repeatition) && Current <= 0)
		{
			return true;
		}

		return false;
	}

	private bool IsDone()
	{
		if ((Repeatition != 0 && Count == Repeatition))
		{
			return true;
		}

		return false;
	}

	public void BindEnded(Action endedAction)
	{
		endAction = endedAction;
	}
}

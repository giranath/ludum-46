using System;

public class TimedRepeater
{
    public Action<int> OnCooldown;

    private float Cooldown;

    private float Current;

    private int Count = 0;

    private int Repeatition;

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

        if (ShouldRepeat() && Current <= 0)
        {
            Count += 1;
            OnCooldown.Invoke(Count);
            Current = Cooldown;
        }
    }

    private bool ShouldRepeat()
    {
        if(Repeatition == 0)
        {
            return true;
        }

        if(Count <= Repeatition)
        {
            return true;
        }

        return false;
    }
}

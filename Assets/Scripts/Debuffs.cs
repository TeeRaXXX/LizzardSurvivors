using System.Collections.Generic;

public class Debuffs
{
    public List<IDebuff> DebuffsList { get; private set; }

    public Debuffs() 
    {
        DebuffsList = new List<IDebuff>();
    }

    public void AddDebuff(IDebuff debuff)
    {
        if (debuff.IsStackable())
            DebuffsList.Add(debuff);
        else if (!DebuffsList.Contains(debuff))
            DebuffsList.Add(debuff);
    }

    public void RemoveDebuff(IDebuff debuff)
    {
        DebuffsList.Remove(debuff);
    }
}
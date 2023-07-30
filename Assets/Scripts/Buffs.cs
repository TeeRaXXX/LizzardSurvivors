using System.Collections.Generic;

public class Buffs
{
    public List<IBuff> BuffsList { get; private set; }

    public Buffs()
    {
        BuffsList = new List<IBuff>();
    }

    public void AddBuff(IBuff buff)
    {
        if (buff.IsStackable())
            BuffsList.Add(buff);
        else if (!BuffsList.Contains(buff))
            BuffsList.Add(buff);
    }

    public void RemoveBuff(IBuff buff)
    {
        BuffsList.Remove(buff);
    }
}
using UnityEngine;

public interface IBuff
{
    public void InitBuff(BuffDebuffInfoSO info);
    public Sprite GetLogo();
    public string GetName();
    public string GetDescription();
    public bool IsStackable();
    public void Start();
    public void End();
}
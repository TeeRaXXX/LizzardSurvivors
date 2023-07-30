using UnityEngine;

public interface IDebuff
{
    public void InitDebuff(BuffDebuffInfoSO info);
    public Sprite GetLogo();
    public string GetName();
    public string GetDescription();
    public bool IsStackable();
    public void Start();
    public void End();
}
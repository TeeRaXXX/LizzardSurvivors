public interface IUpgradable
{
    void Upgrade(bool isNewLevel);

    int GetMaxLevel();

    int GetCurrentLevel();
}
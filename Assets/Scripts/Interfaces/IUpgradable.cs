public interface IUpgradable
{
    void Upgrade(bool isNewLevel);

    void Initialize(int playerIndex);

    int GetMaxLevel();

    int GetCurrentLevel();
}
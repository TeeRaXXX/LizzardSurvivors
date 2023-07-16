public class GlobalBonuses
{
    private int _additionalProjectilesCount;
    private static GlobalBonuses _instance = null;

    public static GlobalBonuses Instance
    {   
        get
        {
            if (_instance == null)
            {
                _instance = new GlobalBonuses();

                _instance._additionalProjectilesCount = 0;
            }

            return _instance;
        }
        private set { }
    }

    public void UpdateAdditionalProjectilesCount(int count)
    {
        _additionalProjectilesCount = count;
        EventManager.OnProjectilesUpdateEvent(false);
    }

    public int GetAdditionalProjectilesCount() => _additionalProjectilesCount;
}
public class GlobalBonuses
{
    private int _additionalProjectilesCount;
    private float _additionalAoeRadius;

    private static GlobalBonuses _instance = null;

    public static GlobalBonuses Instance
    {   
        get
        {
            if (_instance == null)
            {
                _instance = new GlobalBonuses();

                _instance._additionalProjectilesCount = 0;
                _instance._additionalAoeRadius = 1f;
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

    public void UpdateAdditionalAoeRadius(float count)
    {
        _additionalAoeRadius = count;
        EventManager.OnAoeUpdateEvent(false);
    }

    public int GetAdditionalProjectilesCount() => _additionalProjectilesCount;

    public float GetAdditionalAoeRadius() => _additionalAoeRadius;
}
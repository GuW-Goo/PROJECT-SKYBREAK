public class ChoiceData
{
    public WeaponDataSO WeaponData { get; private set; }
    public StatDataSO StatData { get; private set; }

    public ChoiceData(WeaponDataSO weapon, StatDataSO stat)
    {
        WeaponData = weapon;
        StatData = stat;
    }
}
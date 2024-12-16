public class PlayerIngameController : CharacterIngameController
{
    protected CharacterStatsSystem _stats;

    public override void Setup()
    {
        base.Setup();
        _stats = GetComponent<CharacterBlank>().Stats;
    }

    public void UpDownAttribute(string attributeName, bool increase)
    {
        Stat stat = (Stat)_stats.GetType().GetProperty(attributeName).GetValue(_stats);
        _stats.UpDownAttribute(stat, increase);
    }
}

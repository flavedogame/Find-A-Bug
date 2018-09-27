using SQLite4Unity3d;

public class PersistentObjectFunction
{
    [PrimaryKey]
    public string identifier { get; set; }
    public bool isViewed { get; set; }
    public override string ToString()
    {
        return string.Format("[Currency: identifier = {0}, amount = {1}", identifier, isViewed);
    }
}


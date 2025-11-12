using SQLite4Unity3d;

public class History
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }
    public int Score { get; set; }
}

namespace Lab13.Domain;

public class Game:Entity<long>
{
    public long Team1 { get; set; }
    public long Team2 { get; set; }
    public DateTime StartDate { get; set; }

    public Game(long team1, long team2, DateTime startDate)
    {
        Team1 = team1;
        Team2 = team2;
        StartDate = startDate;
    }
}
namespace Lab13.Domain;

public class Player: Student
{
    public long Team { get; set; }

    public Player(String name,String school, long team):base(name,school)
    {
        Team = team;
    }
    public override string ToString()
    {
        return base.ToString();
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
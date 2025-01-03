namespace Lab13.Domain;

public class Team :Entity<long>
{
    public String Name { get; set; }

    public Team(String name)
    {
        Name = name;
    }

    public override String ToString()
    {
        return "Nume echipa: " + Name;
    }
    
}
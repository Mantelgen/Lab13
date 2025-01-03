namespace Lab13.Domain;

public class Student: Entity<long>
{
    public String Name{ get;set; }
    public String School { get; set; }

    public Student(String name, String school)
    {
        Name = name;
        School = school;
    }
    public override String ToString()
    {
        return "Nume elev: " + Name+"; Scoala: "+ School;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }
}
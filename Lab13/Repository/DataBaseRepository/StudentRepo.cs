using Lab13.Domain;
using Lab13.Domain.Validators;
using Npgsql;

namespace Lab13.Repository.DataBaseRepository;

public class StudentRepo:AbstractDBRepository<long,Student>
{
    public StudentRepo(IValidator<Student> vali, string host, string username, string password, string database, int port, string tableName) : base(vali, host, username, password, database, port, tableName)
    {
    }

    protected override Student createEntityFromResultSet(NpgsqlDataReader resultSet)
    {
        long id = long.Parse(resultSet["id"].ToString());
        String name = resultSet["name"].ToString();
        String school = resultSet["school"].ToString();
        
        Student student = new Student(name, school);
        student.ID = id;
        return student;
    }

    protected override string getTableInsertValuesSQL(Student Entity)
    {
        return TableName+"(name,school)";
    }

    protected override string getSQLIdForEntityId(long id)
    {
        return "id =" + id;
    }

    protected override string getSQLValuesForEntity(Student entity)
    {
        return "('"+ entity.Name+"', '"+ entity.School+"')";
    }

    protected override string getSQLValuesForEntityUpdate(Student entity)
    {
        throw new NotImplementedException();
    }
}
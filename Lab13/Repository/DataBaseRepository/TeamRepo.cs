using Lab13.Domain;
using Lab13.Domain.Validators;
using Npgsql;

namespace Lab13.Repository.DataBaseRepository;

public class TeamRepo:AbstractDBRepository<long,Team>
{
    public TeamRepo(IValidator<Team> vali, string host, string username, string password, string database, int port, string tableName) : base(vali, host, username, password, database, port, tableName)
    {
    }

    protected override Team createEntityFromResultSet(NpgsqlDataReader resultSet)
    {
        long id = long.Parse(resultSet["id"].ToString());
        String name = resultSet["name"].ToString();
        
        Team team = new Team(name);
        team.ID = id;
        return team;
    }

    protected override string getTableInsertValuesSQL(Team Entity)
    {
        return TableName+"(name)";
    }

    protected override string getSQLIdForEntityId(long id)
    {
        return "id =" + id;
    }

    protected override string getSQLValuesForEntity(Team entity)
    {
        return "('"+ entity.Name+"')";
    }

    protected override string getSQLValuesForEntityUpdate(Team entity)
    {
        throw new NotImplementedException();
    }
}
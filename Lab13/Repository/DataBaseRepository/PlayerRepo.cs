using Lab13.Domain;
using Lab13.Domain.Validators;
using Npgsql;

namespace Lab13.Repository.DataBaseRepository;

public class PlayerRepo:AbstractDBRepository<long,Player>
{
    public PlayerRepo(IValidator<Player> vali, string host, string username, string password, string database, int port, string tableName) : 
        base(vali, host, username, password, database, port, tableName) {}

    protected override Player createEntityFromResultSet(NpgsqlDataReader resultSet)
    {
        long id = long.Parse(resultSet["id"].ToString());
        String name = resultSet["name"].ToString();
        String school = resultSet["school"].ToString();
        long teamid = long.Parse(resultSet["teamid"].ToString());
        
        Player player = new Player(name,school,teamid);
        player.ID = id;
        return player;
    }

    protected override string getTableInsertValuesSQL(Player Entity)
    {
        return TableName+"(name,school,teamid)";
    }

    protected override string getSQLIdForEntityId(long id)
    {
        return "id =" + id;
    }

    protected override string getSQLValuesForEntity(Player entity)
    {
        return "('"+ entity.Name+"', '"+ entity.School+ "', '"+entity.Team+"')";
    }

    protected override string getSQLValuesForEntityUpdate(Player entity)
    {
        throw new NotImplementedException();
    }
}
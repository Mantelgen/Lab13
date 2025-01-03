using Lab13.Domain;
using Lab13.Domain.Validators;
using Npgsql;

namespace Lab13.Repository.DataBaseRepository;

public class GamesRepo: AbstractDBRepository<long, Game>
{
    public GamesRepo(IValidator<Game> vali, string host, string username, string password, string database, int port, string tableName) :
        base(vali, host, username, password, database, port, tableName) {}

    protected override Game createEntityFromResultSet(NpgsqlDataReader resultSet)
    {
        long id = long.Parse(resultSet["id"].ToString());
        long id1 = long.Parse(resultSet["teamid1"].ToString());
        long id2 = long.Parse(resultSet["teamid2"].ToString());
        DateTime date = DateTime.Parse(resultSet["date"].ToString());
        
        Game game = new Game(id1, id2, date);
        game.ID = id;
        return game;
    }

    protected override string getTableInsertValuesSQL(Game Entity)
    {
        return TableName+"(idplayer,idgame,nrpoints,playertype)";
    }

    protected override string getSQLIdForEntityId(long id)
    {
        return "id =" + id;
    }

    protected override string getSQLValuesForEntity(Game entity)
    {
        return "('"+ entity.Team1+"', '"+ entity.Team2+ "', '"+entity.StartDate.ToString()+"')";
    }

    protected override string getSQLValuesForEntityUpdate(Game entity)
    {
        throw new NotImplementedException();
    }
}
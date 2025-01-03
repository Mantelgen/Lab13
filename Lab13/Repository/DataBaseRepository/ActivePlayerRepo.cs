using Lab13.Domain;
using Lab13.Domain.Validators;
using Npgsql;

namespace Lab13.Repository.DataBaseRepository;

public class ActivePlayerRepo: AbstractDBRepository<long, ActivePlayer>
{
    public ActivePlayerRepo(IValidator<ActivePlayer> vali, string host, string username, string password, string database, int port, string tableName) : 
        base(vali, host, username, password, database, port, tableName){}

    protected override ActivePlayer createEntityFromResultSet(NpgsqlDataReader resultSet)
    {
        
        long id = long.Parse(resultSet["id"].ToString());
        int idplayer = int.Parse(resultSet["idplayer"].ToString());
        int idgame = int.Parse(resultSet["idgame"].ToString());
        int nrPoints = int.Parse(resultSet["nrpoints"].ToString());
        PlayerType playerType = (resultSet["playertype"].ToString()=="Rezerva")?PlayerType.Rezerva:PlayerType.Participant;
        
        ActivePlayer player = new ActivePlayer(idplayer, idgame, nrPoints, playerType);
        player.ID = id;
        return player;
    }

    protected override string getTableInsertValuesSQL(ActivePlayer Entity)
    {
        return TableName+"(idplayer,idgame,nrpoints,playertype)";
    }

    protected override string getSQLIdForEntityId(long id)
    {
        return "id =" + id;
    }

    protected override string getSQLValuesForEntity(ActivePlayer entity)
    {
        return "('"+ entity.IDPlayer+"', '"+ entity.IDGame+ "', '"+entity.NrPoints+"', '"+entity.PlayerType+"')";
    }

    protected override string getSQLValuesForEntityUpdate(ActivePlayer entity)
    {
        throw new NotImplementedException();
    }
}
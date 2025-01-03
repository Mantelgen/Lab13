using Lab13.Domain;
using Lab13.Domain.Validators;
using Npgsql;

namespace Lab13.Repository.DataBaseRepository;

public abstract class AbstractDBRepository<ID,E>: IRepository<ID, E> where E : Entity<ID>
{
    protected IValidator<E> vali;
    
    protected String Host;//="localhost";
    protected String Username;//="postgres";
    protected String Password;//="cosmin2004";
    protected String Database;//="NBALeagueRomania";
    protected int Port;//=5433;

    protected String TableName;
    protected NpgsqlConnection Connection;
    

    protected AbstractDBRepository(IValidator<E> vali, string host, string username, string password, string database, int port, String tableName)
    {
        TableName = tableName;
        this.vali = vali;
        
        Host = host;
        Username = username;
        Password = password;
        Database = database;
        Port = port;
        
        String ConnectionString="Host="+Host +";Username="+Username + ";Password="+Password+";Database="+Database+";Port="+Port;
        Connection = new NpgsqlConnection(ConnectionString);
        try
        {
            Connection.Open();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected abstract E createEntityFromResultSet(NpgsqlDataReader resultSet);

    protected String getTableName()
    {
        return TableName;
    }
    protected abstract String getTableInsertValuesSQL(E Entity);

    protected abstract String getSQLIdForEntityId(ID id);

    protected abstract String getSQLValuesForEntity(E entity);

    protected abstract String getSQLValuesForEntityUpdate(E entity);
    public E FindOne(ID id)
    {
        NpgsqlCommand cmd;
        NpgsqlDataReader dreader;
        String sql = "select * from " + TableName + " where" + getSQLIdForEntityId(id);
        cmd = new NpgsqlCommand(sql, Connection);
        dreader = cmd.ExecuteReader();
        E entity;
        if (dreader.Read())
        {
            entity = createEntityFromResultSet(dreader);
        }
        else
        {
            entity= null; 
        }
        return entity;
    }

    public IEnumerable<E> FindAll()
    {
        Connection.Open();
        IDictionary<ID, E> entities=new Dictionary<ID, E>();
        NpgsqlCommand cmd;
        NpgsqlDataReader dreader;
        String sql = "select * from " + TableName;
        cmd = new NpgsqlCommand(sql, Connection);
        dreader = cmd.ExecuteReader();
        while (dreader.Read())
        {
            E entity = createEntityFromResultSet(dreader);
            entities[entity.ID] = entity;
        }
        return entities.Values.ToList<E>();
    }

    public E Save(E entity)
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        String sql = "INSERT INTO " + getTableInsertValuesSQL(entity) + " VALUES " + getSQLValuesForEntity(entity);
        cmd = new NpgsqlCommand(sql, Connection);
        adapter = new NpgsqlDataAdapter(cmd);
        try
        {
            adapter.InsertCommand?.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return entity;
        }
        return null!;
    }

    public E Delete(ID id)
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        String sql = "DELETE FROM " + getTableName() + " WHERE " + getSQLIdForEntityId(id);
        cmd = new NpgsqlCommand(sql, Connection);
        adapter = new NpgsqlDataAdapter(cmd);
        try
        {
            E entity = FindOne(id);
            adapter.InsertCommand?.ExecuteNonQuery();
            return entity;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public E Update(E entity)
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        String sql = "UPDATE " + getTableName() + " SET " + getSQLValuesForEntityUpdate(entity) +
                     " WHERE " + getSQLIdForEntityId(entity.ID);
        cmd = new NpgsqlCommand(sql, Connection);
        adapter = new NpgsqlDataAdapter(cmd);
        try
        {
            E entityToUpdate = FindOne(entity.ID);
            adapter.InsertCommand?.ExecuteNonQuery();
            return entityToUpdate;
        }
        catch (Exception)
        {
            return null!;
        }
       
    }
}
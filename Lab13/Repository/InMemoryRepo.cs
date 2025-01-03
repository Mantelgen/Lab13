using Lab13.Domain;
using Lab13.Domain.Validators;

namespace Lab13.Repository;

public class InMemoryRepository<ID, E> : IRepository<ID, E> where E : Entity<ID>
{
    protected IValidator<E> vali;
 
    protected IDictionary<ID, E> entities=new Dictionary<ID, E>();
 
    public InMemoryRepository(IValidator<E> vali)
    {
        this.vali = vali;
    }
 
    public E Delete(ID id)
    {
        if (entities.ContainsKey(id))
        {
            E entity = entities[id];
            entities.Remove(id);
            return entity;
        }
        throw new KeyNotFoundException("Entity not found");
    }
 
    public IEnumerable<E> FindAll()
    {
        return entities.Values.ToList<E>();
    }
 
    public E FindOne(ID id)
    {
        if (entities.ContainsKey(id))
            return entities[id];
        throw new KeyNotFoundException("Entity not found");
    }
 
    public E Save(E entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity must not be null");
        this.vali.Validate(entity);
        if(this.entities.ContainsKey(entity.ID)){
            return entity;
        }
        this.entities[entity.ID] = entity;
        return default(E);
    }
 
    public E Update(E entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity must not be null");
        this.vali.Validate(entity);
        if (entities.ContainsKey(entity.ID))
        {
            E entityToUpdate = entities[entity.ID];
            entities[entity.ID] = entity;
            return entityToUpdate;
        }

        throw new KeyNotFoundException("Entity not found");
    }
}
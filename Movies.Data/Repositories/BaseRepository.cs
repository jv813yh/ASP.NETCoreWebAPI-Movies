using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movies.Data.DbContexts;
using Movies.Data.Interfaces;

namespace Movies.Data.Repositories
{
    /* 
     * Base repository class that implements the IBaseRepository interface
     * Contains CRUD operations for the entity and some additional methods, some of them are async, basically working with the database
     * Class uses transactions to ensure data integrity
     */
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        // DbContext for the database
        protected readonly MoviesDbContext _dbContext;

        // DbSet for the entity
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        // Async method to delete an entity from the database with transaction
        public async Task DeleteAsync(uint id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Find the entity by its id
                    TEntity? entity = _dbSet.Find(id);

                    // Remove the entity from the DbSet
                    _dbSet.Remove(entity);

                    // Save the changes to the database
                    await _dbContext.SaveChangesAsync();

                    // Commit the transaction if everything was successful
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> ExistsWithIdAsync(uint id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);

            // To avoid problems with tracking multiple entities with the same ID
            if (entity is not null)
                _dbContext.Entry(entity).State = EntityState.Detached;

            return entity is not null;
        }

        public bool ExistsWithId(uint id)
        {
            TEntity? entity =  _dbSet.Find(id);

            // To avoid problems with tracking multiple entities with the same ID
            if (entity is not null)
                _dbContext.Entry(entity).State = EntityState.Detached;

            return entity is not null;
        }

        // Async method to find an entity by its id 
        public async Task<TEntity?> FindByIdAsync(uint id)
         => await _dbSet.FindAsync(id);

        public TEntity? FindById(uint id)
        =>  _dbSet.Find(id);

        // Async method to get all entities from the database and return them as a list
        public async Task<IList<TEntity>> GetAllAsync()
            =>  await _dbSet.ToListAsync();

        public IList<TEntity> GetAll()
            => _dbSet.ToList();

        // Async method to insert new entity into the database with transaction
        // Returns the inserted entity
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Add the entity to the DbSet
                    EntityEntry<TEntity> newEntity = _dbSet.Add(entity);

                    // Save the changes to the database
                    await _dbContext.SaveChangesAsync();

                    // Commit the transaction if everything was successful
                    transaction.Commit();

                    return newEntity.Entity;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Async method to update an entity in the database with transaction
        // Returns the updated entity
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Update entity to the database
                    EntityEntry<TEntity> updatedEntity = _dbContext.Update(entity);

                    // Save the changes to the database
                    await _dbContext.SaveChangesAsync();

                    // Commit the transaction if everything was successful
                    transaction.Commit();

                    return updatedEntity.Entity;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

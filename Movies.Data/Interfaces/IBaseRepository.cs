namespace Movies.Data.Interfaces
{
    // Interface for the base repository
    // Implements CRUD operations for the entity and some additional methods,
    // Uses transactions to ensure data integrity
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity?> FindByIdAsync(uint id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(uint id);

        Task<bool> ExistsWithIdAsync(uint id);

        bool ExistsWithId(uint id);

        TEntity? FindById(uint id);
    }
}

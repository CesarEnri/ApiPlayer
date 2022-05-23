namespace BeliVGames.ApiPlayer.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T: class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> Get(T condition);
}
using Models;

namespace ChessGameWebApp.Server.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> GetById(int id);
        Task<IReadOnlyList<TEntity>> GetAll();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}

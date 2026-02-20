

using BikeRoubada.Business.Models;
using System.Linq.Expressions;

namespace BikeRoubada.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task<TEntity> Atualizar(TEntity entity);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task Remover(Guid id);
        Task<int> SaveChanges();

    }
}

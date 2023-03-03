using Domain.Dtos;
using Domain.Entities;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IBaseServices<T> where T : class, IBaseModel, new()
    {
        Task<T> GetOneAsync(int id, CancellationToken cancellationToken);
        Task<QueryResult<T>> InsertAsync(T item, CancellationToken cancellationToken);
        Task<QueryResult<T>> UpdateAsync(List<Expression<Func<T, object>>> props, T model, long id, CancellationToken cancellationToken);
        Task<QueryResult<T>> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<T>> GetAllWithFilterAsync(int pageSize, int pageNumber, bool ascSorted, Expression<Func<T, dynamic>> orderField = null, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllWithFilterAsync(int pageSize, int pageNumber, bool ascSorted, string orderField, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
        Task<int> GetCountWithFilterAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
        Task<QueryResult<T>> UpdateRange(List<T> lst, CancellationToken cancellationToken);
    }
}

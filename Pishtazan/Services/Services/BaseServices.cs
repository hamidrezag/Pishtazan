using Dapper;
using Domain.Entities;
using Domain.Services;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public abstract class BaseServices<T> : IBaseServices<T>
        where T : class, IBaseModel, new()
    {
        public AppDbContext DbContext { get; set; }
        public BaseServices(AppDbContext datamodel)
        {
            DbContext = datamodel ?? throw new ArgumentNullException();
        }
        public async virtual Task<T> GetOneAsync(int id, CancellationToken cancellationToken)
        {
            var _table = DbContext.Set<T>();
            return await _table.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        }
        public async virtual Task<QueryResult<T>> InsertAsync(T item, CancellationToken cancellationToken)
        {
            var _table = DbContext.Set<T>();
            await _table.AddAsync(item,cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return new QueryResult<T>
            {
                Success = true,
                Model = item,
                Message = "اطلاعات شما با موفقیت ثبت شد"
            };

            
        }
        public async virtual Task<QueryResult<T>> UpdateRange(List<T> lst, CancellationToken cancellationToken)
        {
            lst.ForEach(x => x.ModifiedDateTime = DateTime.Now);
            DbContext.UpdateRange(lst);
            await DbContext.SaveChangesAsync(cancellationToken);
            return new QueryResult<T>
            {
                Success = true,
                Model = null,
                Message = "اطلاعات شما با موفقیت ثبت شد"
            };
        }
        public async virtual Task<QueryResult<T>> UpdateAsync(List<Expression<Func<T, object>>> props, T model, long id, CancellationToken cancellationToken)
        {
            var _table = DbContext.Set<T>();

            var item = await _table.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
            foreach (var el in props)
            {
                PropertyInfo prop = el.ToPropertyInfo();
                if (item.GetType().GetProperties().Any(x => x.Name == prop.Name))
                {
                    var itemprop = item.GetType().GetProperty(prop.Name);
                    var valueOfModel = model.GetType().GetProperty(prop.Name).GetValue(model);
                    itemprop.SetValue(item, valueOfModel);
                }
            }
            item.ModifiedDateTime = DateTime.Now;
            int result = await DbContext.SaveChangesAsync(cancellationToken);
            return new QueryResult<T>
            {
                Success = true,
                Model = item,
                Message = "اطلاعات شما با موفقیت ثبت شد"
            };
        }
        public async virtual Task<QueryResult<T>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var _table = DbContext.Set<T>();
            _table.Remove(await _table.FirstOrDefaultAsync(x => x.Id == id,cancellationToken));
           await DbContext.SaveChangesAsync(cancellationToken);
            return new QueryResult<T>
            {
                Success = true,
                Message = "اطلاعات شما با موفقیت حذف شد"
            };
        }
        public async virtual Task<List<T>> GetAllWithFilterAsync(int pageSize, int pageNumber, bool ascSorted, Expression<Func<T, dynamic>> orderField = null, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            var q = GetAllWithFilterQuery(pageSize, pageNumber, ascSorted, orderField, filter);
            List<T> lst = await q.ToListAsync(cancellationToken);
            return lst;
        }
        public async virtual Task<List<T>> GetAllWithFilterAsync(int pageSize, int pageNumber, bool ascSorted, string orderField , Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await GetAllWithFilterAsync(pageSize,pageNumber,ascSorted, x => EF.Property<T>(x, orderField),filter,cancellationToken);
        }
        public virtual IQueryable<T> GetAllWithFilterQuery(int pageSize, int pageNumber, bool ascSorted, Expression<Func<T, dynamic>> orderField = null, Expression<Func<T, bool>> filter = null)
        {
            return filter is null ?
                DbContext.Set<T>().Pagination(pageSize, pageNumber, orderField, ascSorted)
                : DbContext.Set<T>().Where(filter).Pagination(pageSize, pageNumber, orderField, ascSorted);
        }
        public async virtual Task<int> GetCountWithFilterAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
        {
            if (filter != null)
            {
                return DbContext.Set<T>().Where(filter).Count();
            }
            else
            {
                return DbContext.Set<T>().Count();
            }

        }

    }
}

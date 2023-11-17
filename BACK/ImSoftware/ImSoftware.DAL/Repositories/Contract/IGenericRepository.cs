using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ImSoftware.DAL.Repositories.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filter = null);
        Task<TModel> Get(Expression<Func<TModel, bool>> filter);
        Task<TModel> Add(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);
    }
}

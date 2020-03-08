using System.Collections.Generic;
using Adlib.Database.Models;

namespace Adlib.Api.Repositories
{
    public interface IRepository<TModel>
    {
        (int Id, bool Success) Add(TModel model);
        bool Delete(int id);
        IEnumerable<Ad> Get();
    }
}
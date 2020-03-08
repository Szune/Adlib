using System.Collections.Generic;
using Adlib.Api.Models;

namespace Adlib.Api.Services
{
    public interface IAdService
    {
        (int Id, bool Success) Add(AddAdModel ad);
        bool Delete(int id);
        IEnumerable<GetAdModel> Get(OrderAdBy order);
    }
}
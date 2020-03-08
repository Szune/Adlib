using System.Collections.Generic;
using System.Linq;
using Adlib.Database.Contexts;
using Adlib.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Adlib.Api.Repositories
{
    public class AdRepository : IRepository<Ad>
    {
        private readonly AdContext _context;

        public AdRepository(AdContext context)
        {
            _context = context;
        }
        public (int Id, bool Success) Add(Ad model)
        {
            _context.Ads.Add(model);
            try
            {
                _context.SaveChanges();
                return (model.Id, true);
            }
            catch (DbUpdateException)
            {
                return (-1, false);
            }
        }

        public bool Delete(int id)
        {
            var ad = _context.Ads.SingleOrDefault(a => a.Id == id);
            if (ad == null)
                return false;
            _context.Ads.Remove(ad);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Ad> Get()
        {
            return _context.Ads;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Adlib.Api.Models;
using Adlib.Api.Repositories;
using Adlib.Database.Models;

namespace Adlib.Api.Services
{
    public class AdService : IAdService
    {
        private readonly IRepository<Ad> _adRepository;

        public AdService(IRepository<Ad> adRepository)
        {
            _adRepository = adRepository;
        }
        public (int Id, bool Success) Add(AddAdModel ad)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));
            // TODO: maybe use an auto mapper
            return _adRepository.Add(new Ad
            {
                Subject = ad.Subject,
                Body = ad.Body,
                Price = ad.PriceSek,
                EmailAddress = ad.EmailAddress
            });
        }

        public bool Delete(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Expected integer above zero.", nameof(id));
            return _adRepository.Delete(id);
        }

        public IEnumerable<GetAdModel> Get(OrderAdBy order)
        {
            // TODO: improvement - return x amount of ads and take an offset parameter instead of returning _all_ ads
            var ads = _adRepository.Get();
            IEnumerable<Ad> sortedAds;
            switch (order)
            {
                case OrderAdBy.Time:
                case OrderAdBy.TimeDesc:
                    sortedAds = ads.OrderByDescending(a => a.Created).ToList();
                    break;
                case OrderAdBy.TimeAsc:
                    sortedAds = ads.OrderBy(a => a.Created).ToList();
                    break;
                case OrderAdBy.Price:
                case OrderAdBy.PriceDesc:
                    throw new NotImplementedException();
                case OrderAdBy.PriceAsc:
                    throw new NotImplementedException();
                case OrderAdBy.TimeAndPriceDesc:
                    throw new NotImplementedException();
                case OrderAdBy.TimeAndPriceAsc:
                    throw new NotImplementedException();
                case OrderAdBy.TimeDescPriceAsc:
                    throw new NotImplementedException();
                case OrderAdBy.TimeAscPriceDesc:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }

            // TODO: maybe use an auto mapper
            return sortedAds.Select(a => new GetAdModel
            {
                Id = a.Id,
                Created = a.Created,
                Subject = a.Subject,
                Body = a.Body,
                PriceSek = a.Price,
                EmailAddress = a.EmailAddress
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastucture;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastucture.EfCore;

namespace DiscountManagement.Infrastucture.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long,CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndTime = x.EndTime.ToFarsi(),
                StartTime = x.StartTime.ToFarsi(),
                ProductId = x.ProductId,
                Reason = x.Reason

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndTime = x.EndTime.ToFarsi(),
                EndTimeGr = x.EndTime,
                StartTime = x.StartTime.ToFarsi(),
                StartTimeGr = x.StartTime,
                ProductId = x.ProductId,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi()

            });

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartTime))
            {
                query = query.Where(x => x.StartTimeGr > searchModel.StartTime.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndTime))
            {
                query = query.Where(x => x.EndTimeGr < searchModel.EndTime.ToGeorgianDateTime());
            }

            var discount = query.OrderByDescending(x => x.Id).ToList();
            discount.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return discount;
        }
    }
}

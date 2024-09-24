using _0_Framework.Application;
using _0_Framework.Infrastucture;
using AccountManagement.Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Infrastucture.EfCore;

namespace ShopManagement.Infrastucture.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long,Order>,IOrderRepository
    {
        private readonly AccountContext _accountContext;
        private readonly ShopContext _shopContext;

        public OrderRepository(ShopContext shopContext, AccountContext accountContext) : base(shopContext)
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var order = _shopContext.Orders
                .Select(x => new { x.PayAmount, x.Id })
                .FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                return order.PayAmount;
            }
            else
            {
                return 0;
            }
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _shopContext.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var account = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            var query = _shopContext.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                PaymentMethodId = x.PaymentMethod,
                TotalAmount = x.TotalAmount,
                DiscountAmount = x.DiscountAmount,
                PayAmount = x.PayAmount,
                IsPaid = x.IsPaid,
                IsCanceled = x.IsCanceled,
                IssueTrackingNo = x.IssueTrackingNo,
                RefId = x.RefId,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.AccountId > 0)
            {
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            }

            if (searchModel.IsCanceled)
            {
                query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);
            }

            var orders = query.OrderByDescending(x => x.Id).ToList();

            foreach (var order in orders)
            {
                order.AccountFullName = account.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;
            }

            return orders;
        }
    }
}

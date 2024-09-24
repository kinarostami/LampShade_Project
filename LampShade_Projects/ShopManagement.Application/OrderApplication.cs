using _0_Framework.Application;
using _0_Framework.Application.Sms;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        private readonly ISmsService _smsService;
        private readonly IShopAccountAcl _shopAccountAcl;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl shopInventoryAcl, ISmsService smsService, IShopAccountAcl shopAccountAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _shopInventoryAcl = shopInventoryAcl;
            _smsService = smsService;
            _shopAccountAcl = shopAccountAcl;
        }

        public long PlaceOrder(Cart cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();
            var order = new Order(currentAccountId,cart.PaymentMethod,cart.TotalAmount,cart.DiscountAmount,cart.PayAmount);

            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public string PaymentSucceeded(long orderId,long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var issueTrackingNo = CodeGenerator.Generate("S");
            order.SetIssueTrackingNo(issueTrackingNo);
            // Reduce OrderItem From Inventory
            if (!_shopInventoryAcl.ReduceFromInventory(order.Items)) return "";

            _orderRepository.SaveChanges();

            var account = _shopAccountAcl.GetAccountBy(order.AccountId);

            _smsService.Send(account.mobile,$"کاربر {account.name} گرامی سفارش شما {issueTrackingNo} با موفقیت پرداخت شد و ارسال خواهد شد");
            return issueTrackingNo;
        }

        public void Cancel(long id)
        {
            var order = _orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            return _orderRepository.GetItems(orderId);
        }
    }
}

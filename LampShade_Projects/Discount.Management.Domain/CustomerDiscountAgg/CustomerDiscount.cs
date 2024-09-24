using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount : EntityBase
    {
        public long ProductId { get;  private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Reason { get; private set; }

        public CustomerDiscount(long productId,int discountRate,DateTime startTime,DateTime endTime,string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartTime = startTime;
            EndTime = endTime;
            Reason = reason;
        }

        public void Edit(long productId, int discountRate, DateTime startTime, DateTime endTime, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartTime = startTime;
            EndTime = endTime;
            Reason = reason;
        }
    }
}

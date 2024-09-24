namespace ShopManagement.Application.Contracts.Order;

public class Cart
{
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double PayAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CartItem> CartItems { get; set; }

    public Cart()
    {
        CartItems = new List<CartItem>();
    }

    public void Add(CartItem cartItem)
    {
        CartItems.Add(cartItem);
        TotalAmount += cartItem.TotalItemPrice;
        DiscountAmount += cartItem.DiscountAmount;
        PayAmount += cartItem.ItemPayAmount;
    }

    public void SetPaymentMethod(int methodId)
    {
        PaymentMethod = methodId;
    }
}
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Comment;
using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Infrastucture.EFCore;
using DiscountManagement.Infrastucture.EFCore;
using InventoryManagement.Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastucture.EfCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartTime < DateTime.Now && x.EndTime > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndTime }).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(z => z.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    CategorySlug = x.Category.Slug,
                    Slug = x.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Kewords = x.Keywords,
                    MetaDescriprion = x.MetaDescription,
                    ShortDescrioption = x.ShortDescription,
                    ProductPictures = MapPictures(x.ProductPictures),
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);


            //محاسبه قیمت محصول
            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.DoublePrice = price;

                //محاسبه درصد محصول
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndTime.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;

                    //محاصبه قیمت تخفیف و قیمت بعد از تخفیف
                    var discountAmount = Math.Round(price * discountRate / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            product.Comments = _commentContext.Comments.Where(x => x.Type == CommentType.Product) // 1 == product
                .Where(x => x.OwnerRecordId == product.Id)
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        private static List<ProductPictureQueryModel> MapPictures(List<ProductPicture> ProductPictures)
        {
            return ProductPictures
                .Select(x => new ProductPictureQueryModel
                {
                    ProductId = x.ProductId,
                    Pictre = x.Picture,
                    PictreAlt = x.PictureAlt,
                    PictreTitle = x.PictureTitle,
                    IsRemoved = x.IsRemoved
                }).Where(x => !x.IsRemoved).ToList();

        }

        public List<ProductQueryModel> GetProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartTime < DateTime.Now && x.EndTime > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var products = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Category = x.Category.Name,
                    Slug = x.Slug

                }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
                //محاسبه قیمت محصول
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    //محاسبه درصد محصول
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;

                        //محاصبه قیمت تخفیف و قیمت بعد از تخفیف
                        var discountAmount = Math.Round(price * discountRate / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }
            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory
         .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartTime < DateTime.Now && x.EndTime > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndTime }).ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    CategorySlug = x.Category.Slug,
                    Slug = x.Slug,
                    ShortDescrioption = x.ShortDescription
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescrioption.Contains(value));
            }

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                //محاسبه قیمت محصول
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    //محاسبه درصد محصول
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount == null) continue;
                    
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndTime.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;

                    //محاصبه قیمت تخفیف و قیمت بعد از تخفیف
                    var discountAmount = Math.Round(price * discountRate / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }

            }

            return products;
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = _inventoryContext.Inventory.ToList();
            
            foreach (var cartItem in cartItems)
            {
                if (inventory.Any(x => x.ProductId == cartItem.Id && x.InStock))
                {
                    var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
                    if (itemInventory != null)
                    {
                        cartItem.IsInStock = itemInventory.CalculateCurrentCount() >= cartItem.Count;
                    }
                }
            }

            return cartItems;
        }
    }
}

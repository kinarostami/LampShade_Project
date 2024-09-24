namespace _01_LampshadeQuery.Contracts.Product;

public class ProductPictureQueryModel
{
    public long ProductId { get; set; }
    public string Pictre { get; set; }
    public string PictreAlt { get; set; }
    public string PictreTitle { get; set; }
    public bool IsRemoved { get; set; }
}
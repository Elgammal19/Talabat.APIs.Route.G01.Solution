namespace Talabat.Core.Entities.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }  // Product Id
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
    }
}
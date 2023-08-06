namespace EShop.DTO.Product
{
    public class UpdateProductInput
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public decimal Price { get; set; }
        public string AvatarUrl { get; set; }
    }
}

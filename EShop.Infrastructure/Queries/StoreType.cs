using EShop.Infrastructure.DataLoaders;
using EShop.Models; 

namespace EShop.Infrastructure.Queries
{
    public class StoreType : ISearchResultType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime createdAt { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string Address { get; set; }

        public string? UserId { get; set; }

        //[IsProjected(true)] //sloja li go,v banana ne slagam productId
        //public string ProductId { get; set; }

        //[GraphQLNonNullType]
        //public async Task<ProductType> Product([Service] ProductDataLoader productDataLoader)
        //{
        //    Product product = await productDataLoader.LoadAsync(ProductId, CancellationToken.None);

        //    return new ProductType()
        //    {
        //        Id = product.Id,
        //        CreatedAt = product.CreatedAt,
        //        UpdatedAt = product.UpdatedAt,
        //        Name = product.Name,
        //        Description = product.Description,
        //        Warranty = product.Warranty,
        //        Price = product.Price,
        //        AvatarUrl = product.AvatarUrl
        //    };
        //}
        public string ProductId { get; set; }

        public ICollection<Product> Products { get; set; }

        public string CartId { get; set; }

        public ICollection<Cart> Carts { get; set; }
    }
}

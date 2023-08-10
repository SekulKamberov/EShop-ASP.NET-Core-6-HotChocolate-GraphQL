using Microsoft.Extensions.DependencyInjection;

using EShop.Core.Repositories;
using EShop.Core.Services;
using EShop.Data;
using EShop.DTO.Common;
using EShop.DTO.Product;
using EShop.Models;
using EShop.Infrastructure.Specifications;
using EShop.Common.CustomException;

namespace EShop.Infrastructure.Mutations
{
    public class ProductMutations : IProductMutations
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductCategory> categoryRepo;
        private readonly IGenericRepository<Store> storeRepository;

        public ProductMutations(IServiceProvider serviceProvider)
        {
            productRepository = serviceProvider.GetRequiredService<IGenericRepository<Product>>();
            categoryRepo = serviceProvider.GetRequiredService<IGenericRepository<ProductCategory>>();
            storeRepository = serviceProvider.GetRequiredService<IGenericRepository<Store>>();
        }

        public async Task<ProductPayload> AddProduct(AddProductInput input, [Service] EShopDbContext context)
        {
            var isValidCategory = await categoryRepo
                .GetEntityBySpec(new ProductCategorySpecification(input.CategoryId)) is null;
            if (isValidCategory)
                throw new ModelExceptions() { DefaultError = $"The category is invalid" };

            var isValidStore = await storeRepository.GetEntityBySpec(new StoreSpecification(input.StoreId)) is null;
            if (isValidStore)
                throw new ModelExceptions() { DefaultError = $"Store does not exist" };

            Product product = new Product
            {
                CategoryId = input.CategoryId,
                StoreId = input.StoreId,
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                AvatarUrl = input.AvatarUrl,
                Warranty = input.Warranty
            };

            var result = await productRepository.AddEntity(product);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The product could not be added" };

            return new ProductPayload
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = input.Description,
                AvatarUrl = input.AvatarUrl,
                Warranty = input.Warranty
            };
        } 

        public async Task<ProductPayload> UpdateProduct(
            UpdateProductInput input, 
            [Service] EShopDbContext context)
        {
            Product product = await productRepository.GetEntityBySpec(new ProductCheckSpecification(input.Id));
            if (product is null)
                throw new ModelExceptions() { DefaultError = $"The product id {input.Id} is not available" };

            product.Name = input.Name is null ? product.Name : input.Name;
            product.Price = input.Price is 0 ? product.Price : input.Price;
            product.CategoryId = input.CategoryId is null ? product.CategoryId : input.CategoryId;
            product.AvatarUrl = input.AvatarUrl is null ? product.AvatarUrl : input.AvatarUrl;

            var result = await productRepository.UpdateEntity(product);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The product could not be updated" };

            return new ProductPayload
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                AvatarUrl = product.AvatarUrl
            };
        }

        public async Task<ProductPayload> DeleteProduct(
            DeleteInput input, 
            [Service] EShopDbContext context)
        {
            Product product = await productRepository.GetEntityBySpec(new ProductCheckSpecification(input.Id));

            if (product is null)
                throw new ModelExceptions() { DefaultError = $"The product id {input.Id} is not available" };

            var result = await productRepository.DeleteEntity(product);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The product could not be deleted" };

            return new ProductPayload
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                AvatarUrl = product.AvatarUrl
            };
        }
    }
}

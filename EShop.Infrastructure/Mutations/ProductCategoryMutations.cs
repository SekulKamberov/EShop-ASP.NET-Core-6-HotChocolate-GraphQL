using Microsoft.Extensions.DependencyInjection;

using EShop.Core.Repositories;
using EShop.Core.Services;
using EShop.Data;
using EShop.DTO.Category;
using EShop.Models;
using EShop.Infrastructure.Specifications;
using EShop.Common.CustomException;
using EShop.DTO.Common;

namespace EShop.Infrastructure.Mutations
{
    public class ProductCategoryMutations : IProductCategoryMutations
    {
        private readonly IGenericRepository<ProductCategory> productCategoryRepo;

        public ProductCategoryMutations(IServiceProvider serviceProvider) 
            => productCategoryRepo = serviceProvider
                .GetRequiredService<IGenericRepository<ProductCategory>>(); 

        public async Task<CategoryPayload> AddCategory(AddCategoryInput input, EShopDbContext context)
        {
            if (await productCategoryRepo.GetEntityBySpec(new CategoryCheckSpecification(input.Name)) != null) 
                throw new ModelExceptions() { DefaultError = $"The name {input.Name} is not available" };

            ProductCategory category = new ProductCategory { Name = input.Name };

            var result = await productCategoryRepo.AddEntity(category); 
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The store could not be created" };

            return new CategoryPayload { Id = category.Id, Name = category.Name };
        }

        public async Task<CategoryPayload> DeleteCategory(DeleteInput input, EShopDbContext context)
        {
            ProductCategory category = await productCategoryRepo
                .GetEntityBySpec(new ProductCategorySpecification(input.Id));

            if (category is null) 
                throw new ModelExceptions() { DefaultError = $"The Id {category.Id} is not available" }; 

            var result = await productCategoryRepo.DeleteEntity(category);
            if(!result) 
                throw new ModelExceptions() { DefaultError = "Could not be deleted" };

            return new CategoryPayload { Id = category.Id, Name = category.Name }; 
        }

        public async Task<CategoryPayload> UpdateCategory(UpdateCategoryInput input, EShopDbContext context)
        {
            ProductCategory category = await productCategoryRepo
                .GetEntityBySpec(new ProductCategorySpecification(input.Id));

            if (category is null)
                throw new ModelExceptions() { DefaultError = $"The Id {category.Id} is not available" };

            category.Name = input.Name is null ? category.Name : input.Name;
            
            var result = await productCategoryRepo.UpdateEntity(category);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"Could not be updated" };

            return new CategoryPayload { Id = category.Id, Name = category.Name };
        }
    }
}

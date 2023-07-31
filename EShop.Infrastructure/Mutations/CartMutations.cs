using Microsoft.Extensions.DependencyInjection;

using EShop.Common.CustomException;
using EShop.Core.Repositories;
using EShop.Core.Services;
using EShop.Data;
using EShop.DTO.Cart;
using EShop.DTO.Common;
using EShop.Infrastructure.Specifications;
using EShop.Models; 

namespace EShop.Infrastructure.Mutations
{
    public class CartMutations : ICartMutations
    {
        private readonly IGenericRepository<Cart> cartRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<CartProduct> cartProductRepository;
        private readonly IGenericRepository<Store> storeRepository;
        private readonly IGenericRepository<Product> productRepository;

        public CartMutations(IServiceProvider serviceProvider)
        {
            cartRepository = serviceProvider.GetRequiredService<IGenericRepository<Cart>>();
            userRepository = serviceProvider.GetRequiredService<IGenericRepository<User>>();
            cartProductRepository = serviceProvider.GetRequiredService<IGenericRepository<CartProduct>>();
            storeRepository = serviceProvider.GetRequiredService<IGenericRepository<Store>>();
            productRepository = serviceProvider.GetRequiredService<IGenericRepository<Product>>();

        }

        public async Task<CartPayload> AddCart(AddCartInput input, EShopDbContext context, string id)
        {
            User user = await userRepository.GetEntityBySpec(new UserSpecification(id));
            if (user is null)
                throw new AccessViolationException("Forbidden");

            if (await storeRepository.GetEntityBySpec(new StoreSpecification(input.StoreId)) is null)
                throw new ModelExceptions() { DefaultError = "The store does not exist" };

            if (input.CartProducts is null || input.CartProducts.Count < 0)
                throw new ModelExceptions() { DefaultError = "An empty cart cannot be created" };

            Cart cart = new Cart { UserId = user.Id, StoreId = input.StoreId };

            var result = await cartRepository.AddEntity(cart);
            if (!result)
                throw new ModelExceptions() { DefaultError = "The cart could not be created" };

            List<CartProduct> cartProducts = new List<CartProduct>();
            foreach (var product in input.CartProducts)
            {
                var actualProd = await productRepository
                    .GetEntityBySpec(new ProductCheckSpecification(product.ProductId));

                if (actualProd is null) { }

                else if (actualProd.StoreId == input.StoreId)
                {
                    CartProduct cartProduct = new CartProduct
                    {
                        CartId = cart.Id,
                        ProductId = product.ProductId,
                    };

                    cartProducts.Add(cartProduct);
                } 
            }

            if (cartProducts.Count <= 0)
            {
                await cartRepository.DeleteAll(c => c.Id == cart.Id);
                throw new ModelExceptions()
                {
                    DefaultError = "The cart could not be created due to a problem with the products"
                };
            }

            await cartProductRepository.AddRangeAsync(cartProducts);

            return new CartPayload { 
                Id = cart.Id, 
                StoreId = cart.StoreId, 
                UserId = cart.UserId,
                CartProducts = cart.CartProducts
                    .Select(c => new CartAndProduct { ProductId = c.ProductId}).ToList(),
            };

        }

        public async Task<CartPayload> DeleteCart(DeleteInput input, EShopDbContext context, string Id)
        {
            Cart cart = await cartRepository.GetEntityBySpec(new CartCheckSpecification(input.Id));
            if (cart is null)
                throw new ModelExceptions() { DefaultError = $"The cart id {input.Id} is not available" };

            if (cart.UserId != Id)
                throw new AccessViolationException("Forbidden");

            var result = await cartRepository.DeleteEntity(cart);
            if (!result)
                throw new ModelExceptions() { DefaultError = "The cart could not be deleted" };

            return new CartPayload { Id = cart.Id, StoreId = cart.StoreId, UserId = cart.UserId };

        }

        public async Task<CartPayload> UpdateCart(UpdateCartInput input, EShopDbContext context, string Id)
        {
            Cart cart = await cartRepository.GetEntityBySpec(new CartCheckSpecification(input.Id));
            if (cart is null)
                throw new ModelExceptions() { DefaultError = $"The cart id {input.Id} is not available" };

            if (cart.UserId != Id)
                throw new AccessViolationException("Forbidden");

            var result = await cartProductRepository.DeleteAll(c => c.CartId == input.Id);
            if (!result)
                throw new ModelExceptions() { DefaultError = "The cart could not be updated" };

            foreach (var product in input.CartProducts)
            {
                CartProduct cartProduct = new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = product.ProductId,
                };
                await cartProductRepository.AddEntity(cartProduct);
            }

            return new CartPayload { Id = cart.Id, StoreId = cart.StoreId, UserId = cart.UserId };
        }
    }
}

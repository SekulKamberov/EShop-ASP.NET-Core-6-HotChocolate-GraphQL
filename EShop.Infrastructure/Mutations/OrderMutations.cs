using Microsoft.Extensions.DependencyInjection;

using EShop.Data;
using EShop.Core.Repositories;
using EShop.Core.Services;
using EShop.DTO.Common;
using EShop.DTO.Order; 
using EShop.Models;
using EShop.Infrastructure.Specifications;
using EShop.Common.CustomException;

namespace EShop.Infrastructure.Mutations
{
    public class OrderMutations : IOrderMutations
    {
        private readonly IGenericRepository<Order> orderRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Cart> cartRepository;

        public OrderMutations(IServiceProvider serviceProvider)
        {
            orderRepository = serviceProvider.GetRequiredService<IGenericRepository<Order>>();
            userRepository = serviceProvider.GetRequiredService<IGenericRepository<User>>();
            cartRepository = serviceProvider.GetRequiredService<IGenericRepository<Cart>>();
        } 

        public async Task<OrderPayload> MakeOrder(MakeOrderInput input, EShopDbContext context, string id)
        {
            User user = await userRepository.GetEntityBySpec(new UserSpecification(id));
            if (user is null)
                throw new AccessViolationException("Forbidden");

            if (await cartRepository.GetEntityBySpec(new CartCheckSpecification(input.CartId)) is null)
                throw new ModelExceptions() { DefaultError = $"Cart does not exist" };

            Order order = new Order
            {
                Address = input.Address,
                CartId = input.CartId,
                UserId = user.Id,
                PhoneNumber = input.PhoneNumber 
            };

            var result = await orderRepository.AddEntity(order);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The order could not be created" };

            return new OrderPayload
            {
                Id = order.Id,
                Address = order.Address,
                CartId = order.CartId,
                UserId = order.UserId,
                PhoneNumber = order.PhoneNumber
            };

        }

        public async Task<OrderPayload> UpdateOrder(UpdateOrderInput input, EShopDbContext context, string id)
        {
            Order order = await orderRepository.GetEntityBySpec(new OrderCheckSpecification(input.Id));
            if (order is null)
                throw new ModelExceptions() { DefaultError = $"The order id {input.Id} is not available" };

            if (order.UserId != id)
                throw new AccessViolationException("Forbidden");

            if (order.IsDelivered)
                throw new ModelExceptions() { DefaultError = "The order has been delivered" };

            order.PhoneNumber = input.PhoneNumber is null ? order.PhoneNumber : input.PhoneNumber;
            order.Address = input.Address is null ? order.Address : input.Address;
            order.CartId = input.CartId is null ? order.CartId : input.CartId;

            var result = await orderRepository.UpdateEntity(order);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The order could not be updated" };

            return new OrderPayload
            {
                Id = order.Id,
                UserId = order.UserId,
                CartId = order.CartId,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber
            };

        }

        public async Task<OrderPayload> DeleteOrder(DeleteInput input, EShopDbContext context, string id)
        {
            Order order = await orderRepository.GetEntityBySpec(new OrderCheckSpecification(input.Id));
            if (order == null)
                throw new ModelExceptions() { DefaultError = $"The order id {input.Id} is not available" };

            if (order.UserId != id)
                throw new AccessViolationException($"Forbidden");

            var result = await orderRepository.DeleteEntity(order);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The order could not be deleted" };

            return new OrderPayload
            {
                Id = order.Id,
                Address = order.Address,
                CartId = order.CartId,
                UserId = order.UserId,
                PhoneNumber = order.PhoneNumber
            };

        }
    }
}

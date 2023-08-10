﻿using System.Security.Claims;

using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.DependencyInjection;

using EShop.Data;
using EShop.Models;
using EShop.DTO.Store;
using EShop.DTO.Common;
using EShop.Core.Services;
using EShop.Core.Repositories; 
using EShop.Common.CustomException;
using EShop.Infrastructure.Specifications; 

namespace EShop.Infrastructure.Mutations
{
    public class StoreMutations : IStoreMutations
    {
        private readonly IGenericRepository<Store> storeRepository;

        public StoreMutations(IServiceProvider serviceProvider)
                => storeRepository = serviceProvider.GetRequiredService<IGenericRepository<Store>>();

        public async Task<StorePayload> AddStore(
            AddStoreInput input, 
            EShopDbContext context,
            IHttpContextAccessor contextAccessor)
        {
            if (await storeRepository.GetEntityBySpec(new StoreCheckSpecification(input.Name)) != null)
                throw new ModelExceptions() { DefaultError = $"The name {input.Name} is not available" };

            var userId = GetUserId(contextAccessor);
            Store store = new Store
            {
                Name = input.Name,
                Description = input.Description,
                PhoneNumber = input.PhoneNumber,
                Address = input.Address,
                AvatarUrl = input.AvatarUrl,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            var result = await storeRepository.AddEntity(store);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The store could not be created" };

            return new StorePayload
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                Description = store.Description,
                Address = input.Address,
                AvatarUrl = input.AvatarUrl
            };
        }

        public async Task<StorePayload> DeleteStore(DeleteInput input, EShopDbContext context)
        {
            Store store = await storeRepository.GetEntityBySpec(new StoreSpecification(input.Id));
            if (store is null)
                throw new ModelExceptions() { DefaultError = $"The store id {input.Id} is not available" };

            var result = await storeRepository.DeleteEntity(store);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The store could not be deleted" };

            return new StorePayload
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                Description = store.Description,
                Address = store.Address,
                AvatarUrl = store.AvatarUrl
            };
        }

        public async Task<StorePayload> UpdateStore(UpdateStoreInput input, EShopDbContext context)
        {
            Store store = await storeRepository.GetEntityBySpec(new StoreSpecification(input.Id));
            if (store is null)
                throw new ModelExceptions() { DefaultError = $"The store id {input.Id} is not available" };

            store.PhoneNumber = input.PhoneNumber is null ? store.PhoneNumber : input.PhoneNumber;
            store.Description = input.Description is null ? store.Description : input.Description;
            store.Address = input.Address is null ? store.Address : input.Address;
            store.AvatarUrl = input.AvatarUrl is null ? store.AvatarUrl : input.AvatarUrl;


            var result = await storeRepository.UpdateEntity(store);
            if (!result)
                throw new ModelExceptions() { DefaultError = $"The store could not be updated" };

            return new StorePayload
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                Description = store.Description,
                Address = input.Address,
                AvatarUrl = input.AvatarUrl
            };
        }

        private static string GetUserId(IHttpContextAccessor contextAccessor)
            => contextAccessor.HttpContext.User.Claims
                .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)
                .Value.ToString();
    }
}

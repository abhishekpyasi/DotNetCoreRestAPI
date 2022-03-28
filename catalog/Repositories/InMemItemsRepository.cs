
/* 
here we are making a in memory reporitory of items with is a list of items.
item is defined in entities Item class which has four attributes.

*/
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using catalog.Entities;
using Microsoft.AspNetCore.Http.Features;

namespace catalog.Repositories
{


    public class InMemItemsRepository : IItemsRepository
    {

        private readonly List<Item> items = new List<Item>{
            new Item { Id = Guid.NewGuid(), Name = "potion", Price =9 , CreatedDate = DateTimeOffset.UtcNow },
                        new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price =20 , CreatedDate = DateTimeOffset.UtcNow },
                                    new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price =18 , CreatedDate = DateTimeOffset.UtcNow }

        };






        public async Task<IEnumerable<Item>> GetItemsAsync()
        {

            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();

            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)

        {
            items.Add(item);
            await Task.CompletedTask;

        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;




        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingitem => existingitem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;


        }
    }
}
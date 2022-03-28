

//Controller is component which will handle http request which will be sent from browser

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using catalog.Dtos;
using catalog.Entities;
using catalog.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace catalog.Controllers
{
    [ApiController]  // APIController brings additional behavior for this class which will be useful
    [Route("items")]
    // [Route("[controller]")]
    // GET /items will be route which this controller will respond
    //[Route("Items)] will also have same effect as above
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {


            this.repository = repository;

        }
        // HttpGet will be cater to 'GET' request coming /items route
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {

            var items = (await repository.GetItemsAsync()) // first complete async task then select

            .Select(item => item.AsDto());
            return items;


        }

        // method for routes defined as /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {

                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {

            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow




            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());

            // CreatedAtAction(actionName, routeValues, createdResource);


        }
        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> updateItemAsync(Guid id, UpdateItemDto itemDto)
        {

            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)

            {

                return NotFound();
            }

            Item updatedItem = existingItem with
            {

                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {

            Item item = await repository.GetItemAsync(id);

            if (item is null)
            {

                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();


        }

    }


}
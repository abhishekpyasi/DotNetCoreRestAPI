/*

Item is entity or model which will be returned when user will request resource from 
browser through http requests
 */

using System;
namespace catalog.Entities
{

    public record class Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }


    }

}
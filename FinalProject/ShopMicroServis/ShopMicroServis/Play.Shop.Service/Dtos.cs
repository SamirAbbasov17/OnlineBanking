using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Shop.Service.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, decimal Price , string Image);

    public record CreateItemDto([Required] string Name, string Description, [Range(0, 1000)] decimal Price, string Image);

    public record UpdateItemDto([Required] string Name, string Description, [Range(0, 1000)] decimal Price, string Image);
}
﻿using Newtonsoft.Json;

namespace AdminPanel.ViewModels
{
    public class ShopVM
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("image")]
        public IFormFile? Image { get; set; }

    }
}

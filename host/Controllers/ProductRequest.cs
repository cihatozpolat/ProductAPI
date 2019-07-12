using System;

namespace ProductAPI.Controllers
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Country { get; set; }
    }
}
using System;

namespace domain
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
        public virtual int Price { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual string Country { get; set; }

    }
}
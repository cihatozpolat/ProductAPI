using FluentNHibernate.Mapping;

namespace domain
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id).Not.Nullable();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Color).Not.Nullable();
            Map(x => x.Price).Not.Nullable();
            Map(x => x.ExpirationDate).Not.Nullable();
            Map(x => x.Country).Not.Nullable();
        }
    }
}
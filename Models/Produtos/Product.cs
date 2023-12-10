namespace HefestusApi.Models.Produtos
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriceSale { get; set; }
        public int PriceTotal { get; set; }

        public int FamilyId { get; set; }
        public int GroupId { get; set; }
        public int SubgroupId { get; set; }

        public ProductFamily Family { get; set; }
        public ProductGroup Group { get; set; }
        public ProductSubGroup Subgroup { get; set; }
    }
}

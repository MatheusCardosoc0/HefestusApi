using HefestusApi.Models.Administracao;
using HefestusApi.Models.Vendas;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Produtos
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public required string Name { get; set; }
        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public required int NCM { get; set; }
        public required string GTIN { get; set; } = "Sem GTIN";
        public string? GTINtrib { get; set; }
        public int FamilyId { get; set; }
        public int GroupId { get; set; }
        public int SubgroupId { get; set; }
        public string? Reference { get; set; }
        public string? Batch { get; set; }
        public required string UnitOfMensuration { get; set; }

        public ProductFamily Family { get; set; }
        public ProductGroup Group { get; set; }
        public ProductSubGroup Subgroup { get; set; }

        [JsonIgnore]
        public List<OrderProduct>? OrderProducts { get; set; }

        public List<Stock> Stocks { get; set; }
    }

    public class Stock
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public float CurrentStock { get; set; }
        public float MinStock { get; set; }
        public float MaxStock { get; set; }
        public decimal UnitCost { get; set; }
        public decimal AverageCost { get; set; }
        public decimal PromotionalPrice { get; set; }
        public decimal PriceSale { get; set; }
        public decimal BruteCost { get; set; }
        public decimal LiquidCost { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal MinPriceSale { get; set; }
        public decimal MinWholesalePrice { get; set; }
        public int SystemLocationId { get; set; }
        public SystemLocation SystemLocation { get; set; }
        public string? Location { get; set; }
        public DateTime? LastStockUpdate { get; set; }
    }
}

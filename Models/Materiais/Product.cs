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
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float MinPriceSale { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public decimal AverageCost { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float PromotionalPrice { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float PriceSale { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float BruteCost { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float LiquidCost { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float WholesalePrice { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float MinWholesalePrice { get; set; }
        public string? UrlImage { get; set; }
        public required int NCM { get; set; }
        public required string GTIN { get; set; } = "Sem GTIN";
        public string? GTINtrib { get; set; } 
        public int FamilyId { get; set; }
        public int GroupId { get; set; }
        public int SubgroupId { get; set; }
        public string? Reference {  get; set; }
        public string? Batch {  get; set; }

        public required ProductFamily Family { get; set; }
        public required ProductGroup Group { get; set; }
        public required ProductSubGroup Subgroup { get; set; }

        [JsonIgnore]
        public List<OrderProduct>? OrderProducts { get; set; }
    }
}

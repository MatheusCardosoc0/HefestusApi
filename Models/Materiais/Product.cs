using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Produtos
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string Description { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float PriceSale { get; set; }
        [MaxDecimalPlaces(4, ErrorMessage = "O preço de venda não pode ter mais de 4 casas decimais.")]
        public float PriceTotal { get; set; }

        public int FamilyId { get; set; }
        public int GroupId { get; set; }
        public int SubgroupId { get; set; }

        public ProductFamily Family { get; set; }
        public ProductGroup Group { get; set; }
        public ProductSubGroup Subgroup { get; set; }

        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }
    }
}

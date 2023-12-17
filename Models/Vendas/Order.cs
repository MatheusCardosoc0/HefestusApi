using HefestusApi.Models.Administracao;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Interfaces;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Vendas
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Person Client { get; set; }
        public int ResponsibleId { get; set; }
        public Person Responsible { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } 
        public int PaymentConditionId { get; set; }
        public PaymentCondition PaymentCondition { get; set; }
        public int PaymentOptionId { get; set; }
        public PaymentOptions PaymentOption { get; set; }
        public decimal Value { get; set; } 
    }
}


public class OrderProduct
{
    public int OrderId { get; set; }
    [JsonIgnore]
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public decimal Price { get; set; } 
}
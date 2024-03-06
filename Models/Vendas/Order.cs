using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HefestusApi.Models.Administracao;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;

namespace HefestusApi.Models.Vendas
{
    /// <summary>
    /// Representa uma ordem de venda no sistema.
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public Person Client { get; set; }

        [Required]
        public int ResponsibleId { get; set; }

        [Required]
        public Person Responsible { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        [Required]
        public int PaymentConditionId { get; set; }

        [Required]
        public PaymentCondition PaymentCondition { get; set; }

        [Required]
        public int PaymentOptionId { get; set; }

        [Required]
        public PaymentOptions PaymentOption { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor total deve ser positivo.")]
        public decimal TotalValue { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor líquido deve ser positivo.")]
        public decimal LiquidValue { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor bruto deve ser positivo.")]
        public decimal BruteValue { get; set; }

        [Required]
        public string DateOfCompletion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// Identificador da fatura associada a ordem.
        /// </summary>
        public int? InvoiceId { get; set; } 

        [Required]
        [Range(0, 1, ErrorMessage = "O desconto deve estar entre 0 e 1.")]
        public float Discount { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O tipo de ordem deve ter no máximo 100 caracteres.")]
        public string TypeOrder { get; set; }

        public List<OrderInstallment> OrderInstallments { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O custo do frete deve ser positivo.")]
        public float? CostOfFreight { get; set; }

        public string? TypeFreight { get; set; }
    }

    /// <summary>
    /// Representa um produto associado a uma ordem de venda.
    /// </summary>
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore]
        [Required]
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O preço total deve ser positivo.")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "A quantidade deve ser positiva.")]
        public float Amount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O preço unitário deve ser positivo.")]
        public decimal UnitPrice { get; set; }
    }

    public class OrderInstallment
    {
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public int InstallmentNumber { get; set; }
        [Required]
        public int PaymentOptionId { get; set; }
        [Required]
        public PaymentOptions PaymentOption { get; set; }
        public string Maturity { get; set; }
        public decimal Value { get; set; }
    }
}

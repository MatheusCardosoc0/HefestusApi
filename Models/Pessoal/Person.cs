﻿using HefestusApi.Models.Administracao;
using HefestusApi.Models.Interfaces;
using HefestusApi.Models.Vendas;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Pessoal
{
    public class Person 
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public string? Name { get; set; }
        public string? Email { get; set; } 
        public string? Phone { get; set; }
        public int Age { get; set; }
        public string? CPF { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? IBGE { get; set; }
        public string? Razao { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? CEP { get; set; }
        public string? UrlImage { get; set; } = string.Empty;
        public bool? IsBlocked { get; set; } = false;
        public string? MaritalStatus { get; set; } = string.Empty;
        public string? Habilities { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public bool? ICMSContributor { get; set; } = false;
        public string PersonType {  get; set; } = string.Empty;
        public int CityId { get; set; }
        public City City { get; set; }
        public List<PersonGroup> PersonGroup { get; set; }
        public string SystemLocationId { get; set; }
        public int? SubLocationId { get; set; }
        public SubLocation? SubLocation { get; set; }
        public string? UserId { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
    }
}
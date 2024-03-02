﻿// <auto-generated />
using System;
using HefestusApi.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HefestusApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HefestusApi.Models.Administracao.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IBGENumber")
                        .HasColumnType("integer");

                    b.Property<string>("LastModifiedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Cities_Name");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("BirthDate")
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Habilities")
                        .HasColumnType("text");

                    b.Property<string>("IBGE")
                        .HasColumnType("text");

                    b.Property<bool?>("ICMSContributor")
                        .HasColumnType("boolean");

                    b.Property<string>("InscricaoEstadual")
                        .HasColumnType("text");

                    b.Property<bool?>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Razao")
                        .HasColumnType("text");

                    b.Property<string>("UrlImage")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Persons_Name");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Person");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.PersonGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_PersonGroups_Name");

                    b.ToTable("PersonGroup");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HefestusApi.Models.Financeiro.PaymentCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Installments")
                        .HasColumnType("integer");

                    b.Property<int>("Interval")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_PaymentConditions_Name");

                    b.ToTable("PaymentCondition");
                });

            modelBuilder.Entity("HefestusApi.Models.Financeiro.PaymentOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isUseCreditLimit")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_PaymentOptions_Name");

                    b.ToTable("PaymentOptions");
                });

            modelBuilder.Entity("HefestusApi.Models.Produtos.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AverageCost")
                        .HasColumnType("numeric");

                    b.Property<string>("Batch")
                        .HasColumnType("text");

                    b.Property<float>("BruteCost")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("FamilyId")
                        .HasColumnType("integer");

                    b.Property<string>("GTIN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GTINtrib")
                        .HasColumnType("text");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<float>("LiquidCost")
                        .HasColumnType("real");

                    b.Property<float>("MinPriceSale")
                        .HasColumnType("real");

                    b.Property<float>("MinWholesalePrice")
                        .HasColumnType("real");

                    b.Property<int>("NCM")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<float>("PriceSale")
                        .HasColumnType("real");

                    b.Property<float>("PromotionalPrice")
                        .HasColumnType("real");

                    b.Property<string>("Reference")
                        .HasColumnType("text");

                    b.Property<int>("SubgroupId")
                        .HasColumnType("integer");

                    b.Property<string>("UnitOfMensuration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UrlImage")
                        .HasColumnType("text");

                    b.Property<float>("WholesalePrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.HasIndex("GroupId");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Products_Name");

                    b.HasIndex("SubgroupId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("HefestusApi.Models.Produtos.ProductFamily", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_ProductFamilies_Name");

                    b.ToTable("ProductFamily");
                });

            modelBuilder.Entity("HefestusApi.Models.Produtos.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_ProductGroups_Name");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("HefestusApi.Models.Produtos.ProductSubGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_ProductSubGroups_Name");

                    b.ToTable("ProductSubGroup");
                });

            modelBuilder.Entity("HefestusApi.Models.Vendas.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BruteValue")
                        .HasColumnType("numeric");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<float?>("CostOfFreight")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("DateOfCompletion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<decimal>("LiquidValue")
                        .HasColumnType("numeric");

                    b.Property<int>("PaymentConditionId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentOptionId")
                        .HasColumnType("integer");

                    b.Property<int>("ResponsibleId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("numeric");

                    b.Property<string>("TypeFreight")
                        .HasColumnType("text");

                    b.Property<string>("TypeOrder")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PaymentConditionId");

                    b.HasIndex("PaymentOptionId");

                    b.HasIndex("ResponsibleId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("HefestusApi.Models.Vendas.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("PersonPersonGroup", b =>
                {
                    b.Property<int>("PersonGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonsId")
                        .HasColumnType("integer");

                    b.HasKey("PersonGroupId", "PersonsId");

                    b.HasIndex("PersonsId");

                    b.ToTable("PersonPersonGroup");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.Person", b =>
                {
                    b.HasOne("HefestusApi.Models.Administracao.City", "City")
                        .WithMany("Persons")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Administracao.User", "User")
                        .WithOne("Person")
                        .HasForeignKey("HefestusApi.Models.Administracao.Person", "UserId");

                    b.Navigation("City");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HefestusApi.Models.Produtos.Product", b =>
                {
                    b.HasOne("HefestusApi.Models.Produtos.ProductFamily", "Family")
                        .WithMany()
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Produtos.ProductGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Produtos.ProductSubGroup", "Subgroup")
                        .WithMany()
                        .HasForeignKey("SubgroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("Group");

                    b.Navigation("Subgroup");
                });

            modelBuilder.Entity("HefestusApi.Models.Vendas.Order", b =>
                {
                    b.HasOne("HefestusApi.Models.Administracao.Person", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Financeiro.PaymentCondition", "PaymentCondition")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Financeiro.PaymentOptions", "PaymentOption")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Administracao.Person", "Responsible")
                        .WithMany()
                        .HasForeignKey("ResponsibleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("PaymentCondition");

                    b.Navigation("PaymentOption");

                    b.Navigation("Responsible");
                });

            modelBuilder.Entity("HefestusApi.Models.Vendas.OrderProduct", b =>
                {
                    b.HasOne("HefestusApi.Models.Vendas.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Produtos.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PersonPersonGroup", b =>
                {
                    b.HasOne("HefestusApi.Models.Administracao.PersonGroup", null)
                        .WithMany()
                        .HasForeignKey("PersonGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HefestusApi.Models.Administracao.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.City", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.Person", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.User", b =>
                {
                    b.Navigation("Person")
                        .IsRequired();
                });

            modelBuilder.Entity("HefestusApi.Models.Financeiro.PaymentCondition", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HefestusApi.Models.Financeiro.PaymentOptions", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HefestusApi.Models.Produtos.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("HefestusApi.Models.Vendas.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

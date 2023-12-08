﻿// <auto-generated />
using System;
using HefestusApi.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HefestusApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231208004729_AddCity")]
    partial class AddCity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("IBGENumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Habilities")
                        .HasColumnType("text");

                    b.Property<string>("IBGE")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InscricaoEstadual")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Razao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UrlImage")
                        .HasColumnType("text");

                    b.Property<int>("cityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("cityId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.PersonGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonPersonGroup", b =>
                {
                    b.Property<int>("PersonGroupsId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonsId")
                        .HasColumnType("integer");

                    b.HasKey("PersonGroupsId", "PersonsId");

                    b.HasIndex("PersonsId");

                    b.ToTable("PersonPersonGroup");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.Person", b =>
                {
                    b.HasOne("HefestusApi.Models.Administracao.City", "city")
                        .WithMany("Persons")
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("HefestusApi.Models.Administracao.User", b =>
                {
                    b.HasOne("HefestusApi.Models.Administracao.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("HefestusApi.Models.Administracao.User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("PersonPersonGroup", b =>
                {
                    b.HasOne("HefestusApi.Models.Administracao.PersonGroup", null)
                        .WithMany()
                        .HasForeignKey("PersonGroupsId")
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
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TSENA.Migrations
{
    [DbContext(typeof(ShopManagementeContext))]
    [Migration("20250309191229_CreationUserModel")]
    partial class CreationUserModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TSENA.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ShopCreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ShopLogoNetworkPath")
                        .HasColumnType("longtext");

                    b.Property<string>("ShopName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Shop");
                });
#pragma warning restore 612, 618
        }
    }
}

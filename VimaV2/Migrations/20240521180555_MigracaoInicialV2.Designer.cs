﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VimaV2.Database;


#nullable disable

namespace VimaV2.Migrations
{
    [DbContext(typeof(VimaV2DbContext))]
    [Migration("20240521180555_MigracaoInicialV2")]
    partial class MigracaoInicialV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WordGuess;

namespace WordGuess.Migrations
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20201024115934_Players")]
    partial class Players
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WordGuess.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(7,1)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(50)");

                    b.HasKey("PlayerID");

                    b.ToTable("Players");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PuppyToy.Services.DataAccess;

#nullable disable

namespace PuppyToy.Services.Migrations
{
    [DbContext(typeof(PuppyToyContext))]
    [Migration("20220727133329_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("PuppyToy.Models.Storable.EmlalockFeedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PupDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeDelta")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimeUnit")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmlalockFeedItems");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PuppyToy.Services.DataAccess;

#nullable disable

namespace PuppyToy.Services.Migrations
{
    [DbContext(typeof(PuppyToyContext))]
    partial class PuppyToyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("SessionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeDelta")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimeUnit")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("EmlalockFeedItems");
                });

            modelBuilder.Entity("PuppyToy.Models.Storable.EmlalockSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmlalockSessions");
                });

            modelBuilder.Entity("PuppyToy.Models.Storable.EmlalockFeedItem", b =>
                {
                    b.HasOne("PuppyToy.Models.Storable.EmlalockSession", "Session")
                        .WithMany("FeedItems")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("PuppyToy.Models.Storable.EmlalockSession", b =>
                {
                    b.Navigation("FeedItems");
                });
#pragma warning restore 612, 618
        }
    }
}

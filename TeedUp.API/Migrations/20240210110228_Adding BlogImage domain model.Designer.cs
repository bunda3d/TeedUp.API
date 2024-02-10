﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeedUp.API.Data;

#nullable disable

namespace TeedUp.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240210110228_Adding BlogImage domain model")]
    partial class AddingBlogImagedomainmodel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogPostCategory", b =>
                {
                    b.Property<Guid>("BlogPostsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BlogPostsId", "CategoriesId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("BlogPostCategory");
                });

            modelBuilder.Entity("TeedUp.API.Models.Domain.BlogImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("DECIMAL(12,9)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("DECIMAL(12,9)");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("BlogImages");
                });

            modelBuilder.Entity("TeedUp.API.Models.Domain.BlogPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeaturedImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("TeedUp.API.Models.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BlogPostCategory", b =>
                {
                    b.HasOne("TeedUp.API.Models.Domain.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeedUp.API.Models.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

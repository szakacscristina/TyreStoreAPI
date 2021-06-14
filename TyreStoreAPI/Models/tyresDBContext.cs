using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TyreStoreAPI.Models
{
    public partial class tyresDBContext : DbContext
    {
        public tyresDBContext()
        {
        }

        public tyresDBContext(DbContextOptions<tyresDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tyres> Tyres { get; set; }
        public virtual DbSet<TyresModels> TyresModels { get; set; }
        public virtual DbSet<TyresSizes> TyresSizes { get; set; }
        public virtual DbSet<VehicleManufacturers> VehicleManufacturers { get; set; }
        public virtual DbSet<VehicleModels> VehicleModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=TyresDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tyres>(entity =>
            {
                entity.ToTable("tyres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Brand)
                    .HasColumnName("brand")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Part)
                    .HasColumnName("part")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Season)
                    .HasColumnName("season")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.TyreId).HasColumnName("tyre_id");

                entity.HasOne(d => d.Tyre)
                    .WithMany(p => p.Tyres)
                    .HasForeignKey(d => d.TyreId)
                    .HasConstraintName("FK__tyres__tyre_id__38996AB5");
            });

            modelBuilder.Entity<TyresModels>(entity =>
            {
                entity.ToTable("tyres_models");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.TyreId).HasColumnName("tyre_id");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.TyresModels)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tyres_mod__model__3E52440B");

                entity.HasOne(d => d.Tyre)
                    .WithMany(p => p.TyresModels)
                    .HasForeignKey(d => d.TyreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tyres_mod__tyre___3F466844");
            });

            modelBuilder.Entity<TyresSizes>(entity =>
            {
                entity.ToTable("tyres_sizes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AspectRatio)
                    .HasColumnName("aspect_ratio")
                    .HasColumnType("decimal(38, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RimDiameter)
                    .HasColumnName("rim_diameter")
                    .HasColumnType("decimal(38, 0)");

                entity.Property(e => e.TyreWidth)
                    .HasColumnName("tyre_width")
                    .HasColumnType("decimal(38, 0)");
            });

            modelBuilder.Entity<VehicleManufacturers>(entity =>
            {
                entity.ToTable("vehicle_manufacturers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehicleModels>(entity =>
            {
                entity.ToTable("vehicle_models");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndYear)
                    .HasColumnName("end_year")
                    .HasColumnType("decimal(38, 0)");

                entity.Property(e => e.ManufacturerId)
                    .HasColumnName("manufacturer_id")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .HasColumnName("slug")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartYear)
                    .HasColumnName("start_year")
                    .HasColumnType("decimal(38, 0)");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__vehicle_m__manuf__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

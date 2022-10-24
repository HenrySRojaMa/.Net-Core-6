using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Taller1.Models
{
    public partial class PruebaContext : DbContext
    {

        public PruebaContext(IConfiguration _configuration, DbContextOptions<PruebaContext> options)
            : base(options)
        {
            Configuration = _configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual DbSet<Cabecera> Cabeceras { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Detalle> Detalles { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("PruebaDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabecera>(entity =>
            {
                entity.HasKey(e => e.NumeroCompra);

                entity.ToTable("Cabecera");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Cabeceras)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cabecera_Cliente");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Cedula);

                entity.ToTable("Cliente");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Detalle>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.NumeroCompraNavigation)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(d => d.NumeroCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detalles_Cabecera");

                entity.HasOne(d => d.ProductoNavigation)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(d => d.Producto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detalles_Producto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("Producto");

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Usuario");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Codigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Cliente");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

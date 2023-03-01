﻿// <auto-generated />
using System;
using ActivosFijos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ActivosFijos.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ActivosFijos.Model.ActivoFijo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<double>("DepreciacionAcumulada")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("TipoActivoId")
                        .HasColumnType("int");

                    b.Property<double>("ValorCompra")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("TipoActivoId");

                    b.ToTable("ActivosFijo");
                });

            modelBuilder.Entity("ActivosFijos.Model.AsientosContables", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CuentaContable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaAsiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Inventario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MontoAsiento")
                        .HasColumnType("float");

                    b.Property<int>("TipoMovimiento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AsientosContables");
                });

            modelBuilder.Entity("ActivosFijos.Model.CalculoDepreciacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivoFijoId")
                        .HasColumnType("int");

                    b.Property<int>("AñoProceso")
                        .HasColumnType("int");

                    b.Property<string>("CuentaCompra")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CuentaDepreciacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DepreciacionAcumulada")
                        .HasColumnType("float");

                    b.Property<DateTime?>("FechaProceso")
                        .HasColumnType("datetime2");

                    b.Property<int>("MesProceso")
                        .HasColumnType("int");

                    b.Property<double>("MontoDepreciado")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ActivoFijoId");

                    b.ToTable("CalculoDepreciacion");
                });

            modelBuilder.Entity("ActivosFijos.Model.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("ActivosFijos.Model.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cedula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPersona")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Empleado");
                });

            modelBuilder.Entity("ActivosFijos.Model.TipoActivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CuentaContableCompra")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CuentaContableDepreciacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TipoActivo");
                });

            modelBuilder.Entity("ActivosFijos.Model.ActivoFijo", b =>
                {
                    b.HasOne("ActivosFijos.Model.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActivosFijos.Model.TipoActivo", "TipoActivo")
                        .WithMany("ActivosFijos")
                        .HasForeignKey("TipoActivoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");

                    b.Navigation("TipoActivo");
                });

            modelBuilder.Entity("ActivosFijos.Model.CalculoDepreciacion", b =>
                {
                    b.HasOne("ActivosFijos.Model.ActivoFijo", "ActivosFijos")
                        .WithMany()
                        .HasForeignKey("ActivoFijoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivosFijos");
                });

            modelBuilder.Entity("ActivosFijos.Model.Empleado", b =>
                {
                    b.HasOne("ActivosFijos.Model.Departamento", "Departamento")
                        .WithMany("Empleados")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("ActivosFijos.Model.Departamento", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("ActivosFijos.Model.TipoActivo", b =>
                {
                    b.Navigation("ActivosFijos");
                });
#pragma warning restore 612, 618
        }
    }
}

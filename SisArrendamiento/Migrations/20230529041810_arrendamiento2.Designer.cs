﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SisArrendamiento.Models;

#nullable disable

namespace SisArrendamiento.Migrations
{
    [DbContext(typeof(ArrendamientoWebContext))]
    [Migration("20230529041810_arrendamiento2")]
    partial class arrendamiento2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SisArrendamiento.Models.Alquiler", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<decimal?>("Agua")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("agua");

                    b.Property<decimal?>("AlquilerMensual")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("alquiler_mensual");

                    b.Property<int>("ArrendadorCodigo")
                        .HasColumnType("int")
                        .HasColumnName("arrendador_codigo");

                    b.Property<int>("ArrendatarioCodigo")
                        .HasColumnType("int")
                        .HasColumnName("arrendatario_codigo");

                    b.Property<decimal?>("Cable")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("cable");

                    b.Property<int>("CuartoCodigo")
                        .HasColumnType("int")
                        .HasColumnName("cuarto_codigo");

                    b.Property<DateTime>("FechaActual")
                        .HasColumnType("date")
                        .HasColumnName("fecha_actual");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("date")
                        .HasColumnName("fecha_vencimiento");

                    b.Property<int>("LuzBañoCodigo")
                        .HasColumnType("int")
                        .HasColumnName("luz_baño_codigo");

                    b.Property<int>("LuzCuartoCodigo")
                        .HasColumnType("int")
                        .HasColumnName("luz_cuarto_codigo");

                    b.Property<int>("LuzEscaleraCodigo")
                        .HasColumnType("int")
                        .HasColumnName("luz_escalera_codigo");

                    b.Property<decimal?>("PagoTotal")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("pago_total");

                    b.HasKey("Codigo")
                        .HasName("alquiler_PK");

                    b.HasIndex("ArrendadorCodigo");

                    b.HasIndex("ArrendatarioCodigo");

                    b.HasIndex("CuartoCodigo");

                    b.HasIndex("LuzBañoCodigo");

                    b.HasIndex("LuzCuartoCodigo");

                    b.HasIndex("LuzEscaleraCodigo");

                    b.ToTable("alquiler", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.Arrendador", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<string>("Apellidos")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellidos");

                    b.Property<string>("CedulaIdentidad")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("cedula_identidad");

                    b.Property<string>("Nombres")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombres");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("telefono");

                    b.HasKey("Codigo")
                        .HasName("arrendador_PK");

                    b.ToTable("arrendador", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.Arrendatario", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<string>("Apellidos")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellidos");

                    b.Property<string>("CedulaIdentidad")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("cedula_identidad");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("date")
                        .HasColumnName("fecha_ingreso");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("date")
                        .HasColumnName("fecha_nacimiento");

                    b.Property<string>("Nombres")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombres");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("telefono");

                    b.HasKey("Codigo")
                        .HasName("arrendatario_PK");

                    b.ToTable("arrendatario", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.Conviviente", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<int?>("ArrendatarioCodigo")
                        .HasColumnType("int")
                        .HasColumnName("arrendatario_codigo");

                    b.Property<string>("CedulaIdentidad")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("cedula_identidad");

                    b.Property<string>("Nombres")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombres");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("telefono");

                    b.HasKey("Codigo")
                        .HasName("conviviente_PK");

                    b.HasIndex("ArrendatarioCodigo");

                    b.ToTable("conviviente", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.Cuarto", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<string>("Piso")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("piso");

                    b.Property<string>("Zona")
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("zona");

                    b.HasKey("Codigo")
                        .HasName("cuarto_PK");

                    b.ToTable("cuarto", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.LuzBaño", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<decimal?>("ActualLuzLectura")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("actual_luz_lectura");

                    b.Property<decimal?>("AnteriorLuzLectura")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("anterior_luz_lectura");

                    b.Property<decimal?>("KwConsumido")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("kw_consumido");

                    b.Property<decimal?>("Monto")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("monto");

                    b.HasKey("Codigo")
                        .HasName("luz_baño_PK");

                    b.ToTable("luz_baño", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.LuzCuarto", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<decimal?>("ActualLuzLectura")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("actual_luz_lectura");

                    b.Property<decimal?>("AnteriorLuzLectura")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("anterior_luz_lectura");

                    b.Property<decimal?>("KwConsumido")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("kw_consumido");

                    b.Property<decimal?>("Monto")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("monto");

                    b.HasKey("Codigo")
                        .HasName("luz_cuarto_PK");

                    b.ToTable("luz_cuarto", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.LuzEscalera", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codigo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<decimal?>("ActualLuzLectura")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("actual_luz_lectura");

                    b.Property<decimal?>("AnteriorLuzLectura")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("anterior_luz_lectura");

                    b.Property<decimal?>("KwConsumido")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("kw_consumido");

                    b.Property<decimal?>("Monto")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("monto");

                    b.HasKey("Codigo")
                        .HasName("luz_escalera_PK");

                    b.ToTable("luz_escalera", (string)null);
                });

            modelBuilder.Entity("SisArrendamiento.Models.Alquiler", b =>
                {
                    b.HasOne("SisArrendamiento.Models.Arrendador", "ArrendadorCodigoNavigation")
                        .WithMany("Alquilers")
                        .HasForeignKey("ArrendadorCodigo")
                        .IsRequired()
                        .HasConstraintName("alquiler_arrendador_FK");

                    b.HasOne("SisArrendamiento.Models.Arrendatario", "ArrendatarioCodigoNavigation")
                        .WithMany("Alquilers")
                        .HasForeignKey("ArrendatarioCodigo")
                        .IsRequired()
                        .HasConstraintName("alquiler_arrendatario_FK");

                    b.HasOne("SisArrendamiento.Models.Cuarto", "CuartoCodigoNavigation")
                        .WithMany("Alquilers")
                        .HasForeignKey("CuartoCodigo")
                        .IsRequired()
                        .HasConstraintName("alquiler_cuarto_FK");

                    b.HasOne("SisArrendamiento.Models.LuzBaño", "LuzBañoCodigoNavigation")
                        .WithMany("Alquilers")
                        .HasForeignKey("LuzBañoCodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("alquiler_luz_baño_FK");

                    b.HasOne("SisArrendamiento.Models.LuzCuarto", "LuzCuartoCodigoNavigation")
                        .WithMany("Alquilers")
                        .HasForeignKey("LuzCuartoCodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("alquiler_luz_cuarto_FK");

                    b.HasOne("SisArrendamiento.Models.LuzEscalera", "LuzEscaleraCodigoNavigation")
                        .WithMany("Alquilers")
                        .HasForeignKey("LuzEscaleraCodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("alquiler_luz_escalera_FK");

                    b.Navigation("ArrendadorCodigoNavigation");

                    b.Navigation("ArrendatarioCodigoNavigation");

                    b.Navigation("CuartoCodigoNavigation");

                    b.Navigation("LuzBañoCodigoNavigation");

                    b.Navigation("LuzCuartoCodigoNavigation");

                    b.Navigation("LuzEscaleraCodigoNavigation");
                });

            modelBuilder.Entity("SisArrendamiento.Models.Conviviente", b =>
                {
                    b.HasOne("SisArrendamiento.Models.Arrendatario", "ArrendatarioCodigoNavigation")
                        .WithMany("Convivientes")
                        .HasForeignKey("ArrendatarioCodigo")
                        .HasConstraintName("conviviente_arrendatario_FK");

                    b.Navigation("ArrendatarioCodigoNavigation");
                });

            modelBuilder.Entity("SisArrendamiento.Models.Arrendador", b =>
                {
                    b.Navigation("Alquilers");
                });

            modelBuilder.Entity("SisArrendamiento.Models.Arrendatario", b =>
                {
                    b.Navigation("Alquilers");

                    b.Navigation("Convivientes");
                });

            modelBuilder.Entity("SisArrendamiento.Models.Cuarto", b =>
                {
                    b.Navigation("Alquilers");
                });

            modelBuilder.Entity("SisArrendamiento.Models.LuzBaño", b =>
                {
                    b.Navigation("Alquilers");
                });

            modelBuilder.Entity("SisArrendamiento.Models.LuzCuarto", b =>
                {
                    b.Navigation("Alquilers");
                });

            modelBuilder.Entity("SisArrendamiento.Models.LuzEscalera", b =>
                {
                    b.Navigation("Alquilers");
                });
#pragma warning restore 612, 618
        }
    }
}

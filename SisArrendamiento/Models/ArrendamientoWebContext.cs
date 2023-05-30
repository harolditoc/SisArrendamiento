using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SisArrendamiento.Models;

public partial class ArrendamientoWebContext : DbContext
{
    public ArrendamientoWebContext()
    {
    }

    public ArrendamientoWebContext(DbContextOptions<ArrendamientoWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alquiler> Alquilers { get; set; }

    public virtual DbSet<Arrendador> Arrendadors { get; set; }

    public virtual DbSet<Arrendatario> Arrendatarios { get; set; }

    public virtual DbSet<Conviviente> Convivientes { get; set; }

    public virtual DbSet<Cuarto> Cuartos { get; set; }

    public virtual DbSet<LuzBaño> LuzBaños { get; set; }

    public virtual DbSet<LuzCuarto> LuzCuartos { get; set; }

    public virtual DbSet<LuzEscalera> LuzEscaleras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-H31JF77\\SQLEXPRESS; Database=ArrendamientoWeb2; Trusted_Connection=True; TrustServerCertificate=True;");
        //=> optionsBuilder.UseSqlServer("Data source=ksperu.com;Initial Catalog=ArrendamientoWeb;User ID=sqlArrendamiento;Password=arrendamiento2023;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alquiler>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("alquiler_PK");

            entity.ToTable("alquiler");

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.Agua)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("agua");
            entity.Property(e => e.AlquilerMensual)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("alquiler_mensual");
            entity.Property(e => e.ArrendadorCodigo).HasColumnName("arrendador_codigo");
            entity.Property(e => e.ArrendatarioCodigo).HasColumnName("arrendatario_codigo");
            entity.Property(e => e.Cable)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("cable");
            entity.Property(e => e.CuartoCodigo).HasColumnName("cuarto_codigo");
            entity.Property(e => e.FechaActual)
                .HasColumnType("date")
                .HasColumnName("fecha_actual");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_vencimiento");
            entity.Property(e => e.LuzBañoCodigo).HasColumnName("luz_baño_codigo");
            entity.Property(e => e.LuzCuartoCodigo).HasColumnName("luz_cuarto_codigo");
            entity.Property(e => e.LuzEscaleraCodigo).HasColumnName("luz_escalera_codigo");
            entity.Property(e => e.PagoTotal)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("pago_total");

            entity.HasOne(d => d.ArrendadorCodigoNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.ArrendadorCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("alquiler_arrendador_FK");

            entity.HasOne(d => d.ArrendatarioCodigoNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.ArrendatarioCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("alquiler_arrendatario_FK");

            entity.HasOne(d => d.CuartoCodigoNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.CuartoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("alquiler_cuarto_FK");

            entity.HasOne(d => d.LuzBañoCodigoNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.LuzBañoCodigo)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("alquiler_luz_baño_FK");

            entity.HasOne(d => d.LuzCuartoCodigoNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.LuzCuartoCodigo)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("alquiler_luz_cuarto_FK");

            entity.HasOne(d => d.LuzEscaleraCodigoNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.LuzEscaleraCodigo)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("alquiler_luz_escalera_FK");
        });

        modelBuilder.Entity<Arrendador>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("arrendador_PK");

            entity.ToTable("arrendador");

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.CedulaIdentidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula_identidad");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Arrendatario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("arrendatario_PK");

            entity.ToTable("arrendatario");

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.Apellidos)   
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.CedulaIdentidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula_identidad");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("date")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Conviviente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("conviviente_PK");

            entity.ToTable("conviviente");

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.ArrendatarioCodigo).HasColumnName("arrendatario_codigo");
            entity.Property(e => e.CedulaIdentidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula_identidad");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.ArrendatarioCodigoNavigation).WithMany(p => p.Convivientes)
                .HasForeignKey(d => d.ArrendatarioCodigo)
                .HasConstraintName("conviviente_arrendatario_FK");
        });

        modelBuilder.Entity<Cuarto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("cuarto_PK");

            entity.ToTable("cuarto");

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.Piso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("piso");
            entity.Property(e => e.Zona)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("zona");
        });

        modelBuilder.Entity<LuzBaño>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("luz_baño_PK");

            entity.ToTable("luz_baño");

            //entity.HasMany(c => c.Alquilers).WithOne(d => d.LuzBañoCodigoNavigation).OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.ActualLuzLectura)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("actual_luz_lectura");
            entity.Property(e => e.AnteriorLuzLectura)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("anterior_luz_lectura");
            entity.Property(e => e.KwConsumido)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("kw_consumido");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("monto");
        });

        modelBuilder.Entity<LuzCuarto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("luz_cuarto_PK");

            entity.ToTable("luz_cuarto");

            //entity.HasMany(c => c.Alquilers).WithOne(d => d.LuzCuartoCodigoNavigation).OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.ActualLuzLectura)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("actual_luz_lectura");
            entity.Property(e => e.AnteriorLuzLectura)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("anterior_luz_lectura");
            entity.Property(e => e.KwConsumido)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("kw_consumido");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("monto");
        });

        modelBuilder.Entity<LuzEscalera>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("luz_escalera_PK");

            entity.ToTable("luz_escalera");

            //entity.HasMany(c => c.Alquilers).WithOne(d => d.LuzEscaleraCodigoNavigation).OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Codigo)
                .HasColumnName("codigo")
                .UseIdentityColumn(1, 1);
            entity.Property(e => e.ActualLuzLectura)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("actual_luz_lectura");
            entity.Property(e => e.AnteriorLuzLectura)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("anterior_luz_lectura");
            entity.Property(e => e.KwConsumido)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("kw_consumido");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("monto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

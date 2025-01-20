using System;
using System.Collections.Generic;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.DAL.Commerce.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Bodega> Bodegas { get; set; }

    public virtual DbSet<ExistenciaBodega> ExistenciaBodegas { get; set; }

    public virtual DbSet<ExistenciaLote> ExistenciaLotes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("pgsodium", "key_status", new[] { "default", "valid", "invalid", "expired" })
            .HasPostgresEnum("pgsodium", "key_type", new[] { "aead-ietf", "aead-det", "hmacsha512", "hmacsha256", "auth", "shorthash", "generichash", "kdf", "secretbox", "secretstream", "stream_xchacha20" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "pgjwt")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("pg_catalog", "pg_cron")
            .HasPostgresExtension("pgsodium", "pgsodium")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Articulo1).HasName("articulo_pkey");

            entity.ToTable("articulo", "softbytecommerce");

            entity.Property(e => e.Articulo1)
                .HasMaxLength(50)
                .HasColumnName("articulo");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Categoria)
                .HasMaxLength(150)
                .HasColumnName("categoria");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("createdby");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactualizacion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.PesoBruto)
                .HasPrecision(10, 2)
                .HasColumnName("peso_bruto");
            entity.Property(e => e.PesoNeto)
                .HasPrecision(10, 2)
                .HasColumnName("peso_neto");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.SubCategoria)
                .HasComment("Se usa para guardar las subcategorías de los productos")
                .HasColumnType("character varying");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .HasColumnName("updatedby");
            entity.Property(e => e.Volumen)
                .HasPrecision(10, 2)
                .HasColumnName("volumen");
        });

        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.Bodega1).HasName("bodegas_pkey");

            entity.ToTable("bodegas", "softbytecommerce");

            entity.Property(e => e.Bodega1)
                .HasMaxLength(50)
                .HasColumnName("bodega");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Bodegacentral)
                .HasDefaultValue(false)
                .HasColumnName("bodegacentral");
            entity.Property(e => e.Bodegasecundaria)
                .HasDefaultValue(false)
                .HasColumnName("bodegasecundaria");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("createdby");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .HasColumnName("departamento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactualizacion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Municipio)
                .HasMaxLength(100)
                .HasColumnName("municipio");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .HasColumnName("updatedby");
        });

        modelBuilder.Entity<ExistenciaBodega>(entity =>
        {
            entity.HasKey(e => new { e.Bodega, e.Articulo }).HasName("existencia_bodega_pkey");

            entity.ToTable("existencia_bodega", "softbytecommerce");

            entity.Property(e => e.Bodega)
                .HasMaxLength(50)
                .HasColumnName("bodega");
            entity.Property(e => e.Articulo)
                .HasMaxLength(50)
                .HasColumnName("articulo");
            entity.Property(e => e.BloqueaTrans)
                .HasDefaultValue(false)
                .HasColumnName("bloquea_trans");
            entity.Property(e => e.CantDisponible)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_disponible");
            entity.Property(e => e.CantNoAprobada)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_no_aprobada");
            entity.Property(e => e.CantPedida)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_pedida");
            entity.Property(e => e.CantProduccion)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_produccion");
            entity.Property(e => e.CantRemitida)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_remitida");
            entity.Property(e => e.CantReservada)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_reservada");
            entity.Property(e => e.CantTransito)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_transito");
            entity.Property(e => e.CantVencida)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_vencida");
            entity.Property(e => e.Congelado)
                .HasDefaultValue(false)
                .HasColumnName("congelado");
            entity.Property(e => e.CostoPromComparativoDolar)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_prom_comparativo_dolar");
            entity.Property(e => e.CostoPromComparativoLoc)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_prom_comparativo_loc");
            entity.Property(e => e.CostoUntEstandarDol)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_estandar_dol");
            entity.Property(e => e.CostoUntEstandarLoc)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_estandar_loc");
            entity.Property(e => e.CostoUntPromedioDol)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_promedio_dol");
            entity.Property(e => e.CostoUntPromedioLoc)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_promedio_loc");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("createdby");
            entity.Property(e => e.ExistenciaMaxima)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("existencia_maxima");
            entity.Property(e => e.ExistenciaMinima)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("existencia_minima");
            entity.Property(e => e.FechaCong)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cong");
            entity.Property(e => e.FechaDescong)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_descong");
            entity.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactualizacion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.PuntoDeReorden)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("punto_de_reorden");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .HasColumnName("updatedby");

            entity.HasOne(d => d.ArticuloNavigation).WithMany(p => p.ExistenciaBodegas)
                .HasForeignKey(d => d.Articulo)
                .HasConstraintName("fk_existencia_bodega_articulo");

            entity.HasOne(d => d.BodegaNavigation).WithMany(p => p.ExistenciaBodegas)
                .HasForeignKey(d => d.Bodega)
                .HasConstraintName("fk_existencia_bodega_bodega");
        });

        modelBuilder.Entity<ExistenciaLote>(entity =>
        {
            entity.HasKey(e => new { e.Bodega, e.Articulo, e.Lote }).HasName("existencia_lote_pkey");

            entity.ToTable("existencia_lote", "softbytecommerce");

            entity.Property(e => e.Bodega)
                .HasMaxLength(50)
                .HasColumnName("bodega");
            entity.Property(e => e.Articulo)
                .HasMaxLength(50)
                .HasColumnName("articulo");
            entity.Property(e => e.Lote)
                .HasMaxLength(50)
                .HasColumnName("lote");
            entity.Property(e => e.CantDisponible)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_disponible");
            entity.Property(e => e.CantNoAprobada)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_no_aprobada");
            entity.Property(e => e.CantRemitida)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_remitida");
            entity.Property(e => e.CantReservada)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_reservada");
            entity.Property(e => e.CantVencida)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cant_vencida");
            entity.Property(e => e.CostoUntEstandarDol)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_estandar_dol");
            entity.Property(e => e.CostoUntEstandarLoc)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_estandar_loc");
            entity.Property(e => e.CostoUntPromedioDol)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_promedio_dol");
            entity.Property(e => e.CostoUntPromedioLoc)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costo_unt_promedio_loc");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("createdby");
            entity.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactualizacion");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Localizacion)
                .HasMaxLength(100)
                .HasColumnName("localizacion");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .HasColumnName("updatedby");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuario1).HasName("usuario_pkey");

            entity.ToTable("usuario", "softbytecommerce");

            entity.HasIndex(e => e.CorreoElectronico, "usuario_correo_electronico_key").IsUnique();

            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .HasColumnName("celular");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .HasColumnName("clave");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(255)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .HasColumnName("createdby");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.DocumentoIdentificacion)
                .HasMaxLength(50)
                .HasColumnName("documento_identificacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaUltClave)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ult_clave");
            entity.Property(e => e.FotoUrl)
                .HasMaxLength(255)
                .HasColumnName("foto_url");
            entity.Property(e => e.FrecuenciaClave)
                .HasDefaultValue((short)0)
                .HasColumnName("frecuencia_clave");
            entity.Property(e => e.MaxIntentosConex)
                .HasDefaultValue((short)5)
                .HasColumnName("max_intentos_conex");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Noteexistsflag)
                .HasDefaultValue(false)
                .HasColumnName("noteexistsflag");
            entity.Property(e => e.Recorddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorddate");
            entity.Property(e => e.ReqCambioClave)
                .HasDefaultValue(false)
                .HasColumnName("req_cambio_clave");
            entity.Property(e => e.Rowpointer)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("rowpointer");
            entity.Property(e => e.Telefono1)
                .HasMaxLength(15)
                .HasColumnName("telefono_1");
            entity.Property(e => e.Telefono2)
                .HasMaxLength(15)
                .HasColumnName("telefono_2");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
            entity.Property(e => e.TipoAcceso)
                .HasMaxLength(50)
                .HasColumnName("tipo_acceso");
            entity.Property(e => e.TipoPersonalizado)
                .HasMaxLength(50)
                .HasColumnName("tipo_personalizado");
            entity.Property(e => e.Updatedate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .HasColumnName("updatedby");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

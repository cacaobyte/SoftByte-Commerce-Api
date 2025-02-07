using System;
using System.Collections.Generic;
using CommerceCore.ML;
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
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ExistenciaBodega> ExistenciaBodegas { get; set; }

    public virtual DbSet<ExistenciaLote> ExistenciaLotes { get; set; }
    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Guia> Guias { get; set; }
    public virtual DbSet<Regione> Regiones { get; set; }
    public virtual DbSet<Subcategoria> Subcategorias { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Vendedore> Vendedores { get; set; }



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


        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categorias_pkey");

            entity.ToTable("categorias", "softbytecommerce");

            entity.HasIndex(e => e.Nombre, "categorias_nombre_key").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .HasColumnName("update_by");
        });


        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Cliente1).HasName("clientes_pkey");

            entity.ToTable("clientes", "softbytecommerce");

            entity.HasIndex(e => e.Dpi, "clientes_dpi_key").IsUnique();

            entity.HasIndex(e => e.Email, "clientes_email_key").IsUnique();

            entity.HasIndex(e => e.Nit, "clientes_nit_key").IsUnique();

            entity.Property(e => e.Cliente1)
                .HasMaxLength(10)
                .HasColumnName("cliente");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .HasColumnName("celular");
            entity.Property(e => e.Celular2)
                .HasMaxLength(15)
                .HasColumnName("celular2");
            entity.Property(e => e.Cf).HasColumnName("cf");
            entity.Property(e => e.Colonia)
                .HasMaxLength(100)
                .HasColumnName("colonia");
            entity.Property(e => e.Createby)
                .HasMaxLength(50)
                .HasColumnName("createby");
            entity.Property(e => e.Createdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .HasColumnName("departamento");
            entity.Property(e => e.Descuento)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("descuento");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Dpi)
                .HasMaxLength(20)
                .HasColumnName("dpi");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .HasColumnName("empresa");
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .HasColumnName("estado_civil");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .HasColumnName("genero");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .HasColumnName("moneda");
            entity.Property(e => e.Municipio)
                .HasMaxLength(100)
                .HasColumnName("municipio");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nit)
                .HasMaxLength(15)
                .HasColumnName("nit");
            entity.Property(e => e.NombreFactura)
                .HasMaxLength(100)
                .HasColumnName("nombre_factura");
            entity.Property(e => e.Notificar)
                .HasDefaultValue(false)
                .HasColumnName("notificar");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.Profesion)
                .HasMaxLength(100)
                .HasColumnName("profesion");
            entity.Property(e => e.Recorddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorddate");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(50)
                .HasColumnName("segundo_nombre");
            entity.Property(e => e.TercerNombre)
                .HasMaxLength(50)
                .HasColumnName("tercer_nombre");
            entity.Property(e => e.Updateby)
                .HasMaxLength(50)
                .HasColumnName("updateby");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Zona)
                .HasMaxLength(10)
                .HasColumnName("zona");
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




        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("faqs_pkey");

            entity.ToTable("faqs", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(50)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.CreadoPor)
                .HasMaxLength(50)
                .HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Importancia)
                .HasDefaultValue(1)
                .HasColumnName("importancia");
            entity.Property(e => e.Keywords).HasColumnName("keywords");
            entity.Property(e => e.Orden)
                .HasDefaultValue(1)
                .HasColumnName("orden");
            entity.Property(e => e.Pregunta).HasColumnName("pregunta");
            entity.Property(e => e.Respuesta).HasColumnName("respuesta");
            entity.Property(e => e.Subcategoria)
                .HasMaxLength(100)
                .HasColumnName("subcategoria");
            entity.Property(e => e.Visitas)
                .HasDefaultValue(0)
                .HasColumnName("visitas");
        });

        modelBuilder.Entity<Guia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guias_pkey");

            entity.ToTable("guias", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(50)
                .HasColumnName("actualizado_por");
            entity.Property(e => e.Archivo).HasColumnName("archivo");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.Contenido).HasColumnName("contenido");
            entity.Property(e => e.CreadoPor)
                .HasMaxLength(50)
                .HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Idioma)
                .HasMaxLength(20)
                .HasColumnName("idioma");
            entity.Property(e => e.Importancia)
                .HasDefaultValue(1)
                .HasColumnName("importancia");
            entity.Property(e => e.Keywords).HasColumnName("keywords");
            entity.Property(e => e.Orden)
                .HasDefaultValue(1)
                .HasColumnName("orden");
            entity.Property(e => e.Subcategoria)
                .HasMaxLength(100)
                .HasColumnName("subcategoria");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
            entity.Property(e => e.UrlExterna).HasColumnName("url_externa");
            entity.Property(e => e.Version)
                .HasMaxLength(20)
                .HasColumnName("version");
            entity.Property(e => e.Visitas)
                .HasDefaultValue(0)
                .HasColumnName("visitas");
        });

        modelBuilder.Entity<Regione>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("regiones_pkey");

            entity.ToTable("regiones", "softbytecommerce");

            entity.HasIndex(e => e.Codigo, "regiones_codigo_key").IsUnique();

            entity.HasIndex(e => e.Nombre, "regiones_nombre_key").IsUnique();

            entity.Property(e => e.IdRegion).HasColumnName("id_region");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoRegion)
                .HasMaxLength(100)
                .HasColumnName("tipo_region");
        });

        modelBuilder.Entity<Subcategoria>(entity =>
        {
            entity.HasKey(e => e.IdSubcategoria).HasName("subcategorias_pkey");

            entity.ToTable("subcategorias", "softbytecommerce");

            entity.Property(e => e.IdSubcategoria).HasColumnName("id_subcategoria");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .HasColumnName("update_by");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Subcategoria)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("subcategorias_id_categoria_fkey");
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


        modelBuilder.Entity<Vendedore>(entity =>
        {
            entity.HasKey(e => e.Vendedor).HasName("vendedores_pkey");

            entity.ToTable("vendedores", "softbytecommerce");

            entity.HasIndex(e => e.Correo, "vendedores_correo_key").IsUnique();

            entity.HasIndex(e => e.Dpi, "vendedores_dpi_key").IsUnique();

            entity.Property(e => e.Vendedor).HasColumnName("vendedor");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.Bodega)
                .HasMaxLength(10)
                .HasColumnName("bodega");
            entity.Property(e => e.Bono)
                .HasPrecision(10, 2)
                .HasColumnName("bono");
            entity.Property(e => e.Comentarios).HasColumnName("comentarios");
            entity.Property(e => e.Comision)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("comision");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Createby)
                .HasMaxLength(50)
                .HasColumnName("createby");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Dpi)
                .HasMaxLength(20)
                .HasColumnName("dpi");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .HasColumnName("empresa");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .HasColumnName("genero");
            entity.Property(e => e.HorarioTrabajo).HasColumnName("horario_trabajo");
            entity.Property(e => e.MetaVentas)
                .HasPrecision(10, 2)
                .HasColumnName("meta_ventas");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.Notificar)
                .HasDefaultValue(true)
                .HasColumnName("notificar");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Profesion)
                .HasMaxLength(100)
                .HasColumnName("profesion");
            entity.Property(e => e.Recorddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorddate");
            entity.Property(e => e.SueldoBase)
                .HasPrecision(10, 2)
                .HasColumnName("sueldo_base");
            entity.Property(e => e.Telefono1)
                .HasMaxLength(15)
                .HasColumnName("telefono1");
            entity.Property(e => e.Telefono2)
                .HasMaxLength(15)
                .HasColumnName("telefono2");
            entity.Property(e => e.TipoContrato)
                .HasMaxLength(50)
                .HasColumnName("tipo_contrato");
            entity.Property(e => e.UltimaVenta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultima_venta");
            entity.Property(e => e.Updateby)
                .HasMaxLength(50)
                .HasColumnName("updateby");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");

           /* entity.HasOne(d => d.BodegaNavigation).WithMany(p => p.Vendedores)
                .HasForeignKey(d => d.Bodega)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vendedores_bodega_fkey");*/
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

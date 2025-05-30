﻿using System;
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
    public virtual DbSet<Accion> Accions { get; set; }

    public virtual DbSet<Agrupador> Agrupadors { get; set; }

    public virtual DbSet<Aplicacion> Aplicacions { get; set; }

    public virtual DbSet<Aplicacionconfiguracion> Aplicacionconfiguracions { get; set; }

    public virtual DbSet<Aplicacionhost> Aplicacionhosts { get; set; }
    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Bodega> Bodegas { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CotizacionDetalle> CotizacionDetalles { get; set; }

    public virtual DbSet<Cotizacione> Cotizaciones { get; set; }
    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }
    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<ExistenciaBodega> ExistenciaBodegas { get; set; }

    public virtual DbSet<ExistenciaLote> ExistenciaLotes { get; set; }
    public virtual DbSet<Externaltoken> Externaltokens { get; set; }
    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Guia> Guias { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }
    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Regione> Regiones { get; set; }
    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Rolopcion> Rolopcions { get; set; }

    public virtual DbSet<Rolopcionaccion> Rolopcionaccions { get; set; }

    public virtual DbSet<Rolusuario> Rolusuarios { get; set; }
    public virtual DbSet<Subcategoria> Subcategorias { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Usuarioopcion> Usuarioopcions { get; set; }

    public virtual DbSet<Usuarioopcionaccion> Usuarioopcionaccions { get; set; }
    public virtual DbSet<Vendedore> Vendedores { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accion_pkey");

            entity.ToTable("accion", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Opcion).HasColumnName("opcion");

            entity.HasOne(d => d.OpcionNavigation).WithMany(p => p.Accions)
                .HasForeignKey(d => d.Opcion)
                .HasConstraintName("accion_opcion_fkey");
        });

        modelBuilder.Entity<Agrupador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("agrupador_pkey");

            entity.ToTable("agrupador", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Menu).HasColumnName("menu");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Ordenmostrar).HasColumnName("ordenmostrar");
            entity.Property(e => e.Pathicono)
                .HasMaxLength(255)
                .HasColumnName("pathicono");
            entity.Property(e => e.Texto)
                .HasMaxLength(255)
                .HasColumnName("texto");

            entity.HasOne(d => d.MenuNavigation).WithMany(p => p.Agrupadors)
                .HasForeignKey(d => d.Menu)
                .HasConstraintName("agrupador_menu_fkey");
        });

        modelBuilder.Entity<Aplicacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aplicacion_pkey");

            entity.ToTable("aplicacion", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accesstoken)
                .HasMaxLength(255)
                .HasColumnName("accesstoken");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Appkey)
                .HasMaxLength(255)
                .HasColumnName("appkey");
            entity.Property(e => e.Cookieexpire)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cookieexpire");
            entity.Property(e => e.Empresa).HasColumnName("empresa");
            entity.Property(e => e.Interno).HasColumnName("interno");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.EmpresaNavigation).WithMany(p => p.Aplicacions)
                .HasForeignKey(d => d.Empresa)
                .HasConstraintName("aplicacion_empresa_fkey");
        });

        modelBuilder.Entity<Aplicacionconfiguracion>(entity =>
        {
            entity.HasKey(e => e.Aplicacion).HasName("aplicacionconfiguracion_pkey");

            entity.ToTable("aplicacionconfiguracion", "softbytecommerce");

            entity.Property(e => e.Aplicacion)
                .ValueGeneratedNever()
                .HasColumnName("aplicacion");
            entity.Property(e => e.Cssclass)
                .HasMaxLength(255)
                .HasColumnName("cssclass");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo");
            entity.Property(e => e.Mostrarnombreempresa).HasColumnName("mostrarnombreempresa");
            entity.Property(e => e.Nombremostrar)
                .HasMaxLength(255)
                .HasColumnName("nombremostrar");
            entity.Property(e => e.Textboton)
                .HasMaxLength(255)
                .HasColumnName("textboton");

            entity.HasOne(d => d.AplicacionNavigation).WithOne(p => p.Aplicacionconfiguracion)
                .HasForeignKey<Aplicacionconfiguracion>(d => d.Aplicacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aplicacionconfiguracion_aplicacion_fkey");
        });

        modelBuilder.Entity<Aplicacionhost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aplicacionhost_pkey");

            entity.ToTable("aplicacionhost", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aplicacion).HasColumnName("aplicacion");
            entity.Property(e => e.Dominio)
                .HasMaxLength(255)
                .HasColumnName("dominio");

            entity.HasOne(d => d.AplicacionNavigation).WithMany(p => p.Aplicacionhosts)
                .HasForeignKey(d => d.Aplicacion)
                .HasConstraintName("aplicacionhost_aplicacion_fkey");
        });



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
            entity.Property(e => e.Aplicacion).HasColumnName("aplicacion");


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

            entity.Property(e => e.Aplicacion).HasColumnName("aplicacion");
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


        modelBuilder.Entity<CotizacionDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCotizacion).HasName("cotizacion_detalle_pkey");

            entity.ToTable("cotizacion_detalle", "softbytecommerce");

            entity.Property(e => e.IdDetalleCotizacion).HasColumnName("id_detalle_cotizacion");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DescuentoAplicado)
                .HasPrecision(5, 2)
                .HasColumnName("descuento_aplicado");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");
            entity.Property(e => e.IdCotizacion).HasColumnName("id_cotizacion");
            entity.Property(e => e.Impuestos)
                .HasPrecision(10, 2)
                .HasColumnName("impuestos");
            entity.Property(e => e.NombreArticulo)
                .HasMaxLength(150)
                .HasColumnName("nombre_articulo");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
            entity.Property(e => e.UsuarioActualiza)
                .HasMaxLength(255)
                .HasColumnName("usuario_actualiza");
            entity.Property(e => e.UsuarioCreador)
                .HasMaxLength(255)
                .HasColumnName("usuario_creador");
        });

        modelBuilder.Entity<Cotizacione>(entity =>
        {
            entity.HasKey(e => e.IdCotizacion).HasName("cotizaciones_pkey");

            entity.ToTable("cotizaciones", "softbytecommerce");

            entity.Property(e => e.IdCotizacion).HasColumnName("id_cotizacion");
            entity.Property(e => e.ApellidoCliente)
                .HasMaxLength(100)
                .HasColumnName("apellido_cliente");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.DescuentoCliente)
                .HasPrecision(5, 2)
                .HasColumnName("descuento_cliente");
            entity.Property(e => e.DescuentoTotal)
                .HasPrecision(10, 2)
                .HasColumnName("descuento_total");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Impuestos)
                .HasPrecision(10, 2)
                .HasColumnName("impuestos");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .HasColumnName("moneda");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(100)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.Notas).HasColumnName("notas");
            entity.Property(e => e.Origen)
                .HasMaxLength(20)
                .HasColumnName("origen");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(20)
                .HasColumnName("tipo_pago");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
            entity.Property(e => e.UsuarioActualiza)
                .HasMaxLength(255)
                .HasColumnName("usuario_actualiza");
            entity.Property(e => e.UsuarioAprueba)
                .HasMaxLength(255)
                .HasColumnName("usuario_aprueba");
            entity.Property(e => e.UsuarioCreador)
                .HasMaxLength(255)
                .HasColumnName("usuario_creador");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("departamentos_pkey");

            entity.ToTable("departamentos", "softbytecommerce");

            entity.Property(e => e.IdDepartamento)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_departamento");
            entity.Property(e => e.Aplicación)
                .HasComment("relación con la aplicación")
                .HasColumnName("aplicación");
            entity.Property(e => e.CodigoDepartamento)
                .HasMaxLength(50)
                .HasColumnName("codigo_departamento");
            entity.Property(e => e.CorreoContacto)
                .HasMaxLength(150)
                .HasColumnName("correo_contacto");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.ExtensionTelefonica)
                .HasMaxLength(20)
                .HasColumnName("extension_telefonica");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(150)
                .HasColumnName("nombre_departamento");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.UbicacionFisica)
                .HasMaxLength(150)
                .HasColumnName("ubicacion_fisica");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            //entity.HasOne(d => d.AplicaciónNavigation).WithMany(p => p.Departamentos)
            //    .HasForeignKey(d => d.Aplicación)
            //    .HasConstraintName("departamentos_aplicación_fkey");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("empleados_pkey");

            entity.ToTable("empleados", "softbytecommerce");

            entity.Property(e => e.IdEmpleado)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_empleado");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.ContactoEmergenciaDireccion).HasColumnName("contacto_emergencia_direccion");
            entity.Property(e => e.ContactoEmergenciaNombre)
                .HasMaxLength(100)
                .HasColumnName("contacto_emergencia_nombre");
            entity.Property(e => e.ContactoEmergenciaParentesco)
                .HasMaxLength(50)
                .HasColumnName("contacto_emergencia_parentesco");
            entity.Property(e => e.ContactoEmergenciaTelefono)
                .HasMaxLength(30)
                .HasColumnName("contacto_emergencia_telefono");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .HasColumnName("correo");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Departamento).HasColumnName("departamento");
            entity.Property(e => e.DepartamentoResidencia)
                .HasMaxLength(100)
                .HasColumnName("departamento_residencia");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.EstadoEmpleado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado_empleado");
            entity.Property(e => e.FechaEgreso).HasColumnName("fecha_egreso");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .HasColumnName("foto");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .HasColumnName("genero");
            entity.Property(e => e.Jornada)
                .HasMaxLength(50)
                .HasColumnName("jornada");
            entity.Property(e => e.MunicipioResidencia)
                .HasMaxLength(100)
                .HasColumnName("municipio_residencia");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(100)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.NotasInternas).HasColumnName("notas_internas");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .HasColumnName("numero_documento");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Puesto).HasColumnName("puesto");
            entity.Property(e => e.Salario)
                .HasPrecision(12, 2)
                .HasColumnName("salario");
            entity.Property(e => e.Telefono1)
                .HasMaxLength(30)
                .HasColumnName("telefono1");
            entity.Property(e => e.Telefono2)
                .HasMaxLength(30)
                .HasColumnName("telefono2");
            entity.Property(e => e.TipoContrato)
                .HasMaxLength(50)
                .HasColumnName("tipo_contrato");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(20)
                .HasColumnName("tipo_documento");
            entity.Property(e => e.TipoEmpleado)
                .HasMaxLength(50)
                .HasColumnName("tipo_empleado");
            entity.Property(e => e.TipoSangre)
                .HasMaxLength(5)
                .HasColumnName("tipo_sangre");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            //entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.Empleados)
            //    .HasForeignKey(d => d.Departamento)
            //    .HasConstraintName("empleados_departamento_fkey");

            //entity.HasOne(d => d.PuestoNavigation).WithMany(p => p.Empleados)
            //    .HasForeignKey(d => d.Puesto)
            //    .HasConstraintName("empleados_puesto_fkey");
        });



        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("empresa_pkey");

            entity.ToTable("empresa", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
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



        modelBuilder.Entity<Externaltoken>(entity =>
        {
            entity.HasKey(e => new { e.Usuario, e.Llave }).HasName("externaltoken_pkey");

            entity.ToTable("externaltoken", "softbytecommerce");

            entity.Property(e => e.Usuario).HasColumnName("usuario");
            entity.Property(e => e.Llave)
                .HasMaxLength(255)
                .HasColumnName("llave");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Empresa).HasColumnName("empresa");
            entity.Property(e => e.Expire)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expire");

         /*   entity.HasOne(d => d.EmpresaNavigation).WithMany(p => p.Externaltokens)
                .HasForeignKey(d => d.Empresa)
                .HasConstraintName("externaltoken_empresa_fkey");*/
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

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_pkey");

            entity.ToTable("menu", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Aplicacion).HasColumnName("aplicacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.AplicacionNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.Aplicacion)
                .HasConstraintName("menu_aplicacion_fkey");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("opcion_pkey");

            entity.ToTable("opcion", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Agrupador).HasColumnName("agrupador");
            entity.Property(e => e.Menu).HasColumnName("menu");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Ordenmostrar).HasColumnName("ordenmostrar");
            entity.Property(e => e.Pathicono)
                .HasMaxLength(255)
                .HasColumnName("pathicono");
            entity.Property(e => e.Texto)
                .HasMaxLength(255)
                .HasColumnName("texto");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.HasOne(d => d.AgrupadorNavigation).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.Agrupador)
                .HasConstraintName("opcion_agrupador_fkey");

            entity.HasOne(d => d.MenuNavigation).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.Menu)
                .HasConstraintName("opcion_menu_fkey");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto).HasName("puestos_pkey");

            entity.ToTable("puestos", "softbytecommerce");

            entity.Property(e => e.IdPuesto)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_puesto");
            entity.Property(e => e.Aplicación)
                .HasComment("relación de los datos con su aplicación")
                .HasColumnName("aplicación");
            entity.Property(e => e.CantidadVacantes)
                .HasDefaultValue(1)
                .HasColumnName("cantidad_vacantes");
            entity.Property(e => e.CapacitacionRequerida)
                .HasDefaultValue(false)
                .HasColumnName("capacitacion_requerida");
            entity.Property(e => e.CodigoPuesto)
                .HasMaxLength(50)
                .HasColumnName("codigo_puesto");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.EsPuestoBase)
                .HasDefaultValue(true)
                .HasColumnName("es_puesto_base");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.HerramientasUtilizadas).HasColumnName("herramientas_utilizadas");
            entity.Property(e => e.HorarioTrabajo).HasColumnName("horario_trabajo");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.ModalidadTrabajo)
                .HasMaxLength(50)
                .HasColumnName("modalidad_trabajo");
            entity.Property(e => e.NivelJerarquico)
                .HasMaxLength(50)
                .HasColumnName("nivel_jerarquico");
            entity.Property(e => e.NombrePuesto)
                .HasMaxLength(150)
                .HasColumnName("nombre_puesto");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.RequiereSupervision)
                .HasDefaultValue(false)
                .HasColumnName("requiere_supervision");
            entity.Property(e => e.RequisitosMinimos).HasColumnName("requisitos_minimos");
            entity.Property(e => e.RiesgoLaboral).HasColumnName("riesgo_laboral");
            entity.Property(e => e.SueldoBase)
                .HasPrecision(12, 2)
                .HasColumnName("sueldo_base");
            entity.Property(e => e.TipoPuesto)
                .HasMaxLength(50)
                .HasColumnName("tipo_puesto");
            entity.Property(e => e.UniformeRequerido)
                .HasDefaultValue(false)
                .HasColumnName("uniforme_requerido");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            //entity.HasOne(d => d.AplicaciónNavigation).WithMany(p => p.Puestos)
            //    .HasForeignKey(d => d.Aplicación)
            //    .HasConstraintName("puestos_aplicación_fkey");

            //entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Puestos)
            //    .HasForeignKey(d => d.IdDepartamento)
            //    .HasConstraintName("fk_puesto_departamento");
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

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rol_pkey");

            entity.ToTable("rol", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Aplicacion).HasColumnName("aplicacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.AplicacionNavigation).WithMany(p => p.Rols)
                .HasForeignKey(d => d.Aplicacion)
                .HasConstraintName("rol_aplicacion_fkey");
        });

        modelBuilder.Entity<Rolopcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rolopcion_pkey");

            entity.ToTable("rolopcion", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Opcion).HasColumnName("opcion");
            entity.Property(e => e.Permitido).HasColumnName("permitido");
            entity.Property(e => e.Rol).HasColumnName("rol");

            entity.HasOne(d => d.OpcionNavigation).WithMany(p => p.Rolopcions)
                .HasForeignKey(d => d.Opcion)
                .HasConstraintName("rolopcion_opcion_fkey");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Rolopcions)
                .HasForeignKey(d => d.Rol)
                .HasConstraintName("rolopcion_rol_fkey");
        });

        modelBuilder.Entity<Rolopcionaccion>(entity =>
        {
            entity.HasKey(e => new { e.Rolopcion, e.Accion }).HasName("rolopcionaccion_pkey");

            entity.ToTable("rolopcionaccion", "softbytecommerce");

            entity.Property(e => e.Rolopcion).HasColumnName("rolopcion");
            entity.Property(e => e.Accion).HasColumnName("accion");
            entity.Property(e => e.Permitido).HasColumnName("permitido");

            entity.HasOne(d => d.AccionNavigation).WithMany(p => p.Rolopcionaccions)
                .HasForeignKey(d => d.Accion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolopcionaccion_accion_fkey");

            entity.HasOne(d => d.RolopcionNavigation).WithMany(p => p.Rolopcionaccions)
                .HasForeignKey(d => d.Rolopcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolopcionaccion_rolopcion_fkey");
        });

        modelBuilder.Entity<Rolusuario>(entity =>
        {
            entity.HasKey(e => new { e.Rol, e.Usuario }).HasName("rolusuario_pkey");

            entity.ToTable("rolusuario", "softbytecommerce");

            entity.Property(e => e.Rol).HasColumnName("rol");
            entity.Property(e => e.Usuario).HasColumnName("usuario");
            entity.Property(e => e.Superusuario).HasColumnName("superusuario");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Rolusuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rolusuario_rol_fkey");
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
            entity.Property(e => e.Aplicacion)
                .HasColumnName("aplicacion")
                .HasDefaultValue(1);
            entity.HasOne(u => u.AplicacionNavigation)
                .WithMany()
                .HasForeignKey(u => u.Aplicacion)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Usuarioopcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarioopcion_pkey");

            entity.ToTable("usuarioopcion", "softbytecommerce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Opcion).HasColumnName("opcion");
            entity.Property(e => e.Permitido).HasColumnName("permitido");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.OpcionNavigation).WithMany(p => p.Usuarioopcions)
                .HasForeignKey(d => d.Opcion)
                .HasConstraintName("usuarioopcion_opcion_fkey");
        });

        modelBuilder.Entity<Usuarioopcionaccion>(entity =>
        {
            entity.HasKey(e => new { e.Usuarioopcion, e.Accion }).HasName("usuarioopcionaccion_pkey");

            entity.ToTable("usuarioopcionaccion", "softbytecommerce");

            entity.Property(e => e.Usuarioopcion).HasColumnName("usuarioopcion");
            entity.Property(e => e.Accion).HasColumnName("accion");
            entity.Property(e => e.Permitido).HasColumnName("permitido");

            entity.HasOne(d => d.AccionNavigation).WithMany(p => p.Usuarioopcionaccions)
                .HasForeignKey(d => d.Accion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarioopcionaccion_accion_fkey");

            entity.HasOne(d => d.UsuarioopcionNavigation).WithMany(p => p.Usuarioopcionaccions)
                .HasForeignKey(d => d.Usuarioopcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarioopcionaccion_usuarioopcion_fkey");
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

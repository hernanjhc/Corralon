﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VentasConexión : DbContext
    {
        public VentasConexión()
            : base("name=VentasConexión")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bancos> Bancos { get; set; }
        public virtual DbSet<BancoSucursal> BancoSucursal { get; set; }
        public virtual DbSet<Barrios> Barrios { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<Domicilios> Domicilios { get; set; }
        public virtual DbSet<EArticulos> EArticulos { get; set; }
        public virtual DbSet<EMarcas> EMarcas { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<ERubros> ERubros { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<GruposItemsMenu> GruposItemsMenu { get; set; }
        public virtual DbSet<GruposUsuarios> GruposUsuarios { get; set; }
        public virtual DbSet<ItemsMenu> ItemsMenu { get; set; }
        public virtual DbSet<Localidades> Localidades { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<TiposDocumento> TiposDocumento { get; set; }
        public virtual DbSet<Unidades> Unidades { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosItemsMenu> UsuariosItemsMenu { get; set; }
    }
}

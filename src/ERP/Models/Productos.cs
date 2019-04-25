//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            this.RemitosProductos = new HashSet<RemitosProductos>();
            this.VentasProductos = new HashSet<VentasProductos>();
        }
    
        public int Id { get; set; }
        public string CodBarra { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdMarca { get; set; }
        public Nullable<int> IdUnidad { get; set; }
        public Nullable<int> IdRubro { get; set; }
        public Nullable<int> IdSubRubro { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public Nullable<decimal> CostoInicial { get; set; }
        public Nullable<decimal> CostoAdicional1 { get; set; }
        public Nullable<decimal> CostoAdicional2 { get; set; }
        public decimal Costo { get; set; }
        public Nullable<int> StockMinimo { get; set; }
        public decimal Lista1 { get; set; }
        public Nullable<decimal> Lista2 { get; set; }
        public Nullable<decimal> Lista3 { get; set; }
        public int Estado { get; set; }
    
        public virtual Marcas Marcas { get; set; }
        public virtual Proveedores Proveedores { get; set; }
        public virtual Rubros Rubros { get; set; }
        public virtual SubRubro SubRubro { get; set; }
        public virtual Unidad Unidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemitosProductos> RemitosProductos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentasProductos> VentasProductos { get; set; }
    }
}

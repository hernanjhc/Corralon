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
    
    public partial class Empresas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresas()
        {
            this.EMarcas = new HashSet<EMarcas>();
            this.ERubros = new HashSet<ERubros>();
            this.EPresupuestos = new HashSet<EPresupuestos>();
            this.EPresupuestosDetalles = new HashSet<EPresupuestosDetalles>();
            this.ERemitos = new HashSet<ERemitos>();
            this.ERemitosDetalles = new HashSet<ERemitosDetalles>();
            this.EVentas = new HashSet<EVentas>();
            this.EVentasDetalles = new HashSet<EVentasDetalles>();
            this.EComprasDetalles = new HashSet<EComprasDetalles>();
            this.ECompras = new HashSet<ECompras>();
            this.EArticulos = new HashSet<EArticulos>();
        }
    
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoDoc { get; set; }
        public decimal NroDoc { get; set; }
        public Nullable<int> IdDomicilio { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int Presupuesto { get; set; }
        public int Venta { get; set; }
        public int Remito { get; set; }
        public int NotaDebito { get; set; }
        public int NotaCredito { get; set; }
        public int Compra { get; set; }
    
        public virtual Domicilios Domicilios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMarcas> EMarcas { get; set; }
        public virtual TiposDocumento TiposDocumento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERubros> ERubros { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPresupuestos> EPresupuestos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPresupuestosDetalles> EPresupuestosDetalles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERemitos> ERemitos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERemitosDetalles> ERemitosDetalles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVentas> EVentas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVentasDetalles> EVentasDetalles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EComprasDetalles> EComprasDetalles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ECompras> ECompras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EArticulos> EArticulos { get; set; }
    }
}

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
    
    public partial class Proveedores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proveedores()
        {
            this.ECompras = new HashSet<ECompras>();
            this.EArticulos = new HashSet<EArticulos>();
        }
    
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoDocumento { get; set; }
        public decimal NroDocumento { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string EMail { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> IdDomicilio { get; set; }
        public byte Estado { get; set; }
    
        public virtual Domicilios Domicilios { get; set; }
        public virtual TiposDocumento TiposDocumento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ECompras> ECompras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EArticulos> EArticulos { get; set; }
    }
}

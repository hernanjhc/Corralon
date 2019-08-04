﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Models;

namespace ERP.Repositories
{
    class VentasRepository
    {
        public static IList<EVentas> ObtenerVentas()
        {
            using (var db = new VentasConexión())
            {
                var query = (//from c in db.Clientes
                             from ep in db.EVentas
                                 //join c in db.Clientes on ep.IdCliente equals c.Id
                                 //join td in db.TiposDocumento on c.IdTipoDocumento equals td.Id
                             select ep)
                             .ToList()
                                .Select(
                                    p => new EVentas
                                    {
                                        Id = p.Id,
                                        IdEmpresa = p.IdEmpresa,
                                        IdCliente = p.IdCliente,
                                        //Cliente = p.Clientes.RazonSocial,
                                        Fecha = p.Fecha,                                        
                                        Descuento = p.Descuento,
                                        DescuentoPorc = p.DescuentoPorc,
                                        Importe = p.Importe,
                                        PrecioLista = p.PrecioLista,
                                        IdUsuario = p.IdUsuario,
                                        Estado = p.Estado
                                    });
                return query.OrderBy(p => p.Id).ToList();
            }
        }

        internal static EVentas ObtenerVentaPorId(int id)
        {
            using (var db = new VentasConexión())
            {
                return db.EVentas.Find(id);
            }
        }
    }
}

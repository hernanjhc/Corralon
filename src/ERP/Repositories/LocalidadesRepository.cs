﻿using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    static class LocalidadesRepository
    {
        public static IEnumerable<Localidades> ObtenerLocalidadesPorDepartamento(int idDepartamento)
        {
            using (var db = new Models.VentasConexión())
            {
                var deptos = db.Localidades.Where(l => l.IdDepartamento == idDepartamento).ToList()
                                .Select(
                                    l => new Localidades {
                                                 Id = l.Id,
                                                 Nombre = l.Nombre
                                            }
                                 );
                return deptos.OrderBy(b => b.Nombre).ToList();
            }
        }

        public static Localidades Insertar(int idDepartamento, string nombre)
        {
            using (var db = new VentasConexión())
            {
                if (db.Localidades.Any(l => l.Nombre.ToLower() == nombre.ToLower() &&
                        l.IdDepartamento == idDepartamento))
                {
                    throw new Exception("Ya existe una localidad con este nombre en este departamento.");
                }

                var id = db.Localidades.Any() ? db.Localidades.Max(l => l.Id) + 1 : 1;
                var loc = new Localidades
                {
                    Id = id,
                    IdDepartamento = idDepartamento,
                    Nombre = nombre
                };
                db.Localidades.Add(loc);
                db.SaveChanges();
                return loc;
            }
        }

        internal static Localidades ObtenerLocalidadPorId(int id)
        {
            using (var db = new VentasConexión())
            {
                return db.Localidades.FirstOrDefault(l => l.Id == id);
            }
        }

        internal static void Actualizar(int id, string nombre)
        {
            using (var db = new VentasConexión())
            {
                if (!db.Localidades.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la localidad con Id " + id);
                }
                var loc = db.Localidades.Find(id);
                if (db.Localidades.Any(l => l.Nombre.ToLower() == nombre.ToLower() &&
                        l.IdDepartamento == loc.IdDepartamento))
                {
                    throw new Exception("Ya existe una localidad con este nombre en este departamento.");
                }
                loc.Nombre = nombre;
                db.SaveChanges();
            }
        }

        internal static void Eliminar(int id)
        {
            using (var db = new VentasConexión())
            {
                if (!db.Localidades.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la localidad con Id " + id);
                }
                var l = db.Localidades.Find(id);
                if (l.Barrios.Any())
                {
                    throw new Exception(String.Format("No se puede eliminar la localidad " +
                        "porque tiene {0} barrios relacionados.", l.Barrios.Count));
                }
                if (l.Domicilios.Any())
                {
                    throw new Exception("No se puede eliminar la localidad porque está relacionada a alumnos.");
                }
                db.Localidades.Remove(l);
                db.SaveChanges();
            }
        }
    }

}

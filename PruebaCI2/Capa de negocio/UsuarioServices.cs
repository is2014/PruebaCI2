/**
 * Esta clase representa el acceso a la logica de negocio, la cual contiene las restricciones y acceso a la capa de datos
 * Exactamente con la entidad Usuarios del Entity Framework
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_de_datos;
using PruebaCI2.Persistencia;

namespace Capa_de_negocio
{
    public static class UsuarioServices
    {
        static IOperaciones_Usuarios datos;
        static UsuarioServices()
        {
            datos = new Operaciones_Usuarios();
        }
        public static Usuarios Insertar(Usuarios usuario)
        {
            return datos.InsertarUsuario(usuario);
        }
        public static void Eliminar(decimal codigo)
        {
            datos.EliminarUsuario(codigo);
        }
        public static Usuarios Actualizar(Usuarios usuario)
        {
            return datos.ActualizarUsuario(usuario);
        }
        public static List<Usuarios> ObtenerTodo()
        {
            return datos.ObtenerTodo();
        }
        public static List<Usuarios> ObtenerUsuarios(String consulta)
        {
            return datos.ConsultarUsuarios(consulta);
        }
        public static Usuarios ObtenerUsuario(decimal codigo)
        {
            return datos.ObtenerUsuario(codigo);
        }
        public static Usuarios ObtenerUsuario(string usuario)
        {
            return datos.ObtenerUsuario(usuario);
        }

    }
}
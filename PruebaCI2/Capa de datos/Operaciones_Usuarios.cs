/**
 * Esta clase implementa las operaciones de acceso a la tabla Usuarios de la base de datos
 */

using Capa_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCI2.Persistencia
{
    public class Operaciones_Usuarios : IOperaciones_Usuarios
    {
        public Usuarios ActualizarUsuario(Usuarios usuario)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                acceso.Usuarios.Attach(usuario);
                acceso.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                acceso.SaveChanges();
                Usuarios usuario_bd = acceso.Usuarios.Find(usuario.Codigo_user);
                return usuario_bd;
            }
            
        }

        public List<Usuarios> ConsultarUsuarios(string condicion)
        {
            using (DatosEntities acceso = new DatosEntities())
            {

                List<Usuarios> lista = acceso.Usuarios.SqlQuery("Select * from Usuarios " + condicion).ToList<Usuarios>();
                return lista;

            }
            
        }

        public void EliminarUsuario(decimal codigo)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                Usuarios usuario = acceso.Usuarios.Find(codigo);
                acceso.Usuarios.Attach(usuario);
                acceso.Usuarios.Remove(usuario);
                acceso.SaveChanges();
            }
            
        }

        public Usuarios InsertarUsuario(Usuarios usuario)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                acceso.Usuarios.Add(usuario);
                acceso.SaveChanges();
                Usuarios usuario_bd = acceso.Usuarios.Find(usuario.Codigo_user);
                return usuario_bd;
            }
            
        }

        public List<Usuarios> ObtenerTodo()
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                return acceso.Usuarios.ToList();
            }
            
        }

        public Usuarios ObtenerUsuario(decimal codigo)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                Usuarios usuario = acceso.Usuarios.Find(codigo);
                return usuario;
                
            }
           
        }

        public Usuarios ObtenerUsuario(string usuario)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                Usuarios usuario_bd = acceso.Usuarios.SqlQuery("Select * from Usuarios WHERE Usuario='"+usuario+"' " ).First<Usuarios>();
                return usuario_bd;

            }

        }
    }
}
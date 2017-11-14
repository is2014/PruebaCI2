/**
 * Esta clase implementa las operaciones de acceso a la tabla Tareas de la base de datos
 */

using Capa_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCI2.Persistencia
{
    public class Operaciones_Tareas : IOperaciones_Tareas
    {
        public Tareas ActualizarTarea(Tareas tarea)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                acceso.Tareas.Attach(tarea);
                acceso.Entry(tarea).State = System.Data.Entity.EntityState.Modified;
                acceso.SaveChanges();
                Tareas tarea_bd = acceso.Tareas.Find(tarea.Codigo_tarea);
                return tarea_bd;
            }
          
        }

        public List<Tareas> ConsultarTareas(string condicion)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
              
                List<Tareas> lista = acceso.Tareas.SqlQuery("Select * from Tareas "+ condicion).ToList<Tareas>();
                return lista;
             
            }
          
        }
        public List<Tareas> ObtenerTodo()
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                return acceso.Tareas.ToList();
            }
            
        }

        public void EliminarTarea(decimal codigo)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                Tareas tarea=acceso.Tareas.Find(codigo);
                acceso.Tareas.Attach(tarea);
                acceso.Tareas.Remove(tarea);
                acceso.SaveChanges();
            }
            
        }

        public Tareas InsertarTarea(Tareas tarea)
        {
            using (DatosEntities acceso = new DatosEntities())
            {
                acceso.Tareas.Add(tarea);
                acceso.SaveChanges();
                Tareas tarea_bd = acceso.Tareas.Find(tarea.Codigo_tarea);
                return tarea_bd;
            }
            
        }
        public Tareas ObtenerTarea(decimal codigo) {
            using (DatosEntities acceso = new DatosEntities())
            {
                Tareas tarea=acceso.Tareas.Find(codigo);
                return tarea;
            }
            
        }
    }
}
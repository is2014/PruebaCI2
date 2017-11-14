/**
 * Esta clase representa el acceso a la logica de negocio, la cual contiene las restricciones y acceso a la capa de datos
 * Exactamente con la entidad Tareas del Entity Framework
 */
using PruebaCI2.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capa_de_datos;
namespace Capa_de_negocio
{
    public static class TareaServices
    {
        static IOperaciones_Tareas datos;
        static TareaServices() {
            datos = new Operaciones_Tareas();
        }
        public static TareaResponse Insertar(Tareas tarea,bool sesion_activa)
        {
            if (sesion_activa)  //El usuario debe estar autenticado
            {
                Tareas tarea_db = datos.InsertarTarea(tarea);
                return new TareaResponse { Codigo = tarea.Codigo_tarea, Fecha_creacion = tarea.Fecha_creacion, Descripcion = tarea.Descripcion, Fecha_vencimiento = (DateTime)tarea.Fecha_vencimiento, Estado = tarea.Estado, Autor = tarea.Autor };
            }
            else
            {
                return null;
            }
           
        }
        public static void Eliminar(decimal codigo,bool sesion_activa,decimal usuario)
        { 
            if (sesion_activa)  //El usuario debe estar autenticado
            {
                Tareas tarea = datos.ObtenerTarea(codigo);
                if(tarea!=null)
                {
                    decimal autor = tarea.Autor;
                    if (usuario.Equals(autor))  //Debe ser el mismo usuario que la creó
                    {
                        datos.EliminarTarea(codigo);
                    }
                    else
                    {
                        throw new Exception("No tiene permisos");
                    }
                }
                else
                {
                    throw new Exception("Tarea no existe");
                }
               
            }
            else
            {
                throw new Exception("El usuario no esta autenticado");
            }
            
        }
        public static TareaResponse Actualizar(Tareas tarea,bool sesion_activa,decimal usuario)
        {  
            if (sesion_activa)  //El usuario debe estar autenticado
            {
                Tareas tarea_obj = datos.ObtenerTarea(tarea.Codigo_tarea);
                if(tarea_obj!=null)
                {
                    decimal autor = datos.ObtenerTarea(tarea.Codigo_tarea).Autor;
                    if (usuario.Equals(autor))  //Debe ser el mismo usuario que la creó
                    {
                        Tareas tarea_db = datos.ActualizarTarea(tarea);
                        return new TareaResponse { Codigo = tarea.Codigo_tarea, Fecha_creacion = tarea.Fecha_creacion, Descripcion = tarea.Descripcion, Fecha_vencimiento = (DateTime)tarea.Fecha_vencimiento, Estado = tarea.Estado, Autor = tarea.Autor };
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
            
        }
        public static List<Tareas> ObtenerTodo()
        {
            return datos.ObtenerTodo();
        }
        public static List<TareaResponse> ObtenerTareas(String consulta,bool sesion_activa)
        {  
            if (sesion_activa)   //El usuario debe estar autenticado
            {
                List<TareaResponse> tareas_list = new List<TareaResponse>();
                List<Tareas> tareas_db = datos.ConsultarTareas(consulta);
                foreach (Tareas tarea in tareas_db)
                {
                    tareas_list.Add(new TareaResponse { Codigo = tarea.Codigo_tarea, Fecha_creacion = tarea.Fecha_creacion, Descripcion = tarea.Descripcion, Fecha_vencimiento = (DateTime)tarea.Fecha_vencimiento, Estado = tarea.Estado, Autor = tarea.Autor });
                }
               return tareas_list;
            }
            else
            {
                return null;
            }
            
        }
        public static Usuarios ObtenerTarea_Usuario(decimal codigo )
        {
            Tareas tarea = datos.ObtenerTarea(codigo);
            return tarea.Usuarios;
        }
        public static TareaResponse ObtenerTarea(decimal codigo)
        {
            Tareas tarea= datos.ObtenerTarea(codigo);
            return new TareaResponse { Codigo = tarea.Codigo_tarea, Fecha_creacion = tarea.Fecha_creacion, Descripcion = tarea.Descripcion, Fecha_vencimiento =(DateTime) tarea.Fecha_vencimiento, Estado = tarea.Estado, Autor = tarea.Autor };
        }
    }
}
using Capa_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Capa_de_negocio.Webservices
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public List<TareaResponse> ConsultarTareas(string consulta,bool sesion)
        {
            return TareaServices.ObtenerTareas(consulta,sesion);
        }

        [WebMethod]
        public TareaResponse CrearTarea(DateTime fecha_creacion,string descripcion,string estado,DateTime fecha_vencimiento,decimal autor,bool sesion)
        {
            Tareas tarea = new Tareas();
            tarea.Descripcion = descripcion;
            tarea.Estado = estado;
            tarea.Fecha_creacion = fecha_creacion;
            tarea.Fecha_vencimiento = fecha_vencimiento;
            tarea.Autor = autor;
            return TareaServices.Insertar(tarea,sesion);
            
        }

        [WebMethod]
        public TareaResponse ActualizarTarea(Tareas tarea, bool sesion_activa, decimal usuario)
        {
            return TareaServices.Actualizar(tarea,sesion_activa,usuario);
        }

        [WebMethod]
        public void EliminarTarea(decimal codigo,bool sesion_activa,decimal usuario)
        {
            TareaServices.Eliminar(codigo,sesion_activa,usuario);
        }
    }
}

using PruebaCI2.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_datos
{
    public interface IOperaciones_Tareas
    {
        Tareas InsertarTarea(Tareas tarea);

        void EliminarTarea(decimal codigo);

        Tareas ActualizarTarea(Tareas tarea);

        List<Tareas> ConsultarTareas( string condicion);

        List<Tareas> ObtenerTodo();

        Tareas ObtenerTarea(decimal codigo);
    }
}

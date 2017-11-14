using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_datos
{
    public interface IOperaciones_Usuarios
    {
        Usuarios InsertarUsuario(Usuarios usuario);

        void EliminarUsuario(decimal codigo);

        Usuarios ActualizarUsuario(Usuarios usuario);

        List<Usuarios> ConsultarUsuarios(string condicion);

        List<Usuarios> ObtenerTodo();

        Usuarios ObtenerUsuario(decimal codigo);

        Usuarios ObtenerUsuario(string usuario);
    }
}

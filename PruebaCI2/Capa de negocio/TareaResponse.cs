using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capa_de_negocio
{
    [Serializable]
    public class TareaResponse
    {
        public decimal Codigo { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public DateTime Fecha_vencimiento { get; set; }

        public decimal Autor { get; set; }
    }
}
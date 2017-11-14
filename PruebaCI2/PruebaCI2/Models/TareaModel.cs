/**
 * Esta clase representa el modelo para el controlador TareaController.cs
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaCI2.Models
{
    public class TareaModel
    {

     
        public decimal Codigo { get; set; }

        public DateTime Fecha_creacion { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime Fecha_vencimiento { get; set; }

        public decimal Autor { get; set; }

        public bool Variable_control { get; set; }
    }
}
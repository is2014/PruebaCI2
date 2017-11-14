/**
 * Esta clase representa el modelo para el controlador UsuarioController.cs
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaCI2.Models
{
    public class UsuarioModel
    {
        public decimal Codigo { get; set; }
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre de usuario no puede estar vacio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El usuario no puede estar vacio")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar vacia")]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
 
        [Display(Name = "Recuerdame")]
        public bool Remember { get; set; }
    }
}
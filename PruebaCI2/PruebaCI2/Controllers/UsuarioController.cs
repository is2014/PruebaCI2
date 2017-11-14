/**
 * Esta clase representa el controlador, el cual tiene operaciones para validacion y comunicacion directa con la capa de negocio
 * Exactamente con la clase UsuarioServices.cs
 */

using Capa_de_datos;
using Capa_de_negocio;
using PruebaCI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace PruebaCI2.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IraVista(string page)
        {
            return View(page);
        }
        public ActionResult CerrarSesion()
        {
            Session.Remove("user");
            return View("~/Views/Usuario/Index.cshtml");
        }
        public ActionResult Eliminar(decimal codigo)
        {
            UsuarioServices.Eliminar(codigo);
            return View("~/Views/Tarea/gestionar_usuarios.cshtml");
        }


        [HttpPost]
        public ActionResult Actualizar([Bind(Include = "Codigo, Nombre, Usuario, Contrasena")] UsuarioModel usuariomodel)
        {
            string nombre = "";
            string usuario = "";
            string contrasena = "";
            nombre = usuariomodel.Nombre;
            usuario = usuariomodel.Usuario;
            contrasena = usuariomodel.Contrasena;
            if (nombre == null)
            {
                ModelState.AddModelError("nombre_usr_mensaje", "El nombre de usuario no puede estar vacio");
            }
            if (usuario == null)
            {
                ModelState.AddModelError("usuario_usr_mensaje", "El usuario no puede estar vacio");
            }
            if (contrasena == null)
            {
                ModelState.AddModelError("contrasena_usr_mensaje", "La contraseña no puede estar vacia");
            }

            UsuarioServices.Actualizar(new Usuarios { Codigo_user = usuariomodel.Codigo,
                                                        nombre = usuariomodel.Nombre,
                                                        Usuario = usuariomodel.Usuario,
                                                        Contrasena = usuariomodel.Contrasena});
            TempData["Actualiza_usr"] = "<script>alert('Se ha actualizado con exito');</script>";
            return View("~/Views/Usuario/gestionar_usuarios.cshtml", usuariomodel);


        }

        [HttpPost]
        public ActionResult Crear([Bind(Include = "Nombre, Usuario, Contrasena")] UsuarioModel usuariomodel)
        {
            string sesion_user = Session["user"] as string;
            string nombre = "";
            string usuario = "";
            string contrasena = "";
            nombre = usuariomodel.Nombre;
            usuario = usuariomodel.Usuario;
            contrasena = usuariomodel.Contrasena;
            if (nombre == null)
            {
                ModelState.AddModelError("nombre_usr_mensaje", "El nombre de usuario no puede estar vacio");
            }
            if (usuario == null)
            {
                ModelState.AddModelError("usuario_usr_mensaje", "El usuario no puede estar vacio");
            }
            if (contrasena == null)
            {
                ModelState.AddModelError("contrasena_usr_mensaje", "La contraseña no puede estar vacia");
            }

            UsuarioServices.Insertar(new Usuarios { nombre = nombre, Usuario = usuario , Contrasena = ByteArrayToString(obtenerHash(contrasena)) });
            TempData["mensaje_usr"] = "<script>alert('Se ha registrado con exito');</script>";
            return View("~/Views/Usuario/insertar_usuario.cshtml", usuariomodel);
 

        }
        public ActionResult CargarActualizar(decimal codigo)
        {
            Usuarios usuario_db = UsuarioServices.ObtenerUsuario(codigo);
            UsuarioModel usuario_model = new UsuarioModel
            {
                Codigo = usuario_db.Codigo_user,
                Nombre = usuario_db.nombre,
                Usuario = usuario_db.Usuario,
                Contrasena = usuario_db.Contrasena
            };
            return View("actualizar_tarea", usuario_model);
        }

        [HttpPost]
        public ActionResult Consultar()
        {
            string busqueda = "";
            string filtro = "";
            string consulta = "";
            busqueda = Request["Buscar"];
            filtro = Request["Filtro"];
            if (busqueda != null)
            {
                if (filtro != null)
                {
                    consulta = "WHERE "+filtro+" LIKE '%" + busqueda + "%' ";
                }
                else
                {
                    consulta = "WHERE Usuario LIKE '%"+busqueda+"%' ";
                }
            }
          
            List<Usuarios> usuarios_db = UsuarioServices.ObtenerUsuarios(consulta);
            List<UsuarioModel> usuarios_model = new List<UsuarioModel>();
            foreach(Usuarios usuario in usuarios_db)
            {
                usuarios_model.Add(new UsuarioModel {Codigo = usuario.Codigo_user,
                                                     Usuario = usuario.Usuario,
                                                     Contrasena = usuario.Contrasena,
                                                     Nombre = usuario.nombre
                                                        });
            }

            return View("~/Views/Usuario/gestionar_usuarios.cshtml", usuarios_model);
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Usuario, Contrasena")] UsuarioModel usuariomodel)
        {
            string usuario = "";
            string contrasena = "";
            usuario = usuariomodel.Usuario;
            contrasena = usuariomodel.Contrasena;
            if (usuario == null)
            {
                ModelState.AddModelError("usuario_mensaje", "Usuario no puede estar vacio");
            }
            if (contrasena == null)
            {
                ModelState.AddModelError("contrasena_mensaje", "La contrasena no puede estar vacia");
            }
    
            int cantidad=UsuarioServices.ObtenerUsuarios("WHERE Usuario='"+ usuario + "' AND Contrasena='"+ ByteArrayToString(obtenerHash(contrasena)) + "' ").Count;
            if (cantidad == 0)
            {
                TempData["logueo"] = "<script>alert('Error al loguearse');</script>";
                return View("~/Views/Usuario/Index.cshtml", usuariomodel);
            }
            else
            {
                System.Web.HttpContext.Current.Session["user"] = usuario;
                return View("~/Views/Tarea/Index.cshtml", usuariomodel);
            }
 
        }

        public byte[] obtenerHash(string contrasena)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(contrasena);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return tmpHash;
        }
        public string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

    }
}
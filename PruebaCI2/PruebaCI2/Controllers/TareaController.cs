/**
 * Esta clase representa el controlador, el cual tiene operaciones para validacion y comunicacion directa con la capa de negocio
 * Exactamente con la clase TareaServices.cs
 */

using Capa_de_datos;
using Capa_de_negocio;
using PruebaCI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaCI2.Controllers
{
    public class TareaController : Controller
    {
        // GET: Tarea
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IraVista(string page)
        {
            return View(page);
        }

        public ActionResult Eliminar(decimal codigo)
        {
            string sesion_user = Session["user"] as string;
            if (sesion_user != null)  //El usuario debe estar autenticado
            {
                decimal codigo_user = UsuarioServices.ObtenerUsuario(codigo).Codigo_user;
                decimal autor = TareaServices.ObtenerTarea(codigo).Autor;
                if (codigo_user.Equals(autor))   //Debe ser el mismo usuario que la creó
                {
                    ServiceReference.WebServiceSoapClient servicio = new ServiceReference.WebServiceSoapClient();
                    servicio.EliminarTarea(codigo,true, codigo_user);

                }

            }
            
            return View("~/Views/Tarea/consultar_tareas.cshtml");
        }

        [HttpPost]
        public ActionResult Actualizar([Bind(Include = "Codigo, Descripcion,Estado, Fecha_vencimiento")] TareaModel tareamodel) {

            ServiceReference.Tareas tarea = new ServiceReference.Tareas();
            tarea.Codigo_tarea = tareamodel.Codigo;
            tarea.Descripcion = tareamodel.Descripcion;
            tarea.Estado = tareamodel.Estado;
            try
            {
                string sesion_user = Session["user"] as string;
                if (sesion_user != null)  //El usuario debe estar autenticado
                {
                    decimal codigo_user = UsuarioServices.ObtenerUsuario(tareamodel.Codigo).Codigo_user;
                    decimal autor = TareaServices.ObtenerTarea(tareamodel.Codigo).Autor;
                    if (codigo_user.Equals(autor))   //Debe ser el mismo usuario que la creó
                    {
                        ServiceReference.WebServiceSoapClient servicio = new ServiceReference.WebServiceSoapClient();
                        servicio.ActualizarTarea(tarea,true, codigo_user);
                        TempData["actualiza"] = "<script>alert('Se ha actualizado con exito');</script>";
                    }
                    else
                    {
                        TempData["actualiza"] = "<script>alert('No tiene permisos para realizar esta operacion');</script>";
                    }
                }
                else
                {
                    TempData["actualiza"] = "<script>alert('No existe una sesion activa');</script>";
                }
                    
                return View("~/Views/Tarea/consultar_tareas.cshtml", tareamodel);
            }
            catch(Exception e)
            {
                TempData["actualiza"] = "<script>alert('Error al actualizar');</script>";
                return View("~/Views/Tarea/consultar_tareas.cshtml", tareamodel);
            }
            
  
        }
        public ActionResult CargarActualizar(decimal codigo)
        {

            TareaResponse tarea_db=TareaServices.ObtenerTarea(codigo);
            TareaModel tarea_model = new TareaModel { Codigo = tarea_db.Codigo,
                                                      Descripcion = tarea_db.Descripcion,
                                                      Estado = tarea_db.Estado,
                                                      Fecha_vencimiento = (DateTime)tarea_db.Fecha_vencimiento,
                                                      Fecha_creacion = tarea_db.Fecha_creacion
                                                      };
            return View("actualizar_tarea",tarea_model);
        }

        [HttpPost]
        public ActionResult Crear([Bind(Include = "Descripcion, Estado, Fecha_vencimiento")] TareaModel tareamodel)
        {
            ServiceReference.WebServiceSoapClient servicio = new ServiceReference.WebServiceSoapClient();
            string sesion_user = Session["user"] as string;
            decimal codigo = UsuarioServices.ObtenerUsuario(sesion_user).Codigo_user;
            string descripcion = "";
            string estado = "";
            DateTime fecha_venc = new DateTime();
            descripcion = tareamodel.Descripcion;
            estado = tareamodel.Estado;
            fecha_venc = tareamodel.Fecha_vencimiento;
            try
            {
                if (sesion_user != null)    //El usuario debe estar autenticado
                {
                    servicio.CrearTarea(DateTime.Now, descripcion, estado, fecha_venc, codigo,true);
                    TempData["mensaje"] = "<script>alert('Se ha registrado con exito');</script>";
                }
                else
                {
                    TempData["mensaje"] = "<script>alert('No existe una sesion activa');</script>";
                }
                
                return View("~/Views/Tarea/insertar_tarea.cshtml", tareamodel);
            }
            catch(Exception e)
            {
                TempData["mensaje"] = "<script>alert('Error al registrar la tarea ');</script>";
                return View("~/Views/Tarea/insertar_tarea.cshtml", tareamodel);
            }

            
        }

        [HttpPost]
        public ActionResult Consultar()
        {
            string sesion_user = Session["user"] as string;
            decimal codigo=UsuarioServices.ObtenerUsuario(sesion_user).Codigo_user;
            string consulta1 = "";
            string consulta2 = "";
            string filtro1=Request.Form["Condiciones1"];
            string filtro2 =Request.Form["Condiciones2"];
            string[] filtro1_array=null;
            string[] filtro2_array=null;
            string orden = Request["Ordenar"];
            if (filtro1 != null)
            {
                filtro1_array = filtro1.Split(',');
                for (int i = 0; i < filtro1_array.Length; i++)
                {
                    if (filtro1_array[i].Equals("1") || filtro1_array[i].Equals(1))
                    {
                        consulta1 += "Autor='" + codigo + "' ";
                    }
                    else
                    {
                        consulta1 = "";
                        break;
                    }
                }
            }
            if (filtro2 != null)
            {
                filtro2_array = filtro2.Split(',');
                for (int i = 0; i < filtro2_array.Length; i++)
                {
                    if (filtro2_array[i].Equals("0") || filtro2_array[i].Equals(0))
                    {
                        if (!consulta2.Equals(""))
                        {
                            consulta2 += " OR Estado='SI' ";
                        }
                        else
                        {
                            consulta2 += "WHERE Estado='SI' ";
                        }

                    }
                    else if (filtro2_array[i].Equals("1") || filtro2_array[i].Equals(1))
                    {
                        if (!consulta2.Equals(""))
                        {
                            consulta2 += " OR Estado='NO' ";
                        }
                        else
                        {
                            consulta2 += "WHERE Estado='NO' ";
                        }
                    }
                    else if (filtro2_array[i].Equals("2") || filtro2_array[i].Equals(2))
                    {
                        consulta2 = "";
                        break;
                    }
                }
            }

            if (!consulta1.Equals(""))
            {
                consulta1 = "WHERE " + consulta1;
                consulta2 = "AND (" + consulta2+")";
            }
    
            if (orden.Equals("0") || orden.Equals(0)) {
                consulta2 += " ORDER BY Fecha_vencimiento";
            }
            //ServiceReference.WebServiceSoapClient servicio = new ServiceReference.WebServiceSoapClient();
            List<TareaResponse> tareas = TareaServices.ObtenerTareas(consulta1 + consulta2, true);
            List<TareaModel> tareas_model = new List<TareaModel>();
            try
            {
                if (sesion_user != null)    //El usuario debe estar autenticado
                {
                    foreach (TareaResponse tarea_obj in tareas)
                    {
                        tareas_model.Add(new TareaModel
                        {
                            Codigo = tarea_obj.Codigo,
                            Autor = tarea_obj.Autor,
                            Descripcion = tarea_obj.Descripcion,
                            Fecha_creacion = tarea_obj.Fecha_creacion,
                            Fecha_vencimiento = (DateTime)tarea_obj.Fecha_vencimiento,
                            Estado = tarea_obj.Estado
                        });
                    }
                }
                
                return View("~/Views/Tarea/consultar_tareas.cshtml", tareas_model.ToList());
            }
            catch(Exception e)
            {
                return View("~/Views/Tarea/consultar_tareas.cshtml", tareas_model.ToList());
            }
            

        }
    }
}
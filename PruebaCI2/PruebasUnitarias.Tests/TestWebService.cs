using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PruebasUnitarias
{
    [TestClass]
    public class TestWebService
    {
        [TestMethod]
        public void TestCrearTarea()
        {
            DateTime fecha_actual = DateTime.Now;
            string descripcion = "Ejemplo de descripcion";
            string estado = "SI";
            DateTime fecha_vence = fecha_actual;
            decimal autor = 1;
            bool sesion_activa = true;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            servicio.CrearTarea(fecha_actual, descripcion, estado, fecha_vence, autor, sesion_activa);
        }

        [TestMethod]
        public void TestCrearTareaSinSesion()
        {
            DateTime fecha_actual = DateTime.Now;
            string descripcion = "Ejemplo de descripcion";
            string estado = "SI";
            DateTime fecha_vence = fecha_actual;
            decimal autor = 1;
            bool sesion_activa = false;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            Tests.ServiceReference.TareaResponse tarea = servicio.CrearTarea(fecha_actual, descripcion, estado, fecha_vence, autor, sesion_activa);
            Assert.IsNull(tarea);
        }

        [TestMethod]
        public void TestEliminarTarea()
        {
            decimal cod_tarea = 7;
            decimal cod_user = 1;
            bool sesion_activa = true;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            servicio.EliminarTarea(cod_tarea, sesion_activa, cod_user);
        }
        [TestMethod]
        public void TestEliminarTareaNoExiste()
        {
            decimal cod_tarea = 100;
            decimal cod_user = 1;
            bool sesion_activa = true;
            try
            {

                Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
                servicio.EliminarTarea(cod_tarea, sesion_activa, cod_user);
                Assert.Fail();
            }
            catch (Exception) { }

        }
        [TestMethod]
        public void TestEliminarTareaSinSesion()
        {
            decimal cod_tarea = 8;
            decimal cod_user = 1;
            bool sesion_activa = false;
            try
            {

                Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
                servicio.EliminarTarea(cod_tarea, sesion_activa, cod_user);
                Assert.Fail();
            }
            catch (Exception) { }

        }

        [TestMethod]
        public void TestEliminarSinPermisos()
        {
            decimal cod_tarea = 5;
            decimal cod_user = 2;
            bool sesion_activa = true;
            try
            {
                Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
                servicio.EliminarTarea(cod_tarea, sesion_activa, cod_user);
                Assert.Fail();
            }
            catch (Exception) { }
        }
        [TestMethod]
        public void TestActualizarTarea()
        {
            decimal codigo = 9;
            DateTime fecha_actual = DateTime.Now;
            string descripcion = "Ejemplo de descripcion actualizacion";
            string estado = "SI";
            DateTime fecha_vence = fecha_actual;
            decimal autor = 1;
            bool sesion_activa = true;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            servicio.ActualizarTarea(new Tests.ServiceReference.Tareas { Codigo_tarea = codigo, Fecha_creacion = fecha_actual, Descripcion = descripcion, Estado = estado, Fecha_vencimiento = fecha_vence, Autor = autor }, sesion_activa, autor);
        }
        [TestMethod]
        public void TestActualizarTareaSinPermisos()
        {
            decimal codigo = 9;
            DateTime fecha_actual = DateTime.Now;
            string descripcion = "Ejemplo de descripcion actualizacion";
            string estado = "SI";
            DateTime fecha_vence = fecha_actual;
            decimal autor = 2;
            bool sesion_activa = true;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            Tests.ServiceReference.TareaResponse tarea = servicio.ActualizarTarea(new Tests.ServiceReference.Tareas { Codigo_tarea = codigo, Fecha_creacion = fecha_actual, Descripcion = descripcion, Estado = estado, Fecha_vencimiento = fecha_vence, Autor = autor }, sesion_activa, autor);
            Assert.IsNull(tarea);
        }

        [TestMethod]
        public void TestActualizarSinSesion()
        {
            decimal codigo = 9;
            DateTime fecha_actual = DateTime.Now;
            string descripcion = "Ejemplo de descripcion actualizacion";
            string estado = "SI";
            DateTime fecha_vence = fecha_actual;
            decimal autor = 1;
            bool sesion_activa = false;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            Tests.ServiceReference.TareaResponse tarea = servicio.ActualizarTarea(new Tests.ServiceReference.Tareas { Codigo_tarea = codigo, Fecha_creacion = fecha_actual, Descripcion = descripcion, Estado = estado, Fecha_vencimiento = fecha_vence, Autor = autor }, sesion_activa, autor);
            Assert.IsNull(tarea);
        }

        [TestMethod]
        public void TestConsultarTareas()
        {
            string consulta = "WHERE Estado='SI' ";
            bool sesion_activo = true;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            Tests.ServiceReference.TareaResponse[] tarea = servicio.ConsultarTareas(consulta, sesion_activo);
        }

        [TestMethod]
        public void TestConsultarTareasSinSesion()
        {
            string consulta = "WHERE Estado='SI' ";
            bool sesion_activo = false;
            Tests.ServiceReference.WebServiceSoapClient servicio = new Tests.ServiceReference.WebServiceSoapClient();
            Tests.ServiceReference.TareaResponse[] tarea = servicio.ConsultarTareas(consulta, sesion_activo);
            Assert.IsNull(tarea);
        }
    }
}

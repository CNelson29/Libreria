using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Description;

namespace Libreria.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=CARLOSNT\\SQLEXPRESS;Initial Catalog=DB_ACCESO;Integrated Security=true";

        // GET: Acceso
        public ActionResult login()
        {
            return View();
        }
        public ActionResult registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registro(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if(oUsuario.Contraseña == oUsuario.ConfirmarContraseña)
            {

            }else{
                ViewData["Mensaje"] = "La contraseña no coincide.";
                return View();
            }
            using (SqlConnection cn = new SqlConnection(cadena)) {

                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Contraseña", oUsuario.Contraseña);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("login", "Acceso");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult login(Usuario oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Contraseña", oUsuario.Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                oUsuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                

            }
            if (oUsuario.IdUsuario != 0)
            {
                Session["Usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado.";
                return View();
            }



            
        }


    }
}
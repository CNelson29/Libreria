﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["usuario"] ==  null){

                filterContext.Result = new RedirectResult("~/Acceso/login");

            }
            base.OnActionExecuting(filterContext);
        }
    }
}
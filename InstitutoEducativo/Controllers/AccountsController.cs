using InstitutoEducativo.Data;
using InstitutoEducativo.Models;
using InstitutoEducativo.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Controllers
{
    public class AccountsController : Controller
    {

        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly DbContextInstituto _miContexto;
        
        public AccountsController (
            UserManager<Persona> userManager,
            SignInManager<Persona> signInManager,
            DbContextInstituto miContexto
            )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._miContexto = miContexto;
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            ViewData["CarreraId"] = new SelectList(_miContexto.Carreras, "CarreraId", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroUsuario modelo)
        {
            if (ModelState.IsValid)
            {
                var Alumnos = _miContexto.Alumnos;
                var LegajoMax = 0;

                foreach (Alumno alumno in Alumnos)
                {
                    if(alumno.Legajo != null)
                    {
                        var ParseLegajo = int.Parse(alumno.Legajo);
                        if (ParseLegajo > LegajoMax)
                        {
                            LegajoMax = ParseLegajo;
                        }
                    }
                 
                }

                Persona persona = new Alumno()
                {
                    UserName = modelo.Email,
                    Email = modelo.Email,
                    CarreraId = modelo.CarreraId,
                    Direccion = modelo.Direccion,
                    Telefono = modelo.Telefono,
                    Dni = modelo.Dni,
                    Apellido = modelo.Apellido,
                    Legajo = (LegajoMax + 1).ToString(),
                    FechaAlta = DateTime.Now,
                    Activo = false
                    

                };

                var resultadoRegistracion = await _userManager.CreateAsync(persona, modelo.Contrasena);

                if (resultadoRegistracion.Succeeded)
                {
                    await _signInManager.SignInAsync(persona, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in resultadoRegistracion.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> EmailLibre(string email)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(email);
            

            if (usuarioExistente == null)
            {
              
                return Json(true);
            }
            else
            {
            
                return Json($"El correo {email} ya está en uso.");
            }
            //Utilizo JSON, Jquery Validate method, espera una respuesta de este tipo.
            //Para que esto funcione desde luego, tienen que estar como siempre las librerias de Jquery disponibles.
            //Importante, que estén en el siguiente ORDEN!!!!!
            //jquery.js
            //jquery.validate.js
            //jquery.validate.unobtrisive.js

            //Jquery está en el Layout, y luego las otras dos, están definidas en el archivo _ValidationScriptsPartial.cshtml. 
            //Si incluyen el render de la sección de script esa, estará entonces disponible.
        }


        [HttpGet]
        public IActionResult IniciarSesion(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            if (returnUrl != null)
            {
                ViewBag.Mensaje = "Para acceder al recurso " + returnUrl + ", primero debe Iniciar sesión";

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login modelo)
        {
            string returnUrl = TempData["returnUrl"] as string;

            if (ModelState.IsValid)
            {
                var resultadoInicioSesion = await _signInManager.PasswordSignInAsync(modelo.Email, modelo.Password, modelo.Recordarme, false);

                if (resultadoInicioSesion.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Inicio de sesión inválido.");
            }
            return View(modelo);
        }


    }
}

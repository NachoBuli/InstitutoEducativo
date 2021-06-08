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
        private readonly RoleManager<Rol> _roleManager;
        
        public AccountsController (
            UserManager<Persona> userManager,
            SignInManager<Persona> signInManager,
            DbContextInstituto miContexto,
            RoleManager<Rol> roleManager

            )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._miContexto = miContexto;
            this._roleManager = roleManager;
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
                    Id = new Guid(),
                    UserName = modelo.Email,
                    Email = modelo.Email,
                    CarreraId = modelo.CarreraId,
                    Direccion = modelo.Direccion,
                    Telefono = modelo.Telefono,
                    Dni = modelo.Dni,
                    Apellido = modelo.Apellido,
                    Legajo = "1" + (LegajoMax + 1).ToString(),
                    FechaAlta = DateTime.Now,
                    Activo = false
                    
                };

                var resultadoRegistracion = await _userManager.CreateAsync(persona, modelo.Contrasena);

                if (resultadoRegistracion.Succeeded)
                {
                    Rol rolAlumno = null;
                    var Name = "Alumno";
                    rolAlumno = await _roleManager.FindByNameAsync(Name);

                    if (rolAlumno == null)
                    {
                        rolAlumno = new Rol();
                        rolAlumno.Name = Name;
                        var resuNewRol = await _roleManager.CreateAsync(rolAlumno);
                    }

                    var resuAddToRole = await _userManager.CreateAsync(persona, Name);
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
         
        
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> CrearRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                Rol rol = new Rol();
                rol.Name = "Admin";


                var resu1 = await _roleManager.CreateAsync(rol);

            };

            return RedirectToAction("Roles");
        }


       public async Task<IActionResult> Roles()
       {
            var roles = _roleManager.Roles.ToList();
           return View(roles);
        }

        //public async Task<IActionResult> CrearAdministrador()
        //{
        //    var name = "Admin";
        //    var email = "admin@admin.com";
        //    Persona admin = new Persona()
        //    {
        //        Nombre = name,
        //        Apellido = name,
        //        UserName = email,
        //        Email = email
        //    };

        //    var resuNewAdmin = await _userManager.CreateAsync(admin, "Password1!");

        //    if (resuNewAdmin.Succeeded)
        //    {
        //        Rol rolAdmin = await _roleManager.FindByNameAsync(name);

        //        if (rolAdmin == null)
        //        {
        //            rolAdmin = new Rol();
        //            rolAdmin.Name = name;
        //            var resuNewRol = await _roleManager.CreateAsync(rolAdmin);
        //        }

        //        var resuAddToRole = await _userManager.AddToRoleAsync(admin, name);
        //    }

        //    return RedirectToAction("Roles");
        //}

        public IActionResult AccesoDenegado(string returnurl)
        {

            return View(model: returnurl);
        }

    }
}

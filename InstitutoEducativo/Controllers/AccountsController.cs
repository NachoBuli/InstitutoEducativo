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
                var MatriculaMax = 0;

                foreach (Alumno alumno in Alumnos)
                {
                    
                    if (alumno.NumeroMatricula != 0)
                    {
                        var MatriculaAlumno = alumno.NumeroMatricula;
                        if (MatriculaAlumno > MatriculaMax)
                        {
                            MatriculaMax = MatriculaAlumno;
                        }
                    }
                }

                MatriculaMax = MatriculaMax + 1;

                Persona persona = new Alumno()
                {
                    Id = new Guid(),
                    Nombre = modelo.Nombre,
                    UserName = modelo.Email,
                    Email = modelo.Email,
                    CarreraId = modelo.CarreraId,
                    Direccion = modelo.Direccion,
                    Telefono = modelo.Telefono,
                    Dni = modelo.Dni,
                    Apellido = modelo.Apellido,
                    FechaAlta = DateTime.Now,
                    Activo = false,
                    NumeroMatricula = MatriculaMax
                    
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

                    var resuAddToRole = await _userManager.AddToRoleAsync(persona, Name);
                    await _signInManager.SignInAsync(persona, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
               

                foreach (var error in resultadoRegistracion.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            ViewData["CarreraId"] = new SelectList(_miContexto.Carreras, "CarreraId", "Nombre");
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

        public async Task<IActionResult> LlenarConDatos()
        {

            if (_miContexto.Carreras.FirstOrDefault(c => c.Nombre == "Analisis De Sistemas") == null)
            {
                var contraseña = "Password1";
                var NameA = "Alumno";
                var NameP = "Profesor";
                var NameE = "Empleado";

                Carrera carrera = new Carrera
                {
                    CarreraId = Guid.NewGuid(),
                    Nombre = "Analisis De Sistemas"

                };


                _miContexto.Carreras.Add(carrera);
                _miContexto.SaveChanges();

                Materia materia = new Materia
                {
                    MateriaId = Guid.NewGuid(),
                    Nombre = "Programacion",
                    CodigoMateria = "p-202",
                    Descripcion = "Codigo",
                    CupoMaximo = 2,
                    MateriasCursadas = new List<MateriaCursada>(),
                    Calificaciones = new List<Calificacion>(),
                    CarreraId = carrera.CarreraId,
                    Carrera = carrera
                };

                Materia materia2 = new Materia
                {
                    MateriaId = Guid.NewGuid(),
                    Nombre = "Ingles",
                    CodigoMateria = "i-220",
                    Descripcion = "English",
                    CupoMaximo = 5,
                    MateriasCursadas = new List<MateriaCursada>(),
                    Calificaciones = new List<Calificacion>(),
                    CarreraId = carrera.CarreraId,
                    Carrera = carrera
                };


                Profesor profesor = new Profesor
                {
                    Id = Guid.NewGuid(),
                    Nombre = "profesor",
                    UserName = "profesor@profesor.com",
                    Email = "profesor@profesor.com",
                    Direccion = "Avenida Libertador, Buenos Aires",
                    Telefono = "1122345678",
                    Dni = "12345621",
                    Apellido = "profe",
                    FechaAlta = DateTime.Now,
                    Legajo = "Profesor-12346",
                    CalificacionesRealizadas = new List<Calificacion>(),
                    MateriasCursadasActivas = new List<MateriaCursada>()
                };

               

                Persona alumno = new Alumno
                {
                    Id = Guid.NewGuid(),
                    Nombre = "alumno",
                    UserName = "alumno@alumno.com",
                    Email = "alumno@alumno.com",
                    CarreraId = carrera.CarreraId,
                    Carrera = carrera,
                    Direccion = "Laprida, Buenos Aires",
                    Telefono = "1122345678",
                    Dni = "12335621",
                    Apellido = "Alu",
                    FechaAlta = DateTime.Now,
                    Activo = true,
                    NumeroMatricula = 12345,


                };

                Persona alumno2 = new Alumno
                {
                    Id = Guid.NewGuid(),
                    Nombre = "alumno2",
                    UserName = "alumno2@alumno2.com",
                    Email = "alumno2@alumno2.com",
                    CarreraId = carrera.CarreraId,
                    Carrera = carrera,
                    Direccion = "Callao, Buenos Aires",
                    Telefono = "1122345678",
                    Dni = "12335621",
                    Apellido = "Alu",
                    FechaAlta = DateTime.Now,
                    Activo = true,
                    NumeroMatricula = 12345,


                };

                Persona alumno3 = new Alumno
                {
                    Id = Guid.NewGuid(),
                    Nombre = "alumno3",
                    UserName = "alumno3@alumno3.com",
                    Email = "alumno3@alumno3.com",
                    CarreraId = carrera.CarreraId,
                    Carrera = carrera,
                    Direccion = "Callao, Buenos Aires",
                    Telefono = "1122345678",
                    Dni = "12335621",
                    Apellido = "Alu",
                    FechaAlta = DateTime.Now,
                    Activo = true,
                    NumeroMatricula = 12345,


                };

                Persona empleado = new Empleado
                {
                    Id = Guid.NewGuid(),
                    Nombre = "empleado",
                    UserName = "empleado@empleado.com",
                    Email = "empleado@empleado.com",
                    Direccion = "Aguero, Buenos Aires",
                    Telefono = "1122845678",
                    Dni = "17335621",
                    Apellido = "Empl",
                    FechaAlta = DateTime.Now,
                    Legajo = "Empleado-12345"
                };



                var resultadoRegistracionA = await _userManager.CreateAsync(alumno, contraseña);
                var resultadoRegistracionA2 = await _userManager.CreateAsync(alumno2, contraseña);
                var resultadoRegistracionA3 = await _userManager.CreateAsync(alumno3, contraseña);
                var resultadoRegistracionP = await _userManager.CreateAsync(profesor, contraseña);
                var resultadoRegistracionE = await _userManager.CreateAsync(empleado, contraseña);

                var resuAddToRoleA = await _userManager.AddToRoleAsync(alumno, NameA);
                var resuAddToRoleA2 = await _userManager.CreateAsync(alumno2, NameA);
                var resuAddToRoleA3 = await _userManager.CreateAsync(alumno3, NameA);
                var resuAddToRoleP = await _userManager.AddToRoleAsync(profesor, NameP);
                var resuAddToRoleE = await _userManager.AddToRoleAsync(empleado, NameE);

                MateriaCursada materiaCursada = new MateriaCursada
                {
                    MateriaCursadaId = Guid.NewGuid(),
                    Nombre = "Programacion-grupo1",
                    Anio = 1,
                    Cuatrimestre = 2,
                    Activo = true,
                    MateriaId = materia.MateriaId,
                    Materia = materia,
                    ProfesorId = profesor.Id,
                    Profesor = profesor,
                    AlumnoMateriaCursadas = new List<AlumnoMateriaCursada>(),
                    Calificaciones = new List<Calificacion>()
                };

                MateriaCursada materiaCursada2 = new MateriaCursada
                {
                    MateriaCursadaId = Guid.NewGuid(),
                    Nombre = "Programacion-grupo2",
                    Anio = 1,
                    Cuatrimestre = 2,
                    Activo = true,
                    MateriaId = materia.MateriaId,
                    Materia = materia,
                    ProfesorId = profesor.Id,
                    Profesor = profesor,
                    AlumnoMateriaCursadas = new List<AlumnoMateriaCursada>(),
                    Calificaciones = new List<Calificacion>()
                };

                MateriaCursada materiaCursada3 = new MateriaCursada
                {
                    MateriaCursadaId = Guid.NewGuid(),
                    Nombre = "Ingles-grupo1",
                    Anio = 1,
                    Cuatrimestre = 2,
                    Activo = true,
                    MateriaId = materia2.MateriaId,
                    Materia = materia2,
                    ProfesorId = profesor.Id,
                    Profesor = profesor,
                    AlumnoMateriaCursadas = new List<AlumnoMateriaCursada>(),
                    Calificaciones = new List<Calificacion>()
                };
                _miContexto.Materias.Add(materia);
                _miContexto.Materias.Add(materia2);
                _miContexto.MateriaCursadas.Add(materiaCursada);
                _miContexto.MateriaCursadas.Add(materiaCursada2);
                _miContexto.MateriaCursadas.Add(materiaCursada3);
                _miContexto.SaveChanges();


                ViewData["Mensaje"] = "La base de datos tiene datos, puede continuar";

                return View("IniciarSesion");

            }

            ViewData["Err"] = "No puede Llenar la base dos veces";
            return View("IniciarSesion");



        }

    }
}

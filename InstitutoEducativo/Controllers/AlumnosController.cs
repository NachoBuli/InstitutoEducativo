﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstitutoEducativo.Data;
using InstitutoEducativo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace InstitutoEducativo.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly DbContextInstituto _context;
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _roleManager;      

        public AlumnosController(DbContextInstituto context, UserManager<Persona> userManager, SignInManager<Persona> signInManager, RoleManager<Rol> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            var dbContextInstituto = _context.Alumnos.Include(a => a.Carrera);
            return View(await dbContextInstituto.ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.Carrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        //[Authorize (Roles = "Empleado")]
        public IActionResult Create()
        {
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre");
            return View();
        }

        //POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       //[Authorize(Roles ="Empleado")]
        public async Task<IActionResult> Create([Bind("Activo,NumeroMatricula,FechaAlta,CarreraId,Nombre,Apellido,Dni,Telefono,Direccion,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var Alumnos = _context.Alumnos;
                var MatriculaMax = 0;

                alumno.Activo = true;
                alumno.Id = Guid.NewGuid();
                alumno.FechaAlta = DateTime.Now;
                alumno.UserName = alumno.Email;

                foreach (Alumno a in Alumnos)
                {
                    if (a.NumeroMatricula != 0)
                    {
                        var MatriculaAlumno = a.NumeroMatricula;
                        if (MatriculaAlumno > MatriculaMax)
                        {
                            MatriculaMax = MatriculaAlumno;
                        }
                    }
                }

                var resultado = await _userManager.CreateAsync(alumno, alumno.PasswordHash);
                if (resultado.Succeeded)
                {
                    Rol rolAlumno = null;
                    var name = "Alumno";
                    rolAlumno = await _roleManager.FindByNameAsync(name);

                    if (rolAlumno == null)
                    {
                        rolAlumno = new Rol();
                        rolAlumno.Name = name;
                        var resultNewRol = await _roleManager.CreateAsync(rolAlumno);
                    }

                    var resultAddToRol = await _userManager.AddToRoleAsync(alumno, name);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre", alumno.CarreraId);
            return View(alumno);
        }

        //GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre", alumno.CarreraId);
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Activo,NumeroMatricula,CarreraId,FechaAlta,Nombre,Apellido,Dni,Telefono,Direccion,Legajo,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre", alumno.CarreraId);
            return View(alumno);
        }

        // GET: Alumnos/Delete/5
        [Authorize (Roles = "Alumno")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.Carrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(Guid id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }

        //[Authorize(Roles = "Alumno")]
        public async Task<IActionResult> RegistrarMaterias()
        {
            //var alumno = _userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
           
            Alumno alumno = (Alumno)await _userManager.GetUserAsync(HttpContext.User);
            if (alumno.Activo == true)
            {
                var carreraId = alumno.CarreraId;
                var carrera = await _context.Carreras.Include(carrera => carrera.Materias)
                    .FirstOrDefaultAsync(c => carreraId == c.CarreraId);
                var materias = carrera.Materias;
                return View(materias);
            }else
            {
                return RedirectToAction("AccesoDenegado", "Accounts");
            }
            

            
        }
        //[HttpPost, ActionName("AgregarMateria")]
        public async Task<IActionResult> AgregarMateriaMostrar (Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
           
            return View(materia);
        }

        public async Task<IActionResult> AgregarMateria(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = _context.Materias.Include(mc => mc.MateriasCursadas)
                .ThenInclude(ma => ma.AlumnoMateriaCursadas)
                .ThenInclude(m => m.MateriaCursada)
                .FirstOrDefault(m => m.MateriaId == id);

            if (materia == null)
            {
                return NotFound();
            }
            var alumnoid = Guid.Parse(_userManager.GetUserId(User));
            var alumno = _context.Alumnos.Include(a => a.AlumnosMateriasCursadas)
                .ThenInclude(am => am.MateriaCursada)
                .ThenInclude(mc => mc.Materia)
                .FirstOrDefault(a => a.Id == alumnoid);

            if(alumno != null)
            {
                if (alumno.AlumnosMateriasCursadas.Count >= 5)
                {
                    TempData["message"] = "No podes inscribirte en mas de 5 materias";
                    return RedirectToAction("RegistrarMaterias", "Alumnos");
                }
                else
                {
                    bool encontrado = false;
                    foreach (AlumnoMateriaCursada alumnomateriaCursada in alumno.AlumnosMateriasCursadas)
                    {
                        if (alumnomateriaCursada.MateriaCursada.Materia == materia)
                        {
                            encontrado = true;
                        }
                    }
                    if (encontrado == true)
                    {
                        TempData["message"] = "Ya te inscribiste para esta materia";
                        return RedirectToAction("RegistrarMaterias");
                    }
                    else
                    {
                        if(materia.MateriasCursadas.Count == 0)
                        {
                            TempData["message"] = "Disculpa pero, todavia no hay grupos disponibles. Intentá inscribirte en otro momento";
                            return RedirectToAction("RegistrarMaterias");
                        }
                        Guid calificacionId = Guid.NewGuid();

                        AlumnoMateriaCursada amc = new AlumnoMateriaCursada
                        {

                            Alumno = alumno,
                            AlumnoId = alumno.Id,
                            MateriaCursada = checkMateriaCursadaLibre(materia),
                            MateriaCursadaId = checkMateriaCursadaLibre(materia).MateriaCursadaId,
                            CalificacionId = calificacionId

                        };

                        Calificacion calificacion = new Calificacion
                        {
                            AlumnoMateriaCursada = amc,
                            CalificacionId = calificacionId,
                            Profesor = amc.MateriaCursada.Profesor,
                            ProfesorId = amc.MateriaCursada.ProfesorId,
                            Materia = materia,
                            NotaFinal = -1111
                        };
                        MateriaCursada materiaCursada = _context.MateriaCursadas
                            .Include(mc => mc.Calificaciones)
                            .FirstOrDefault(mc => mc.MateriaCursadaId == amc.MateriaCursadaId);


                        materiaCursada.Calificaciones.Add(calificacion);
                        _context.AlumnoMateriaCursadas.Add(amc);
                        alumno.AlumnosMateriasCursadas.Add(amc);
                        _context.Calificaciones.Add(calificacion);
                        _context.Alumnos.Update(alumno);
                        _context.SaveChanges();
                    }

                }

                }
            else
            {
                return NotFound();
            }



            TempData["message"] = "Te inscribiste con exito";
            return RedirectToAction("RegistrarMaterias");
        }

        private MateriaCursada checkMateriaCursadaLibre(Materia materia)
        {
            int cupoMax = materia.CupoMaximo;
            MateriaCursada materiaCursadaLibre = null;
           
            foreach (MateriaCursada mc in materia.MateriasCursadas)
            {
                if (mc.AlumnoMateriaCursadas.Count < cupoMax|| mc.AlumnoMateriaCursadas == null)
                {
                    materiaCursadaLibre = mc;
                }
            }
            if (materiaCursadaLibre == null)
            {
                
                var firstMateriaCursada = materia.MateriasCursadas.First();
                materiaCursadaLibre = new MateriaCursada
                {
                    MateriaCursadaId = Guid.NewGuid(),
                    MateriaId = materia.MateriaId,
                    Anio = firstMateriaCursada.Anio,
                    Cuatrimestre = firstMateriaCursada.Cuatrimestre,
                    Activo = false,
                    Materia = firstMateriaCursada.Materia,
                    ProfesorId = firstMateriaCursada.ProfesorId,
                    Nombre = materia.Nombre+firstMateriaCursada.Anio.ToString()+ materia.MateriasCursadas.Count.ToString()

                    };
                _context.MateriaCursadas.Add(materiaCursadaLibre);
                _context.SaveChanges();
                }
            return materiaCursadaLibre;
            }


        public async Task<IActionResult> VerMateriasCursadasAlumno()
        {

            var alumnoid = Guid.Parse(_userManager.GetUserId(User));
            var alumno = _context.Alumnos.Include(a => a.AlumnosMateriasCursadas).ThenInclude(am=>am.Calificacion).ThenInclude(c=>c.Materia).FirstOrDefault(a => a.Id == alumnoid);


            if (alumno.AlumnosMateriasCursadas == null)
            {
                return NotFound();
            }
            var alumnosMateriasCursadas = new List<AlumnoMateriaCursada>();
            foreach(AlumnoMateriaCursada am in alumno.AlumnosMateriasCursadas)
            {
                if (!(am.Calificacion.NotaFinal==-1111)) {
                    alumnosMateriasCursadas.Add(am);
                    
                
                }
            }

            return View(alumnosMateriasCursadas);
        }
    }
                


   
     
    }


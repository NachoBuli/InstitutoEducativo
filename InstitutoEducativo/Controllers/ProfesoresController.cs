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
using InstitutoEducativo.ViewModels;

namespace InstitutoEducativo.Controllers
{
    public class ProfesoresController : Controller
    {
        private readonly DbContextInstituto _context;
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _rolManager;

        public ProfesoresController(DbContextInstituto context, UserManager<Persona> userManager, SignInManager<Persona> signInManager, RoleManager<Rol> rolManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _rolManager = rolManager;

        }

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesores.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        public async Task<IActionResult> ListarMateriasCursadas()
        {
            Profesor profesor = (Profesor)await _userManager.GetUserAsync(HttpContext.User);
            List <MateriaCursadaConNotaPromedio> listaMateriasActivasPorProfesor = new List <MateriaCursadaConNotaPromedio>();
            List<int> promedios = new List<int>();
            var materiaCursadas = _context.MateriaCursadas
                .Include(mc => mc.Calificaciones);
          
          

            if (materiaCursadas == null)
            {
                ViewData["Message"] = "No hay materias cursadas";
                return View();
            }
            else

            {
                foreach (MateriaCursada mc in materiaCursadas)
                {
                    if (mc.ProfesorId == profesor.Id && mc.Activo)
                    {
                        MateriaCursadaConNotaPromedio mcp = new MateriaCursadaConNotaPromedio
                        {
                            materiaCursada = mc
                        };
                           
                        listaMateriasActivasPorProfesor.Add(mcp);
                        
                    }
                }
            }
            return View(listaMateriasActivasPorProfesor);
        }



        public async Task<IActionResult> MostrarAlumnosPorMateriaCursada(Guid? id) // esta bien
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaCursada = _context.MateriaCursadas
                .Include(mc => mc.AlumnoMateriaCursadas)
                .ThenInclude(amc => amc.Alumno)
                .Include(mc => mc.AlumnoMateriaCursadas)
                .ThenInclude(amc => amc.Calificacion)
                .FirstOrDefault(mc => mc.MateriaCursadaId == id);

            
            return View(materiaCursada.AlumnoMateriaCursadas);
        }


        // GET: Profesores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =("Empleado"))]
        public async Task<IActionResult> Create([Bind("FechaAlta,Nombre,Apellido,Dni,Telefono,Direccion,Legajo,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                profesor.Id = Guid.NewGuid();
                profesor.FechaAlta = DateTime.Today;
                profesor.UserName = profesor.Email;

                var resultado = await _userManager.CreateAsync(profesor, profesor.PasswordHash);
                if (resultado.Succeeded)
                {
                    Rol rolProfesor = null;
                    var name = "Profesor";
                    rolProfesor = await _rolManager.FindByNameAsync(name);

                    if (rolProfesor == null)
                    {
                        rolProfesor = new Rol();
                        rolProfesor.Name = name;
                        var resultNewRol = await _rolManager.CreateAsync(rolProfesor);
                    }

                    var resultAddToRol = await _userManager.AddToRoleAsync(profesor, name);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View(profesor);
        }
    

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            return View(profesor);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Empleado")]
        public async Task<IActionResult> Edit(Guid id, [Bind("FechaAlta,Nombre,Apellido,Dni,Telefono,Direccion,Legajo,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            return View(profesor);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Empleado")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(Guid id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }

        public async Task<IActionResult> MostrarProfesores()
        {
            
            return View(_context.Profesores);
        }


        //        //Dejo lo que seria la logica en general, pero de esta manera no funciona correctamente
        public async Task<IActionResult> NotaPromedioMateriaCursada()
        {
            Profesor profesor = (Profesor)await _userManager.GetUserAsync(HttpContext.User);
            var materiaCursadas = _context.MateriaCursadas
                .Include(mc => mc.AlumnoMateriaCursadas)
                .ThenInclude(amc => amc.Alumno)
                .FirstOrDefault(m => m.ProfesorId == profesor.Id);
            if (materiaCursadas == null)
            {
                return View();
            }

            //var materiaCursada = _context.MateriaCursadas.ToList();
            var materiaCursada = profesor.MateriasCursadasActivas;
            int calificaciones = 0;
            Double promedio = 0;
            int alumnosPorCursada;

            foreach (var mCursada in materiaCursada)
            {
                alumnosPorCursada = mCursada.AlumnoMateriaCursadas.Count;

                foreach (var amc in mCursada.AlumnoMateriaCursadas)
                {
                    calificaciones = +amc.Calificacion.NotaFinal;
                }

                promedio = calificaciones / alumnosPorCursada;
            }

            promedio = Math.Round(promedio, 2, MidpointRounding.AwayFromZero);

            return View("ListarMateriasCursadas", promedio);
        }
    }
}

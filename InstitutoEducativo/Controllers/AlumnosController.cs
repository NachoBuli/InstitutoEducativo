using System;
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
        public async Task<IActionResult> RegistrarMaterias(Guid? id)
        {
            //var alumno = _userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Alumno alumno = (Alumno)await _userManager.GetUserAsync(HttpContext.User);
            var carreraId = alumno.CarreraId;
            var carrera = await _context.Carreras.FindAsync(carreraId);
            var materias = carrera.Materias.Count();

            return View(materias);
        }
    }
}

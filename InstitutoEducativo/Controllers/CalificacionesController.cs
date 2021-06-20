using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstitutoEducativo.Data;
using InstitutoEducativo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace InstitutoEducativo.Controllers
{
    public class CalificacionesController : Controller
    {
        private readonly DbContextInstituto _context;
        private readonly UserManager<Persona> _userManager;

        public CalificacionesController(DbContextInstituto context, UserManager<Persona> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            var dbContextInstituto = _context.Calificaciones.Include(c => c.Profesor);
            return View(await dbContextInstituto.ToListAsync());
        }
        [Authorize(Roles = "Alumno")]

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.CalificacionId == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Calificaciones/Create
        public IActionResult Create()
        {
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido");
            return View();
        }

        // POST: Calificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalificacionId,NotaFinal,ProfesorId")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                calificacion.CalificacionId = Guid.NewGuid();
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", calificacion.ProfesorId);
            return View(calificacion);
        }
        //authorize
        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(Guid? CalificacionId) //validaciones
        {
            Calificacion calificacion = _context.Calificaciones.FirstOrDefault(c => c.CalificacionId == CalificacionId);

            return View(calificacion);
        }

        // POST: Calificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NotaFinal")] Calificacion calificacion)
        {
            Profesor profesor = (Profesor)await _userManager.GetUserAsync(HttpContext.User);
            if (id != calificacion.CalificacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.CalificacionId))
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
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", calificacion.ProfesorId);
            return View(calificacion);
        }

        // GET: Calificaciones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.CalificacionId == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(Guid id)
        {
            return _context.Calificaciones.Any(e => e.CalificacionId == id);
        }
    }
}

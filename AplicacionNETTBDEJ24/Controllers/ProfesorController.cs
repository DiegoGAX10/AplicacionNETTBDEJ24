using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacionNETTBDEJ24.Context;
using AplicacionNETTBDEJ24.Models;

namespace AplicacionNETTBDEJ24.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profesor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesor.ToListAsync());
        }

        // GET: Profesor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor
                .FirstOrDefaultAsync(m => m.nuprof == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nuprof,nombre,sueldo,grado")] Profesor profesor)
        {
            // Verificar si el nuprof ya existe
            if (ProfesorExists(profesor.nuprof))
            {
                ModelState.AddModelError("nuprof", "Este número de profesor ya está registrado. Por favor, ingrese otro número.");
                return View(profesor);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(profesor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key value violates unique constraint"))
                    {
                        ModelState.AddModelError("", "Ya existe un registro con este número de profesor.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error de base de datos: " + ex.Message);
                    }
                }
            }
            return View(profesor);
        }

        // GET: Profesor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            return View(profesor);
        }

        // POST: Profesor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nuprof,nombre,sueldo,grado")] Profesor profesor)
        {
            if (id != profesor.nuprof)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el profesor original
                    var originalProfesor = await _context.Profesor.AsNoTracking().FirstOrDefaultAsync(p => p.nuprof == id);

                    if (originalProfesor == null)
                    {
                        return NotFound();
                    }

                    // Actualizar el profesor
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();

                    // Si se cambió el nuprof, actualizar la tabla Materias
                    if (originalProfesor.nuprof != profesor.nuprof)
                    {
                        var materiaEntries = _context.Materias.Where(m => m.nuprof == originalProfesor.nuprof);

                        foreach (var entry in materiaEntries)
                        {
                            entry.nuprof = profesor.nuprof;
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.nuprof))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key value violates unique constraint"))
                    {
                        ModelState.AddModelError("", "Ya existe un registro con este número de profesor.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error de base de datos: " + ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(profesor);
        }

        // GET: Profesor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesor
                .FirstOrDefaultAsync(m => m.nuprof == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.Profesor.FindAsync(id);

            // Verificar si el profesor está relacionado con alguna materia
            bool isRelatedToMateria = await _context.Materias.AnyAsync(m => m.nuprof == id);

            if (isRelatedToMateria)
            {
                // Si está relacionado, arrojar un mensaje de error
                ModelState.AddModelError(string.Empty, "No se puede eliminar al profesor porque está relacionado con una o más materias.");
                return View(profesor); // Retornar a la vista de eliminación con el mensaje de error
            }

            if (profesor != null)
            {
                _context.Profesor.Remove(profesor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesor.Any(e => e.nuprof == id);
        }
    }
}


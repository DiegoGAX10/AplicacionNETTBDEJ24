using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionNETTBDEJ24.Context;
using AplicacionNETTBDEJ24.Models;

namespace AplicacionNETTBDEJ24.Controllers
{
    public class AlumnoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlumnoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alumnoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alumno.ToListAsync());
        }

        // GET: Alumnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno
                .FirstOrDefaultAsync(m => m.nualu == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumnoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nualu,nombre,edad,sem,espe")] Alumno alumno)
        {
            // Verificar si el nualu ya existe
            if (AlumnoExists(alumno.nualu))
            {
                ModelState.AddModelError("nualu", "Este número de alumno ya está registrado. Por favor, ingrese otro número.");
                return View(alumno);
            }

            if (ModelState.IsValid)
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Alumnoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nualu,nombre,edad,sem,espe")] Alumno alumno)
        {
            if (id != alumno.nualu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el alumno original
                    var originalAlumno = await _context.Alumno.AsNoTracking().FirstOrDefaultAsync(a => a.nualu == id);

                    if (originalAlumno == null)
                    {
                        return NotFound();
                    }

                    // Actualizar el alumno
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();

                    // Si se cambió el nualu, actualizar la tabla Kardex
                    if (originalAlumno.nualu != alumno.nualu)
                    {
                        var kardexEntries = _context.Kardex.Where(k => k.nualu == originalAlumno.nualu);

                        foreach (var entry in kardexEntries)
                        {
                            entry.nualu = alumno.nualu;
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.nualu))
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
            return View(alumno);
        }

        // GET: Alumnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno
                .FirstOrDefaultAsync(m => m.nualu == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumno.FindAsync(id);

            // Verificar si el alumno está relacionado con algún kardex
            bool isRelatedToKardex = await _context.Kardex.AnyAsync(k => k.nualu == id);

            if (isRelatedToKardex)
            {
                // Si está relacionado, arrojar un mensaje de error
                ModelState.AddModelError(string.Empty, "No se puede eliminar al alumno porque está registrado en el Kardex.");
                return View(alumno); // Retornar a la vista de eliminación con el mensaje de error
            }

            if (alumno != null)
            {
                _context.Alumno.Remove(alumno);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumno.Any(e => e.nualu == id);
        }
    }
}

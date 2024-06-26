﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacionNETTBDEJ24.Context;
using AplicacionNETTBDEJ24.Models;

namespace AplicacionNETTBDEJ24.Controllers
{
    public class MateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MateriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Materias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materias.ToListAsync());
        }

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materias = await _context.Materias
                .FirstOrDefaultAsync(m => m.numat == id);
            if (materias == null)
            {
                return NotFound();
            }

            return View(materias);
        }

        // GET: Materias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("numat,nombre,credi,nuprof")] Materias materias)
        {
            // Verificar si el numat ya existe
            if (MateriasExists(materias.numat))
            {
                ModelState.AddModelError("numat", "Este número de materia ya está registrado. Por favor, ingrese otro número.");
                return View(materias);
            }

            // Verificar si el profesor especificado existe
            var profesorExiste = await _context.Profesor.AnyAsync(p => p.nuprof == materias.nuprof);
            if (!profesorExiste)
            {
                ModelState.AddModelError("nuprof", "El profesor especificado no existe.");
                return View(materias);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(materias);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Registra los detalles de la excepción
                    Console.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", "No se pueden guardar los cambios. Intente nuevamente, y si el problema persiste, contacte al administrador del sistema.");
                }
            }
            else
            {
                // Registra los errores del estado del modelo
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Clave: {error.Key}, Error: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            return View(materias);
        }

        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materias = await _context.Materias.FindAsync(id);
            if (materias == null)
            {
                return NotFound();
            }
            return View(materias);
        }

        // POST: Materias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("numat,nombre,credi,nuprof")] Materias materias)
        {
            if (id != materias.numat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriasExists(materias.numat))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "No se pueden guardar los cambios. Intente nuevamente, y si el problema persiste, contacte al administrador del sistema.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(materias);
        }


        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materias = await _context.Materias
                .FirstOrDefaultAsync(m => m.numat == id);
            if (materias == null)
            {
                return NotFound();
            }

            return View(materias);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materias = await _context.Materias.FindAsync(id);
            if (materias != null)
            {
                _context.Materias.Remove(materias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriasExists(int id)
        {
            return _context.Materias.Any(e => e.numat == id);
        }
    }
}

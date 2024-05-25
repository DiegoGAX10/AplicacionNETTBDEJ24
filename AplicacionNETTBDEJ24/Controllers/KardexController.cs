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
    public class KardexController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KardexController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kardex
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kardex.ToListAsync());
        }

        // GET: Kardex/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kardex = await _context.Kardex
                .FirstOrDefaultAsync(m => m.nualu == id);
            if (kardex == null)
            {
                return NotFound();
            }

            return View(kardex);
        }

        // GET: Kardex/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kardex/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nualu,numat,calif")] Kardex kardex)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(kardex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Error al guardar los datos: {"Revise bien sus datos"}");
                    return View(kardex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kardex);
        }

        // GET: Kardex/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kardex = await _context.Kardex.FindAsync(id);
            if (kardex == null)
            {
                return NotFound();
            }
            return View(kardex);
        }

        // POST: Kardex/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nualu,numat,calif")] Kardex kardex)
        {
            if (id != kardex.nualu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kardex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KardexExists(kardex.nualu))
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
                    ModelState.AddModelError("", $"Error al guardar los datos: {"Numero de Alumno no existe"}");
                    return View(kardex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kardex);
        }

        // GET: Kardex/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kardex = await _context.Kardex
                .FirstOrDefaultAsync(m => m.nualu == id);
            if (kardex == null)
            {
                return NotFound();
            }

            return View(kardex);
        }

        // POST: Kardex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kardex = await _context.Kardex.FindAsync(id);
            if (kardex != null)
            {
                _context.Kardex.Remove(kardex);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Kardex/ConsultarCalificacion
        public async Task<IActionResult> ConsultarCalificacion()
        {
            var kardexList = await _context.Kardex.ToListAsync();
            return View(kardexList);
        }

        private bool KardexExists(int id)
        {
            return _context.Kardex.Any(e => e.nualu == id);
        }
    }
}

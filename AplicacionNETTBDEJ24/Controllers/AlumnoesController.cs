using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacionNETTBDEJ24.Models;
using AplicacionNETTBDEJ24.Context;

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
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(alumno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key value violates unique constraint"))
                    {
                        ModelState.AddModelError("", "El número de alumno ya existe.");
                    }
                    else if (ex.InnerException != null && ex.InnerException.Message.Contains("La edad del alumno debe estar entre 18 y 65 años."))
                    {
                        ModelState.AddModelError("", "La edad del alumno debe estar entre 18 y 65 años.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error de base de datos: " + ex.Message);
                    }
                }
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
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key value violates unique constraint"))
                    {
                        ModelState.AddModelError("", "El número de alumno ya existe.");
                    }
                    else if (ex.InnerException != null && ex.InnerException.Message.Contains("La edad del alumno debe estar entre 18 y 65 años."))
                    {
                        ModelState.AddModelError("", "La edad del alumno debe estar entre 18 y 65 años.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error de base de datos: " + ex.Message);
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
            if (alumno != null)
            {
                _context.Alumno.Remove(alumno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumno.Any(e => e.nualu == id);
        }
    }
}

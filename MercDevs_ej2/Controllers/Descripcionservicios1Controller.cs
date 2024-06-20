using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MercDevs_ej2.Models;

namespace MercDevs_ej2.Controllers
{
    public class Descripcionservicios1Controller : Controller
    {
        private readonly MercydevsEjercicio2Context _context;

        public Descripcionservicios1Controller(MercydevsEjercicio2Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> DetalleEquipoRecepcionado(int? idServicio)
        {
            if (idServicio == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.Recepcionequipos)  // Incluir la información del equipo recepcionado
                .FirstOrDefaultAsync(s => s.IdServicio == idServicio);

            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);  // Mostrar la vista con los detalles del equipo recepcionado en el servicio
        }

        // GET: Descripcionservicios1
        public async Task<IActionResult> Index()
        {
            var mercydevsEjercicio2Context = _context.Descripcionservicios.Include(d => d.ServicioIdServicioNavigation);
            return View(await mercydevsEjercicio2Context.ToListAsync());
        }

        // GET: Descripcionservicios1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicios = await _context.Descripcionservicios
                .Include(d => d.ServicioIdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDescServ == id);
            if (descripcionservicios == null)
            {
                return NotFound();
            }

            return View(descripcionservicios);
        }

        // GET: Descripcionservicios1/Create
        public IActionResult Create()
        {
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio");
            return View();
        }

        // POST: Descripcionservicios1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDescServ,Nombre,ServicioIdServicio")] Descripcionservicios descripcionservicios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descripcionservicios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicios.ServicioIdServicio);
            return View(descripcionservicios);
        }

        // GET: Descripcionservicios1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicios = await _context.Descripcionservicios.FindAsync(id);
            if (descripcionservicios == null)
            {
                return NotFound();
            }
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicios.ServicioIdServicio);
            return View(descripcionservicios);
        }

        // POST: Descripcionservicios1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDescServ,Nombre,ServicioIdServicio")] Descripcionservicios descripcionservicios)
        {
            if (id != descripcionservicios.IdDescServ)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descripcionservicios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescripcionserviciosExists(descripcionservicios.IdDescServ))
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
            ViewData["ServicioIdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio", descripcionservicios.ServicioIdServicio);
            return View(descripcionservicios);
        }

        // GET: Descripcionservicios1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionservicios = await _context.Descripcionservicios
                .Include(d => d.ServicioIdServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdDescServ == id);
            if (descripcionservicios == null)
            {
                return NotFound();
            }

            return View(descripcionservicios);
        }

        // POST: Descripcionservicios1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descripcionservicios = await _context.Descripcionservicios.FindAsync(id);
            if (descripcionservicios != null)
            {
                _context.Descripcionservicios.Remove(descripcionservicios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescripcionserviciosExists(int id)
        {
            return _context.Descripcionservicios.Any(e => e.IdDescServ == id);
        }
    }
}

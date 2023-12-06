using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyperApplication.Models;
using MyperApplication.TransferObject.Model;

namespace MyperApplication.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;
        private readonly IMapper _mapper;

        public TrabajadoresController(TrabajadoresPruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
            var lstTrabajadores = await _context.Trabajadores.Include(t => t.IdDepartamentoNavigation).Include(t => t.IdDistritoNavigation).Include(t => t.IdProvinciaNavigation).ToListAsync();
            var trabajadoresViewModel = _mapper.Map<List<Trabajadores>>(lstTrabajadores);
            return View(trabajadoresViewModel);
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewData["lstDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewBag.lstProvincia = _context.Provincia.Select(p => new { Id = p.Id, Nombre = p.NombreProvincia, IdDepartamento = p.IdDepartamento }).ToList();
            ViewBag.lstDistrito = _context.Distritos.Select(d => new { Id = d.Id, Nombre = d.NombreDistrito, IdProvincia = d.IdProvincia }).ToList();
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["lstDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewBag.lstProvincia = _context.Provincia.Select(p => new { Id = p.Id, Nombre = p.NombreProvincia, IdDepartamento = p.IdDepartamento }).ToList();
            ViewBag.lstDistrito = _context.Distritos.Select(d => new { Id = d.Id, Nombre = d.NombreDistrito, IdProvincia = d.IdProvincia }).ToList();
            return View(trabajadore);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }
            ViewData["lstDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewBag.lstProvincia = _context.Provincia.Select(p => new { Id = p.Id, Nombre = p.NombreProvincia, IdDepartamento = p.IdDepartamento }).ToList();
            ViewBag.lstDistrito = _context.Distritos.Select(d => new { Id = d.Id, Nombre = d.NombreDistrito, IdProvincia = d.IdProvincia }).ToList();
            return View(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadoreExists(trabajadore.Id))
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
            ViewData["lstDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewBag.lstProvincia = _context.Provincia.Select(p => new { Id = p.Id, Nombre = p.NombreProvincia, IdDepartamento = p.IdDepartamento }).ToList();
            ViewBag.lstDistrito = _context.Distritos.Select(d => new { Id = d.Id, Nombre = d.NombreDistrito, IdProvincia = d.IdProvincia }).ToList();
            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Trabajadores'  is null.");
            }
            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore != null)
            {
                _context.Trabajadores.Remove(trabajadore);
            }
            
            await _context.SaveChangesAsync();
            return Ok(StatusCodes.Status200OK);
        }

        private bool TrabajadoreExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: Trabajadores/ListTrabajadores
        [HttpPost]
        public async Task<JsonResult> Index([FromBody] string sexo)
        {
            var lstTrabajadores = await _context.Trabajadores
                .Where(s => s.Sexo == sexo)
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .ToListAsync();

            var trabajadoresViewModel = _mapper.Map<List<Trabajadores>>(lstTrabajadores);

            return new JsonResult(trabajadoresViewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Microsoft.AspNetCore.Authorization;

namespace Diplom.Views
{
    public class ApplicationBuhController : Controller
    {
        private readonly Context _context;

        public ApplicationBuhController(Context context)
        {
            _context = context;
        }

        // GET: ApplicationBuh
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationBuh.ToListAsync());
        }

        // GET: ApplicationBuh/Details/5
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ApplicationBuh = await _context.ApplicationBuh
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ApplicationBuh == null)
            {
                return NotFound();
            }

            return View(ApplicationBuh);
        }

        // GET: ApplicationBuh/Create
        [Authorize(Roles = "user, admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationBuh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NAME,FILLING_TYPE,TYPE_OF_DESIGN,TYPE_OF_DECORATION,PLACE_OF_MANUFACTURE,CREATE_DATE,END_DATE")] ApplicationBuh ApplicationBuh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ApplicationBuh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ApplicationBuh);
        }

        // GET: ApplicationBuh/Edit/5
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ApplicationBuh = await _context.ApplicationBuh.FindAsync(id);
            if (ApplicationBuh == null)
            {
                return NotFound();
            }
            return View(ApplicationBuh);
        }

        // POST: ApplicationBuh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NAME,FILLING_TYPE,TYPE_OF_DESIGN,TYPE_OF_DECORATION,PLACE_OF_MANUFACTURE,CREATE_DATE,END_DATE")] ApplicationBuh ApplicationBuh)
        {
            if (id != ApplicationBuh.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ApplicationBuh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationBuhExists(ApplicationBuh.ID))
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
            return View(ApplicationBuh);
        }

        // GET: ApplicationBuh/Delete/5
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ApplicationBuh = await _context.ApplicationBuh
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ApplicationBuh == null)
            {
                return NotFound();
            }

            return View(ApplicationBuh);
        }

        // POST: ApplicationBuh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ApplicationBuh = await _context.ApplicationBuh.FindAsync(id);
            _context.ApplicationBuh.Remove(ApplicationBuh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationBuhExists(int id)
        {
            return _context.ApplicationBuh.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;

namespace Diplom.Views
{
    public class ApplicationUrController : Controller
    {
        private readonly Context _context;

        public ApplicationUrController(Context context)
        {
            _context = context;
        }

        // GET: ApplicationUr
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUr.ToListAsync());
        }

        // GET: ApplicationUr/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ApplicationUr = await _context.ApplicationUr
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ApplicationUr == null)
            {
                return NotFound();
            }

            return View(ApplicationUr);
        }

        // GET: ApplicationUr/Create
        [Authorize(Roles = "user, admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUr/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PHYSICAL_LAW,NAME_ORG,ACTIVITY,QUESTION,TIME")] ApplicationUr ApplicationUr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ApplicationUr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ApplicationUr);
        }

        // GET: ApplicationUr/Edit/5
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ApplicationUr = await _context.ApplicationUr.FindAsync(id);
            if (ApplicationUr == null)
            {
                return NotFound();
            }
            return View(ApplicationUr);
        }

        // POST: ApplicationUr/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PHYSICAL_LAW,NAME_ORG,ACTIVITY,QUESTION,TIME")] ApplicationUr ApplicationUr)
        {
            if (id != ApplicationUr.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ApplicationUr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUrExists(ApplicationUr.ID))
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
            return View(ApplicationUr);
        }

        // GET: ApplicationUr/Delete/5
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ApplicationUr = await _context.ApplicationUr
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ApplicationUr == null)
            {
                return NotFound();
            }

            return View(ApplicationUr);
        }

        // POST: ApplicationUr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ApplicationUr = await _context.ApplicationUr.FindAsync(id);
            _context.ApplicationUr.Remove(ApplicationUr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUrExists(int id)
        {
            return _context.ApplicationUr.Any(e => e.ID == id);
        }
    }
}

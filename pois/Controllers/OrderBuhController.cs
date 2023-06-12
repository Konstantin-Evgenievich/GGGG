using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Microsoft.AspNetCore.Authorization;

namespace Diplom.Controllers
{
    public class OrderBuhController : Controller
    {
        private readonly Context _context;

        public OrderBuhController(Context context)
        {
            _context = context;
        }

        // GET: OrderBuh
        public async Task<IActionResult> Index()
        {
            var context = _context.OrderBuh.Include(o => o.Cake).Include(o => o.User);
            return View(await context.ToListAsync());
        }

        // GET: OrderBuh/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.OrderBuh
                .Include(o => o.Cake)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrderBuh/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["CakeID"] = new SelectList(_context.ApplicationBuh, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: OrderBuh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin, user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PRICE,CakeID,UserID,RECEIVING_ADDRESS,status,test")] OrderBuh order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CakeID"] = new SelectList(_context.ApplicationBuh, "ID", "ID", order.CakeID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", order.UserID);
            return View(order);
        }

        // GET: OrderBuh/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.OrderBuh.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CakeID"] = new SelectList(_context.ApplicationBuh, "ID", "ID", order.CakeID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", order.UserID);
            return View(order);
        }

        // POST: OrderBuh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PRICE,CakeID,UserID,RECEIVING_ADDRESS,status")] OrderBuh order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderBuhExists(order.ID))
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
            ViewData["CakeID"] = new SelectList(_context.ApplicationBuh, "ID", "ID", order.CakeID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", order.UserID);
            return View(order);
        }

        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Mine()
        {
            var user = _context.Users.Where(p => p.EMAIL == User.Identity.Name).FirstOrDefault();
            var context = _context.OrderBuh
                .Include(t => t.Cake)
                .Include(t => t.User)
                .Where(p => p.UserID == user.ID);
                //.Where(p => p.Status.STATUS == "забронирован" || p.TicketStatus.STATUS == "куплен");

            return View(await context.ToListAsync());
        }

        // GET: OrderBuh/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.OrderBuh
                .Include(o => o.Cake)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrderBuh/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.OrderBuh.FindAsync(id);
            _context.OrderBuh.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderBuhExists(int id)
        {
            return _context.OrderBuh.Any(e => e.ID == id);
        }
    }
}

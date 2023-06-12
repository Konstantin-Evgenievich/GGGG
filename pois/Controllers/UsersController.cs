using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Diplom.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var context = _context.Users.Include(u => u.Role);
            return View(await context.ToListAsync());
        }

        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Me()
        {
            var user = await _context.Users.Include(u => u.Role)
                .Where(p => p.EMAIL == User.Identity.Name).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> EditMe()
        {
            var user = await _context.Users.Include(u => u.Role)
                .Where(p => p.EMAIL == User.Identity.Name).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMe(int id, [Bind("ID,NAME,MIDDLENAME,SURNAME,DATEOFBIRTH,PHONE,EMAIL,PASSPORT,PASSWORD,RoleId")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UPDATE_DATE = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NAME,MIDDLENAME,SURNAME,DATEOFBIRTH,PHONE,EMAIL,PASSPORT,PASSWORD,RoleId,CREATE_DATE,UPDATE_DATE")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NAME,MIDDLENAME,SURNAME,DATEOFBIRTH,PHONE,EMAIL,PASSPORT,PASSWORD,RoleId,CREATE_DATE,UPDATE_DATE")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Ban(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.RoleId == 1)//нельзя забанить админа
            {
                return NotFound();
            }

            else
            {
                user.RoleId = 3;//banned
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
           

            return Redirect("~/Users/Index");
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}

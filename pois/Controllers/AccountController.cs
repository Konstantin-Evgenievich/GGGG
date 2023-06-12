using System.Diagnostics;
using System.Linq;
using Diplom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Diplom.Controllers
{
    [Route("auth")]
    public class AccountController : Controller
    {
        private Context _context;
        public AccountController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.EMAIL == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User
                    {
                        EMAIL = model.Email,
                        PASSWORD = model.Password,
                        NAME = model.NAME,
                        MIDDLENAME = model.MIDDLENAME,
                        SURNAME = model.SURNAME,
                        DATEOFBIRTH = model.DATEOFBIRTH,
                        PHONE = model.PHONE,
                        PASSPORT = model.PASSPORT,
                        CREATE_DATE = System.DateTime.Now,
                        UPDATE_DATE = System.DateTime.Now
                    };
                    UserRole userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные email и(или) пароль");
            }
            else
                return View(model);
            return View(model);
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.EMAIL == model.Email && u.PASSWORD == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.EMAIL),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
                };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EMAIL == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NAME,SURNAME,MIDDLENAME,PHONE,EMAIL,DATEOFBIRTH,PASSPORT")] User user)
        {

            if (id != user.ID)
            {
                return NotFound();
            }

            else
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return View(user);
        }

    }
}

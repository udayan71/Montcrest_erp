using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Montcrest.Web.ViewModels;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Montcrest.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("MontcrestAPI");

            var payload = new
            {
                fullName = model.FullName,
                email = model.Email,
                password = model.Password,
                mobileNumber = model.MobileNumber,
                address = model.Address
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Registration failed. Email may already exist.";
                return View(model);
            }

            TempData["Success"] = "Registration successful. Please login.";
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync("MontcrestCookie");
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl)
        {
            var client = _httpClientFactory.CreateClient("MontcrestAPI");

            var payload = new { email, password };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var token = doc.RootElement.GetProperty("token").GetString();

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Login failed. Token missing.";
                return View();
            }

            var role = Helpers.JwtTokenHelper.GetRoleFromToken(token);
            var userId = Helpers.JwtTokenHelper.GetUserIdFromToken(token);
            var userEmail = Helpers.JwtTokenHelper.GetEmailFromToken(token);

            // Store session values
            HttpContext.Session.SetString("JWT", token);
            HttpContext.Session.SetString("Role", role);
            HttpContext.Session.SetString("UserId", userId);
            HttpContext.Session.SetString("Email", userEmail);

            // Claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Email, userEmail),
        new Claim(ClaimTypes.Role, role)
    };

            var identity = new ClaimsIdentity(claims, "MontcrestCookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MontcrestCookie", principal);

            // ReturnUrl handling
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // Role based redirect
            return role switch
            {
                "HR" => RedirectToAction("Technologies", "Hr"),
                "Employee" => RedirectToAction("Dashboard", "Employee"),
                "Manager" => RedirectToAction("Index","Manager"),
                _ => RedirectToAction("Index", "Candidate")
            };
        }

    }
}

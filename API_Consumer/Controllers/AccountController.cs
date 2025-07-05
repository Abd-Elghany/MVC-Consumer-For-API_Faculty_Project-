using API_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace API_Consumer.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api"); ;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return View(registerDTO);

            var response = await _httpClient.PostAsJsonAsync("Account/Register", registerDTO);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            // ❌ Handle API validation errors
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, errorContent);

            return View(registerDTO);
        }

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            var response = await _httpClient.PostAsJsonAsync("Account/Login", loginDTO);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResultDTO>();

                // ✅ Store JWT in cookie or session
                HttpContext.Session.SetString("JWToken", result.Token);

                return RedirectToAction("Index", "Home");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, errorContent);

            return View(loginDTO);
        }






    }
}

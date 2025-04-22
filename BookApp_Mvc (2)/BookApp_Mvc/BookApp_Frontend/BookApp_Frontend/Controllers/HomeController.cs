using BookApp_Mvc.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BookApp_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
          
            var httpClient = httpClientFactory.CreateClient(); // Use the default client name
            httpClient.BaseAddress = new Uri("https://localhost:7281/"); // Set the base address

            var response = await httpClient.PostAsJsonAsync("https://localhost:7281/api/Auth/register", userRegister);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(string Email,string Password)
        //{
        //    var httpClient = httpClientFactory.CreateClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:7281/"); // Set the base address

        //    var response = await httpClient.PostAsJsonAsync("https://localhost:7281/api/Auth/Login", Email, Password);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        RedirectToAction("Index", "Book");
        //    }
        //    return View();

        //}
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7281/"); // Set the base address

            var response = await httpClient.PostAsJsonAsync("api/Auth/Login", userLogin);

            if (response.IsSuccessStatusCode)
            {
                
                    var identity = new ClaimsIdentity(
                        new[]
                        {
                        new Claim(ClaimTypes.Name, userLogin.Email),
                        new Claim(ClaimTypes.Role, "User")
                        }, CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                   // var s = HttpContext.Request.Query["returnUrl"];
                    return RedirectToAction("Index", "Home");
                

                //// If login is successful, you might want to handle authentication and redirection here.
                //// For now, assume it's successful and redirect to the Index action of the Book controller.
                //var content = await response.Content.ReadAsStringAsync();
                //return RedirectToAction("Index", "Home", userLogin.Email);
            }

            // If login fails, return the view with validation errors or an error message.
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home"); }




            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

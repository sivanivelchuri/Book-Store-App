using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BookApp_Mvc.Models;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;


public class BookController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BookController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> SearchBooks(string title)
    {
        var httpClient = _httpClientFactory.CreateClient();


        // Make a GET request to the Open Library API
        var response = await httpClient.GetFromJsonAsync<OpenLibraryResponse>($"https://openlibrary.org/search.json?title={title}");
        return View("SearchBooks", response?.Docs);

    }

    // Pass the list of books to the view



    [HttpGet]
    public async Task<IActionResult> AddToWishlist(string title, int coverid)
    {
        if (User.Identity.IsAuthenticated)
        {
            Book book = new Book { Title = title, cover_i = coverid };

            // Create a list and add the book to it
            List<Book> books = new List<Book> { book };
            UserFavorites favorites = new UserFavorites { cover_i = coverid, title = title, Email = User.Identity.Name };

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7222/"); // Set the base address

            var response = await httpClient.PostAsJsonAsync("/api/BookCRUD", favorites);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        else
        {
            // User is not authenticated, indicate a redirect to the login page
            return RedirectToAction("Login", "Home");

        }
    }
    [HttpGet]
    public async Task<IActionResult> MyWishlist()
    {
        var httpClient = _httpClientFactory.CreateClient();
        string email = User.Identity.Name;

        // Make a GET request to the Open Library API
        var response = await httpClient.GetAsync($"https://localhost:7222/api/BookCRUD?Email={email}");
        var content = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(content);
        if (books != null)
        {
            return View(books);
        }
        return null!;

       
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int coverid)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("https://localhost:7222/"); // Set the base address

        var response = await httpClient.DeleteAsync($"https://localhost:7222/api/BookCRUD?c={coverid}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MyWishlist", "Book");
        }
        return View();
    }


}
    
    


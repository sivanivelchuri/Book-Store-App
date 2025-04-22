using BookCRUDApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace BookCRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCRUDController : ControllerBase
    {
        
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DataContext db;
        public BookCRUDController(IHttpClientFactory httpClientFactory, DataContext db)
        {
            _httpClientFactory = httpClientFactory;
            this.db = db;
        }

        [HttpGet("SearchBooks")]
        public async Task<IActionResult> SearchBooks(string title)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Make a GET request to the Open Library API
            var response = await httpClient.GetFromJsonAsync<OpenLibraryResponse>($"https://openlibrary.org/search.json?title={title}");

            // Pass the list of books to the view
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> AddtoWishlist(UserFavorites uf)
        {
            if (uf.Email == null)
            {
                return Ok("Please Login");
            }
            else
            {
                Book obj=new Book();
                obj.Title = uf.title;
               obj.coverid = uf.cover_i;
               db.Books.Add(obj);
               db.UserFavorites.Add(uf);
                
               db.SaveChanges();
                return Ok();

            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int c)
        {
            UserFavorites uf = db.UserFavorites.Where(x => x.cover_i == c).FirstOrDefault();
            db.UserFavorites.Remove(uf);
            db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListFavorites(string Email)
        {
            List<UserFavorites> ufList = await db.UserFavorites.Where(x => x.Email == Email).ToListAsync();

            if (ufList == null)
            {
                
                return NotFound();
            }

            return Ok(ufList);

            
        }

    }
}

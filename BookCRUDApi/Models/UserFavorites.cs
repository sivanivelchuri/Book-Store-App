using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCRUDApi.Models
{
    public class UserFavorites
    {
        [Key]
        public int Id { get; set; }

        public string Email {  get; set; }
        public string title {  get; set; }
        public int cover_i {  get; set; }
        

    }
}

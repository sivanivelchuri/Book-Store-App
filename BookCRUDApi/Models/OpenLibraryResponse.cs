using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class OpenLibraryResponse
{
    [JsonPropertyName("docs")]
    public List<Book> Docs { get; set; }
}

public class Book

{
    [Key]
    public int Id { get; set; } 

    [JsonPropertyName("title")]
    public string Title { get; set; }


    
    [JsonPropertyName("cover_i")]
    public int coverid { get; set; }


}

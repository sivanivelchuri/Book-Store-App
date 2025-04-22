using System.Text.Json.Serialization;

public class OpenLibraryResponse
{
    [JsonPropertyName("docs")]
    public List<Book> Docs { get; set; }
}

public class Book
{
    [JsonPropertyName("title")]
    public string Title { get; set; }


    [JsonPropertyName("cover_i")]
    public int cover_i { get; set; }

   
}

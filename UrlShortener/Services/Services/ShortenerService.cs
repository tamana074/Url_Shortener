using System.Text;
using System.Text.RegularExpressions;
using DataAccess.Entities;

namespace Services.Services;

public class ShortenerService
{

    public bool ValidationUrl(string longUrl)
    {
        var validateDateRegex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
        return validateDateRegex.IsMatch(longUrl);
    }
    public Urls GenerateShortUrl(string longUrl)
    {
        var uri = new Uri(longUrl);
        var baseUrl = uri.GetLeftPart(UriPartial.Authority);
        var urlCode = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
      
        
        var shortUrl = new StringBuilder();
        shortUrl.Append(baseUrl);
        shortUrl.Append("/");
        shortUrl.Append(urlCode);

        var url = new Urls
        {
            Id = new Guid(),
            OriginalUrl = longUrl,
            ShortUrl = shortUrl.ToString(),
            ShortCode = urlCode,
            CreateDate = DateTime.Now
        };
        return url;
    }
}
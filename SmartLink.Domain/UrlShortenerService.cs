namespace SmartLink.Domain;

public class UrlShortenerService
{
    // Проста база даних у пам'яті для Junior-проєкту
    private readonly Dictionary<string, string> _urlMapping = new();

    public string CreateShortCode(string longUrl)
    {
        if (string.IsNullOrWhiteSpace(longUrl) || !Uri.IsWellFormedUriString(longUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid URL provided.");
        }

        // Генеруємо випадковий код (SonarQube може вказати на Guid як на не найнадійніший метод, але для початку ок)
        var shortCode = Guid.NewGuid().ToString().Substring(0, 6);
        _urlMapping[shortCode] = longUrl;
        
        return shortCode;
    }

    public string? GetLongUrl(string shortCode)
    {
        return _urlMapping.TryGetValue(shortCode, out var longUrl) ? longUrl : null;
    }
}
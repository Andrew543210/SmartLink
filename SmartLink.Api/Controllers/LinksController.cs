using Microsoft.AspNetCore.Mvc;
namespace SmartLink.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    public static readonly UrlShortenerService _service = new();
    
    [HttpPost]
    public IActionResult Shorten([FromBody] string url)
    {
        try 
        {
            var code = _service.CreateShortCode(url);
            return Ok(new { ShortCode = code, ShortUrl = $"{Request.Scheme}://{Request.Host}/{code}" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("/{code}")]
    public IActionResult RedirectTo(string code)
    {
        // Викликаємо метод нашого сервісу, який ми написали раніше
        var longUrl = _service.GetLongUrl(code);

        // Перевіряємо, чи знайшлося щось у нашому Dictionary
        if (longUrl == null)
        {
            // Якщо коду не існує — повертаємо 404
            return NotFound();
        }

        // Якщо знайшли — робимо перенаправлення
        return Redirect(longUrl);
    }
}
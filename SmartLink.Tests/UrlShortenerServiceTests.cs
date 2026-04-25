using SmartLink.Domain;
using Xunit;

namespace SmartLink.Tests;

public class UrlShortenerServiceTests
{
    private readonly UrlShortenerService _service = new();

    [Fact]
    public void CreateShortCode_ShouldReturnSixCharacterCode()
    {
        // Arrange
        var longUrl = "https://github.com/Andrew543210";

        // Act
        var code = _service.CreateShortCode(longUrl);

        // Assert
        Assert.NotNull(code);
        Assert.Equal(6, code.Length);
    }

    [Fact]
    public void GetLongUrl_ShouldReturnOriginalUrl_WhenCodeExists()
    {
        // Arrange
        var longUrl = "https://google.com";
        var code = _service.CreateShortCode(longUrl);

        // Act
        var result = _service.GetLongUrl(code);

        // Assert
        Assert.Equal(longUrl, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-url")]
    [InlineData(null)]
    public void CreateShortCode_ShouldThrowException_WhenUrlIsInvalid(string invalidUrl)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CreateShortCode(invalidUrl));
    }
}
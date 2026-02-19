using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AspNetCoreMvc.IntegrationTests;

public class ApplicationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApplicationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Application_Starts_Successfully()
    {
        // Act
        Action act = () => _factory.CreateClient();
        
        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Application_CanCreateClient()
    {
        // Act
        var client = _factory.CreateClient();

        // Assert
        client.Should().NotBeNull();
        client.BaseAddress.Should().NotBeNull();
    }

    [Fact]
    public async Task Application_HomePage_ReturnsContent()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        content.Should().NotBeNullOrEmpty();
    }
}

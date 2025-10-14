using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class StudentWBFTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;


    public StudentWBFTest()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_HomePage_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        var html = await response.Content.ReadAsStringAsync();
        Assert.Contains("Contoso University", html);
    }

    [Fact]
    public async Task Get_StudentsPage_ReturnsSeededStudent()
    {
        var response = await _client.GetAsync("/Students");
        response.EnsureSuccessStatusCode();

        var html = await response.Content.ReadAsStringAsync();
        Console.WriteLine(html);
        Assert.Contains("Smith", html);
    }
}

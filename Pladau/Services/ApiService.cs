
using Pladau.Models;
using System.Text.Json;

namespace Pladau.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string APIURI = "https://localhost:7032/api/";
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Admin>> GetAdminAsync()
    {
        var response = await _httpClient.GetAsync($"{APIURI}admin");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var admins = JsonSerializer.Deserialize<List<Admin>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return admins;
    }
}

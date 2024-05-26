
using Pladau.Models;
using System.Text.Json;

namespace Pladau.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string APIURI = "https://localhost:7032/api/";
    private JsonSerializerOptions jsonOptions;
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        jsonOptions = new JsonSerializerOptions();
        jsonOptions.PropertyNameCaseInsensitive = true;
    }

    public async Task<List<Admin>> GetAdminAsync()
    {
        var response = await _httpClient.GetAsync($"{APIURI}admin");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var admins = JsonSerializer.Deserialize<List<Admin>>(json, jsonOptions);

        return admins;
    }
    public async Task<List<University>> GetAllUniversitiesAsync()
    {
        var response = await _httpClient.GetAsync($"{APIURI}university");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var universities = JsonSerializer.Deserialize<List<University>>(json, jsonOptions);

        return universities;
    }
}

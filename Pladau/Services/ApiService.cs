
using MongoDB.Bson;
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
    public async Task<List<Faculty>> GetFacultiesByIdUni(string id)
    {
        var response = await _httpClient.GetAsync($"{APIURI}university/universities/{id}/faculties");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var faculties = JsonSerializer.Deserialize<List<Faculty>>(json, jsonOptions);

        return faculties;
    }

    public async Task<List<Carrer>> GetCarrerByIdFaculty(string id)
    {
        var response = await _httpClient.GetAsync($"{APIURI}faculty/faculties/{id}/carrers");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var carrers = JsonSerializer.Deserialize<List<Carrer>>(json, jsonOptions);

        return carrers;
    }

    public async Task<List<Subject>> GetSubjectsByIdCarrers(string id)
    {
        var response = await _httpClient.GetAsync($"{APIURI}carrer/carrers/{id}/subjects");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var subjects = JsonSerializer.Deserialize<List<Subject>>(json, jsonOptions);

        return subjects;
    }
}

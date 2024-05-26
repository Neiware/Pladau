using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PladauAPI.Data;
using PladauAPI.Models;

namespace PladauAPI.Services
{
    public class MongoDBService
    {
        private readonly MongoDbContext _context;
        public MongoDBService(MongoDbContext context)
        {
            _context = context;            
        }

        #region General 
        public async Task<List<T>> GetAllAsync<T>(string collectionName)
        {
            var collection = _context.GetCollection<T>(collectionName);
            return await collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(string collectionName, string id)
        {
            var collection = _context.GetCollection<T>(collectionName);
            return await collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync<T>(string collectionName, T document)
        {
            var collection = _context.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }

        public async Task UpdateAsync<T>(string collectionName, string id, T document)
        {
            var collection = _context.GetCollection<T>(collectionName);
            await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), document);
        }

        public async Task DeleteAsync<T>(string collectionName, string id)
        {
            var collection = _context.GetCollection<T>(collectionName);
            await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public async Task UpdateAsync<T>(string collectionName, string id, UpdateDefinition<T> updateDefinition)
        {
            var collection = _context.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.UpdateOneAsync(filter, updateDefinition);
        }

        public async Task ReplaceAsync<T>(string collectionName, string id, T updatedDocument)
        {
            var collection = _context.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.ReplaceOneAsync(filter, updatedDocument);
        }

        public async Task<List<Faculty>> GetFacultiesByIdUni(string id)
        {
            University university = await GetByIdAsync<University>("universities", id);
            if (university == null)
            {
                // Manejar el caso en que la universidad no exista
                return new List<Faculty>(); // O lanzar una excepción, dependiendo de la lógica de tu aplicación
            }

            // Extraer los IDs de las facultades del arreglo en el documento de la universidad
            var facultyIds = university.FacultyIds;

            // Usar los IDs de las facultades para buscar las facultades correspondientes en la colección de facultades
            var faculties = await _context.GetAsync<Faculty>("faculties", f => facultyIds.Contains(f.Id));

            return faculties;
        }

        public async Task<List<Carrer>> GetCarrersByIdFaculty(string id)
        {
            Faculty faculty = await GetByIdAsync<Faculty>("faculties", id);
            if (faculty == null)
            {
                // Manejar el caso en que la universidad no exista
                return new List<Carrer>(); // O lanzar una excepción, dependiendo de la lógica de tu aplicación
            }

            // Extraer los IDs de las facultades del arreglo en el documento de la universidad
            var carrerIds = faculty.CarrerIds;

            // Usar los IDs de las facultades para buscar las facultades correspondientes en la colección de facultades
            var carrers = await _context.GetAsync<Carrer>("carrers", f => carrerIds.Contains(f.Id));

            return carrers;
        }

        public async Task<List<Subject>> GetSubjectsByIdCarrers(string id)
        {
            Carrer carrer = await GetByIdAsync<Carrer>("carrers", id);
            if (carrer == null)
            {
                // Manejar el caso en que la universidad no exista
                return new List<Subject>(); // O lanzar una excepción, dependiendo de la lógica de tu aplicación
            }

            // Extraer los IDs de las facultades del arreglo en el documento de la universidad
            var subjectIds = carrer.SubjectIds;


            // Usar los IDs de las facultades para buscar las facultades correspondientes en la colección de facultades
            var subjects = await _context.GetAsync<Subject>("subjects", f => subjectIds.Contains(f.Id));

            return subjects;
        }
        #endregion


    }
}

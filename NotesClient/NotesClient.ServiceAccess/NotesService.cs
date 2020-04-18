using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NotesClient.ServiceAccess.Models;

namespace NotesClient.ServiceAccess
{
    public class NotesService : INotesService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public NotesService(HttpClient httpClient, IOptionsMonitor<NotesServiceOptions> optionsAccessor)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(optionsAccessor.CurrentValue.ServiceBaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/notes");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using Stream contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Note>>(contentStream, _jsonSerializerOptions);
        }

        public async Task AddAsync(Note note)
        {
            var resource = new NewNote
            {
                AuthorId = note.AuthorId,
                Text = note.Text,
                Tags = note.Tags
            };

            if (resource.AuthorId == default)
            {
                resource.AuthorId = 2; // ASP.NET Core MVC client default
            }

            string content = JsonSerializer.Serialize(resource);
            var request = new HttpRequestMessage(HttpMethod.Post, "api/notes")
            {
                Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
